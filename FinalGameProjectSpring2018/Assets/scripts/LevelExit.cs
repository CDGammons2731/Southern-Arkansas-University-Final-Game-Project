using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GAMEMANAGER;
using PLAYER;

public class LevelExit : MonoBehaviour {

	[SerializeField]
	GameManager go;
    hud hudObj;
    Player playerObj;
    public bool LevelComplete;
    public bool GameComplete;
	public bool LevelIncreased;

	void Start() {
		LevelIncreased = false;
        go = FindObjectOfType<GameManager>();
        hudObj = GameObject.FindGameObjectWithTag("UI").GetComponent<hud>();
        LevelComplete = false;
        GameComplete = false;
	}
    void Update()
    {
        if( playerObj== null){
            playerObj=GameObject.FindGameObjectWithTag("player").GetComponent<Player>();
        }
        if( hudObj==null){
            hudObj = GameObject.FindGameObjectWithTag("UI").GetComponent<hud>();
        }
        
    }

    //Aaron is testing something
    IEnumerator Wait() {
        yield return new WaitForSeconds(0.5f);
        if (hudObj.Completed == false)
        {
            SceneManager.LoadScene("main", LoadSceneMode.Single);
        }
        else
        {
            //Do nothing (End of the game)
            Debug.Log("Game Complete");
        }
       
    }

    void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("player")) {
            LevelComplete = true;
			if (LevelIncreased == false) {
				go.Level++;
				LevelIncreased = true;
			}
            StartCoroutine(Wait());   //This simply gives the Completed bool a chance to change before the statement makes a decision to load the next scene   

		}
	}
}

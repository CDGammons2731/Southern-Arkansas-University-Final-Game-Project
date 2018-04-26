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
    public int numLvls=0;


	void Start() {
        go = FindObjectOfType<GameManager>();
        hudObj = GameObject.FindGameObjectWithTag("UI").GetComponent<hud>();
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

    void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("player")) {


                numLvls++;
                SceneManager.LoadScene("main", LoadSceneMode.Single);




		}
	}
}

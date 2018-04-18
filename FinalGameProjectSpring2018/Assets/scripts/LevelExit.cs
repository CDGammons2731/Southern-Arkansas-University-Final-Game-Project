using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GAMEMANAGER;

public class LevelExit : MonoBehaviour {

	[SerializeField]
	GameManager go;
    hud hudObj;
    int numLvls=0;


	void Start() {
        go = FindObjectOfType<GameManager>();
        hudObj = GameObject.FindGameObjectWithTag("UI").GetComponent<hud>();
	}
    void Update()
    {
        if(numLvls==6){
           /* if(hudObj.evid==0 || hudObj.evid<= 5){
                
            }*/
            
        }
    }

    void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("player")) {
			SceneManager.LoadScene ("main", LoadSceneMode.Single);
            numLvls++;
		}
	}
	
}

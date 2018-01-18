using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using System.IO;

using System;
using SAVE;
[Serializable]
public class GameManager : MonoBehaviour {
	public PlayerHealth P;
	public Text stats;

	public SaveGame SG;
	// @TESTING
	public void HealthUp(){
		SG.health += 100;
	}
	public void ArmorUp(){
		SG.armor += 50;
	}
	public void ScoreUp(){
		SG.score += 250;
	}
	public void NameChange(){

	}

	void Update(){
		
	
	}
	//End TESTING
}

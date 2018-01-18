using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SAVE;
[Serializable]
public class PlayerHealth : MonoBehaviour {
	public GameObject PlayerObj;
	public AudioClip run,jump,speak,hurt,death;
	public int playerHealth =100;
	public int playerArmor = 50;
	public int score=0;
	bool upgrade = false;
	//string name;
	public SaveGame SG;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

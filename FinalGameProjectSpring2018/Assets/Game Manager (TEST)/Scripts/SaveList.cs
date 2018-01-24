using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GAMEMANAGER;

public class SaveList : MonoBehaviour {
	public GameObject ClickText;
	public GameObject ListPosition;
	public string NameSaved;
	public int saveNumber;
	public Vector3 increment= new Vector3 (0.2f,0,0);

	// Use this for initialization
	void Start () {
		//GameObject File = Instantiate (ClickText, ListPosition.transform.position, ListPosition.transform.rotation);
	}
	void MakeNewSpace(){
		GameObject File = Instantiate (ClickText, ListPosition.transform.position + increment, ListPosition.transform.rotation);
	}
	// Update is called once per frame
	void Update () {
		
	}
}

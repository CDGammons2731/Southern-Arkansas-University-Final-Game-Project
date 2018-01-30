using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GAMEMANAGER;

public class SaveList : MonoBehaviour {
	public GameObject ClickText;
	public GameObject ListPosition;
	public string NameSaved;
	public int saveNumber=0;
	public Vector3 increment= new Vector3 (0.01f,0,0);
	public Vector3 newPos;

	// Use this for initialization
	void Start () {
		Vector3 newPos=ListPosition.transform.position + increment;
		//GameObject File = Instantiate (ClickText, ListPosition.transform.position, ListPosition.transform.rotation);
	}
	public void MakeNewSpace(){
		if (saveNumber <= 3) {
			GameObject File = Instantiate (ClickText, newPos, ListPosition.transform.rotation);
			saveNumber++;
			Vector3 v3=new Vector3(increment.x* (float)saveNumber,increment.y* (float)saveNumber, increment.z);
			increment = increment + v3;
			newPos = newPos + increment;
		}
		else {
			Debug.Log ("Cannot create more save files");
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}

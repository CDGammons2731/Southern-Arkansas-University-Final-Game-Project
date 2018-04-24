using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour {

	Text txt;
	// Use this for initialization
	void Start () {
		txt = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		txt.text = (1 / Time.deltaTime).ToString();
	}
}

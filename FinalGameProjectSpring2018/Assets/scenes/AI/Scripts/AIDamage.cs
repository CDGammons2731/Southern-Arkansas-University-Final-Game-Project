using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GUN;

public class AIDamage : MonoBehaviour {

	//private Gun gun;
	public float Health = 100.0f;
	//public int bonusDamage = 2;


	// Update is called once per frame
	void Update () {
		Debug.Log (Health);
	}

	void OnTriggerEnter(Collider other){
			if (Health >= 0) {
				Health -= 10;
			} else {
				Health = 0;
			}
		}
}

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
		//Debug.Log (Health);
	}

	void OnCollisionEnter(Collision collision){
		if (collision.transform.gameObject.name == "bullet") {
			if (Health >= 0) {
				Health -= 10;
			} else {
				Health = 0;
			}
		} else if (collision.transform.gameObject.name == "pellet") {
			if (Health >= 0) {
				Health -= 10;
			} else {
				Health = 0;
			}
		}
		}
}

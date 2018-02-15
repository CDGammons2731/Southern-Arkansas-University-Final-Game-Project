using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GUN;

public class AIDamage : MonoBehaviour {

	private Gun gun;
	public int Health = 100;
	public int bonusDamage = 2;

	void Update(){
		
	}

	void OnCollisionEnter(Collision collision){
		if (collision.transform.gameObject.tag == "bullet") {
			if (Health >= 0) {
				Debug.Log ("Health: " + Health);
				Health -= gun.damage;
			} else {
				Health = 0;
			}
		} else if (collision.transform.gameObject.tag == "pellet") {
			if (Health >= 0) {
				Health -= gun.damage;
			} else {
				Health = 0;
			}
		}
		}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GUN;
using PLAYER;

public class AIDamage : MonoBehaviour {

	public int Health = 100;
	public int damage = 0;
	public 


	void start(){

	}


	void Update(){

		damage = GetComponent<Player> ().weapon.GetComponent<Gun> ().damage;
		//Debug.Log ("Damage: " + damage);

	}

	void OnCollisionEnter(Collision collision){
		if (collision.transform.gameObject.tag == "bullet") {
			if (Health >= 0) {
				Health -= damage;
				//Debug.Log ("Health: " + Health);
			} else {
				Health = 0;
				gameObject.SetActive (false);
			}
		} else if (collision.transform.gameObject.tag == "pellet") {
			if (Health >= 0) {
				Health -= damage;
			} else {
				Health = 0;
				gameObject.SetActive (false);
			}
		}
		}
}

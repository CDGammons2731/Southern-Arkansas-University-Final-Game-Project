using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GUN;

public class AIDAMAG : MonoBehaviour {

	public int Health = 100;
	public int Samage = 0;
	public GameObject player;


	void Awake(){
		player = GameObject.FindGameObjectWithTag ("player");
	}


	void Update(){
		if (player.GetComponent<Player> ().weapon.GetComponent<Gun> ().damage!=null) {
			Samage = player.GetComponent<Player> ().weapon.GetComponent<Gun> ().damage;
			//Debug.Log ("Damage: " + Samage);
		}
	}

	void OnCollisionEnter(Collision collision){
		if (collision.transform.gameObject.tag == "bullet") {
			if (Health >= 0) {
				Health -= Samage;
				Debug.Log ("Health: " + Health);
			} else {
				Health = 0;
			}
		} else if (collision.transform.gameObject.tag == "pellet") {
			if (Health >= 0) {
				Health -= Samage;
			} else {
				Health = 0;
			}
		}
	}
}

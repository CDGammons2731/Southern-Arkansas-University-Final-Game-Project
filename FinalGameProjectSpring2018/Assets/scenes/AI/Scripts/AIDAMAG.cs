using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GUN;
using PLAYER;

public class AIDAMAG : AISpawner {

	Animator anim;
	public int Health = 30;
	public static int Samage = 0;
	public static int Dmge = 0;
	public Transform Head;
	public Transform Bullet;
	public GameObject plyr;
	public int LookRange = 20;
	public bool EnemyHasDied = false;



	void Awake(){
		player = GameObject.FindGameObjectWithTag ("player");
		anim = GetComponent<Animator> ();
	}


	void Update(){
				
		if (Vector3.Distance (player.transform.position, gameObject.transform.position) <= LookRange) {
			gameObject.transform.LookAt (player.transform);
			anim.SetTrigger ("IsFiring");
		} else {
			anim.ResetTrigger ("IsFiring");
		}

		if (plyr.GetComponent<Player> ().weapon.GetComponent<Gun> ().damage != null && Input.GetKeyDown (KeyCode.F)) {
			Samage = AISpawner.Damage;
			Debug.Log (Samage);
		}
	}


	void OnCollisionEnter(Collision collision){
		if (collision.transform.gameObject.tag == "bullet") {
				if (Health >= 0) {
					Health -= Samage;
					//Debug.Log ("Health: " + Health);
				} else {
					DestroyObject (gameObject);
				}
		} else if (collision.transform.gameObject.tag == "pellet") {
				if (Health >= 0) {
					Health -= Samage;
				} else {
					DestroyObject (gameObject);
				}
		}

	}

}

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
	public Transform BoboHead;
	public Transform Bullet;
	public LayerMask mask = -1;
	public GameObject plyr;
	public int LookRange = 20;
	public bool EnemyHasDied = false;


	void Awake(){
		player = GameObject.FindGameObjectWithTag ("player");
		anim = GetComponent<Animator> ();
	}


	void Update(){


		RaycastHit hit;
		Ray BoboPeekABOO = new Ray (BoboHead.position, transform.forward);
		Debug.DrawRay (BoboHead.position, transform.forward);

				
		if (Physics.Raycast(BoboPeekABOO, out hit, LookRange, mask.value)) {
			if (hit.collider.tag == "player") {
				anim.SetTrigger ("IsFiring");
			}
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

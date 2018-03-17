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
	public LayerMask mask = -1;
	public Transform Bullet;
	public Transform play;
	public GameObject plyr;
	public int LookRange = 20;
	public bool EnemyHasDied = false;
	public static bool shootHIM = false;
	public static bool MuhFaceHurt = false;



	void Awake(){
		play = GameObject.FindGameObjectWithTag ("player").transform;
		plyr = GameObject.FindGameObjectWithTag ("player");

		anim = GetComponent<Animator> ();
	}


	void Update(){
		float dist = Vector3.Distance (play.position, transform.position);
		shootHIM = AI.Escape;
		MuhFaceHurt = AI.WhosYourDaddy;
		Debug.Log (MuhFaceHurt);

		RaycastHit hit;
		Ray BoboPeekABOO = new Ray (BoboHead.position, transform.forward);
		//Debug.DrawRay (BoboHead.position, transform.forward);
				
		if (shootHIM == true) {
			if (MuhFaceHurt == false && dist <= 5) {
				anim.SetTrigger ("IsFiring");
				anim.ResetTrigger ("IsHitting");
			} else if(MuhFaceHurt == true && dist <= 5) {
				anim.ResetTrigger ("IsFiring");
				anim.SetTrigger ("IsHitting");
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
			AI.Escape = true;
				if (Health >= 0) {
					Health -= Samage;
					//Debug.Log ("Health: " + Health);
				} else {
					DestroyObject (gameObject);
				}
		} else if (collision.transform.gameObject.tag == "pellet") {
			AI.Escape = true;
				if (Health >= 0) {
					Health -= Samage;
				} else {
					DestroyObject (gameObject);
				}
		}

	}

}

﻿using System.Collections;
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
	public static float dist;
	public float Distance;
	public string curGun;
    //Aaron is adding this




 
    void Awake(){
		play = GameObject.FindGameObjectWithTag ("player").transform;
		plyr = GameObject.FindGameObjectWithTag ("player");

		anim = GetComponent<Animator> ();

	}

	/*void GetDamage(string weaponName){
		switch (weaponName) {
		case "shotgun":
			Samage = plyr.GetComponent<Player> ().weapon.GetComponent<Gun> ().damage;
			break;
		case"revolver":
			Samage = plyr.GetComponent<Player> ().weapon.GetComponent<Gun> ().damage;
			break;
		case "railgun":
			Samage = plyr.GetComponent<Player> ().weapon.GetComponent<Gun> ().damage;
			break;
		case"raygun":
			Samage = plyr.GetComponent<Player> ().weapon.GetComponent<Gun> ().damage;
			break;
		case "rifle":
			Samage = plyr.GetComponent<Player> ().weapon.GetComponent<Gun> ().damage;
			
			break;
		default:
			//do nothing
			break;
		}

	}
    */

	void Update(){

		curGun = Player.AIDAMAGCURRENTGUNINFO;

		if (curGun == "railgun") {
			Samage = 12;
		} else if (curGun == "raygun") {
			Samage = 10;
		} else if (curGun == "shotgun") {
			Samage = 15;
		} else if (curGun == "revolver") {
			Samage = 10;
		} else if (curGun == "tommygun") {
			Samage = 6;
		}

		dist = AIDistanceCalculator.ClosestEnemyDistance;
		Distance = Vector3.Distance (gameObject.transform.position, play.position);
		shootHIM = AI.Escape;
		MuhFaceHurt = AI.WhosYourDaddy;
	

        

       // RaycastHit hit;
        //Ray BoboPeekABOO = new Ray (BoboHead.position, transform.forward);
		//Debug.DrawRay (BoboHead.position, transform.forward);
		if (shootHIM == true && Distance <= LookRange) {
			if (MuhFaceHurt == false) {
				anim.SetTrigger ("IsFiring");
				anim.ResetTrigger ("IsHitting");
			} else {
				Debug.Log ("I'm here!");
				anim.ResetTrigger ("IsFiring");
				anim.SetTrigger ("IsHitting");
			}
		} else {
			anim.ResetTrigger ("IsFiring");
		}

		//Samage = plyr.GetComponent<Player>().weapon.GetComponent<Gun>().;

        //GetDamage (plyr.GetComponent<Player> ().currentGun);
      
		/*if (plyr.GetComponent<Player> ().weapon.GetComponent<Gun> ().damage != null && Input.GetKeyDown (KeyCode.F)) {
			Samage = AISpawner.Damage;
			Debug.Log (Samage);
		}*/
	}


	void OnCollisionEnter(Collision collision){
		if (collision.transform.gameObject.tag == "bullet") {
			AI.Escape = true;
				if (Health > 0) {
				Health -= Samage;
				Debug.Log ("Health: " + Health);
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

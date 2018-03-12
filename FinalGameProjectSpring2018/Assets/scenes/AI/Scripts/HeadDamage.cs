using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GUN;
using PLAYER;

public class HeadDamage : AIDAMAG {
	public static int Clamage;
	public GameObject BoboGoBOOOM;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("player");
	}
	
	// Update is called once per frame
	void Update () {
		if (plyr.GetComponent<Player> ().weapon.GetComponent<Gun> ().damage != null && Input.GetKeyDown (KeyCode.F)) {
			Clamage = AIDAMAG.Damage;
			Clamage = Clamage + (Clamage / 2);
			Debug.Log (Clamage);
		}
	}

	void OnCollisionEnter(Collision collision){
		if (collision.transform.gameObject.tag == "bullet") {
			if (Health >= 0) {
				Health -= Clamage;
				Debug.Log ("Clamage is done!");
			} else {
				DestroyObject (BoboGoBOOOM);
			}
		} else if (collision.transform.gameObject.tag == "pellet") {
			if (Health >= 0) {
				Health -= Clamage;
			} else {
				DestroyObject (BoboGoBOOOM);
			}
		}
	}
}

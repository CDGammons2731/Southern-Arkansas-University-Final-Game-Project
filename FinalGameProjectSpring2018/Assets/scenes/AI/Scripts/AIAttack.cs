using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : MonoBehaviour {

	//public Animation Meleeanim;
	//public Animation Shootanim;
	public GameObject bullet;
	public GameObject ShootableObject;
	public Transform bulletSpawn;
	public Transform plyr;
	public int Shootrange;
	public int Meleerange;
	public int LookRange = 5;
	public int bulletSpeed = 200;

	// Use this for initialization
	void Start () {
		plyr = GameObject.FindGameObjectWithTag("player").transform;
		bullet = GameObject.FindGameObjectWithTag ("bullet");
	}

	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(plyr.position, gameObject.transform.position) <= LookRange){
			gameObject.transform.LookAt (plyr);
		}

		if(Vector3.Distance(plyr.position, gameObject.transform.position) <= Shootrange && Vector3.Distance(plyr.position, gameObject.transform.position) > Meleerange){
			//Shootanim.Play ();
			Instantiate(bullet, gameObject.transform.position, bulletSpawn.rotation);
			bullet.transform.position = Vector3.MoveTowards (bullet.transform.position, plyr.position, bulletSpeed);
		}

		if(Vector3.Distance(plyr.position, gameObject.transform.position) <= Meleerange){
			//Meleeanim.Play ();
		}
	}

}

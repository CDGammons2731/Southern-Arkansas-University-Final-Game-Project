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
	public int Shootrange = 5;
	public int Meleerange = 3;
	public int LookRange = 5;
	public int bulletSpeed = 200;

	// Use this for initialization
	void Start () {
		plyr = GameObject.FindGameObjectWithTag("player").transform;
	}

	// Update is called once per frame
	void Update () {



		if(Vector3.Distance(plyr.position, gameObject.transform.position) <= LookRange){
			gameObject.transform.LookAt (plyr);
			Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation);
			bullet.transform.position = Vector3.MoveTowards (bullet.transform.position, plyr.position, bulletSpeed);
		}

		if(Vector3.Distance(plyr.position, gameObject.transform.position) <= Shootrange){
			//Shootanim.Play ();

		}

		if(Vector3.Distance(plyr.position, gameObject.transform.position) <= Meleerange){
			//Meleeanim.Play ();
		}
	}

}

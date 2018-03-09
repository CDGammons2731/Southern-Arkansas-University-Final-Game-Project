using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : MonoBehaviour {

	public GameObject bullet;
	public GameObject ShootableObject;
	public Transform bulletSpawn;
	public Transform plyr;
	public int Shootrange = 5;
	public int Meleerange = 3;
	public int LookRange = 5;
	public int bulletSpeed = 200;
	public int X = 0;

	// Use this for initialization
	void Start () {
		plyr = GameObject.FindGameObjectWithTag("player").transform;
	}

	// Update is called once per frame
	void Update () {



		if(Vector3.Distance(plyr.position, gameObject.transform.position) <= LookRange){
			gameObject.transform.LookAt (plyr);
			if (X == 20) {
				bullet = (GameObject)Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
				bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
				X = 0;
			}
			X++;

			//Destroy(bullet, 0.35f);

		}

		if(Vector3.Distance(plyr.position, gameObject.transform.position) <= Shootrange){


		}

		if(Vector3.Distance(plyr.position, gameObject.transform.position) <= Meleerange){
		}
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : MonoBehaviour {

	public GameObject bullet;
	public GameObject shot;
	public Transform bulletSpawn;
	public Transform plyr;
	public Transform EnemyShotLookat;
	public int Shootrange = 5;
	public int Meleerange = 3;
	public int LookRange = 20;
	public int bulletSpeed = 200;
	public static bool BoboShoot;

	public int X = 0;

	// Use this for initialization
	void Start () {
		plyr = GameObject.FindGameObjectWithTag("player").transform;
		EnemyShotLookat = GameObject.FindGameObjectWithTag("Player").transform;
	}

	// Update is called once per frame
	void Update () {


		BoboShoot = AI.Escape;


		if(BoboShoot == true){
				shot = (GameObject)Instantiate (bullet, bulletSpawn.position, bulletSpawn.rotation);
				shot.transform.LookAt (EnemyShotLookat.position);
				shot.GetComponent<Rigidbody> ().velocity = shot.transform.forward * bulletSpeed;
			Destroy (shot, 0.35f);
		}
	}

}

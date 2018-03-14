using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : MonoBehaviour {

	public GameObject bullet;
	public GameObject shot;
	public Transform BoboHead;
	public Transform bulletSpawn;
	public Transform plyr;
	public LayerMask mask = -1;
	public int Shootrange = 5;
	public int Meleerange = 3;
	public int LookRange = 20;
	public int bulletSpeed = 200;
	public int X = 0;
	public static bool ShootHIM;


	// Use this for initialization
	void Start () {
		plyr = GameObject.FindGameObjectWithTag("player").transform;
	}

	// Update is called once per frame
	void Update () {
		ShootHIM = AI.Escape;

		RaycastHit hit;
		Ray BoboPeekABOO = new Ray (BoboHead.position, transform.forward);
		Debug.DrawRay (BoboHead.position, transform.forward);

		if(ShootHIM == true){
				if (X == 47) {
					shot = (GameObject)Instantiate (bullet, bulletSpawn.position, bulletSpawn.rotation);
					shot.GetComponent<Rigidbody> ().velocity = shot.transform.forward * bulletSpeed;
					X = 0;
				}
				X++;

				Destroy (shot, 0.35f);
		}

		//if(Vector3.Distance(plyr.position, gameObject.transform.position) <= Meleerange){
		//}
	}

}

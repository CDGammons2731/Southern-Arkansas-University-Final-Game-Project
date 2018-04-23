using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : MonoBehaviour {

	public GameObject bullet;
	public GameObject shot;
	public Transform bulletSpawn;
	public Transform plyr;
	public Transform EnemyShotLookat;
	public static bool BoboEscape;
	public int Shootrange = 5;
	public int Meleerange = 3;
	public int LookRange = 20;
	public int bulletSpeed = 200;
	public float BobofireRate = 0.8f;
	public float BobonextFire;
	public AudioSource m_MyAudioSource;

	public int X = 0;

	// Use this for initialization
	void Start () {
		plyr = GameObject.FindGameObjectWithTag("player").transform;
		EnemyShotLookat = GameObject.FindGameObjectWithTag("Player").transform;
		m_MyAudioSource = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {

		BoboEscape = AI.Escape;

		if(Time.time > BobonextFire){
			if (BoboEscape = true) {
				if (Vector3.Distance (gameObject.transform.position, plyr.transform.position) > 3 && Vector3.Distance (gameObject.transform.position, plyr.transform.position) < 20) {
					m_MyAudioSource.Play ();
					BobonextFire = Time.time + BobofireRate;
					shot = (GameObject)Instantiate (bullet, bulletSpawn.position, bulletSpawn.rotation);
					shot.transform.LookAt (EnemyShotLookat.position);
					shot.GetComponent<Rigidbody> ().velocity = shot.transform.forward * bulletSpeed;
					Destroy (shot, 0.8f);
				}
			}
		}
	}
}

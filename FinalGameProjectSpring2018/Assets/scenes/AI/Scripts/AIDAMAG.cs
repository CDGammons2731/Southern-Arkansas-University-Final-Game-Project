using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GUN;

public class AIDAMAG : MonoBehaviour {

	public int Health = 30;
	public int Samage = 0;
	public int EnemyNumber = 0;
	public GameObject player;
	public float Respawn = 0f;
	public float TimeToRespawn = 300f;


	void Awake(){
		player = GameObject.FindGameObjectWithTag ("player");
		Respawn = TimeToRespawn;
	}


	void Update(){
		if (player.GetComponent<Player> ().weapon.GetComponent<Gun> ().damage!=null) {
			Samage = player.GetComponent<Player> ().weapon.GetComponent<Gun> ().damage;
			//Debug.Log ("Damage: " + Samage);
		}

		Respawn = Respawn - Time.deltaTime;
		if (Respawn <= 0) {
			Respawn = TimeToRespawn;
		}
	}

	void OnCollisionEnter(Collision collision){
		if (collision.transform.gameObject.tag == "bullet") {
			if (Health >= 0) {
				Health -= Samage;
				//Debug.Log ("Health: " + Health);
			} else {
				if (EnemyNumber <= 2) {
					EnemyNumber++;
				}
				DestroyObject (gameObject);
			}
		} else if (collision.transform.gameObject.tag == "pellet") {
			if (Health >= 0) {
				Health -= Samage;
			} else {
				EnemyNumber++;
				DestroyObject (gameObject);
			}
		}
	}
}

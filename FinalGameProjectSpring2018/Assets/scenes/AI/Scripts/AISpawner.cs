using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AISpawner : AIDAMAG {
	public GameObject AIToSpawn;
	public int EnemiesNumber = 0;
	public int EnemiesMaxNumber = 2;
	public float range = 10.0f;
	public bool SpawnActivateRandomizer = false;
	private float SpawnActivator = 0.0f;
	public float NumberToActivate = 5.0f;
	public float MaxRange = 10.0f;
	public float Respawn = 0f;
	public float TimeToRespawn = 300f;

	// Use this for initialization
	void Start () {
		//SpawnActivator = Random.Range (0.0f, MaxRange);
		SpawnActivator = 6;
		Debug.Log (SpawnActivator);
		if (SpawnActivator > NumberToActivate) {
			SpawnActivateRandomizer = true;
		}
	}

	bool RandomPoint(Vector3 Center, float range, out Vector3 result){
		for (int i = 0; i < 30; i++) {
			Vector3 RandomPosition = Center + Random.insideUnitSphere * range;
			NavMeshHit hit;
			if (NavMesh.SamplePosition (RandomPosition, out hit, 20.0f, NavMesh.AllAreas)) {
				result = hit.position;
				return true;
			}
		}
		result = Vector3.zero;
		return false;
	}
	
	// Update is called once per frame
	void Update () {
		EnemiesNumber = EnemiesNumber - EnemyNumber;


		if (SpawnActivateRandomizer == true) {
			if (Respawn <= 0f) {
				Vector3 point;
				if (EnemiesNumber <= EnemiesMaxNumber) {
					if (RandomPoint (transform.position, range, out point)) {
						Instantiate (AIToSpawn, point, Quaternion.identity);
						EnemiesNumber++;
						Respawn = TimeToRespawn;
					}
				} else {
					Respawn = TimeToRespawn;
				}
			}

			Respawn = Respawn - Time.deltaTime;
			Debug.Log (Respawn);
		}
	}
}

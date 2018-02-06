using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AISpawner : MonoBehaviour {
	public GameObject AIToSpawn;
	private int EnemiesNumber = 0;
	public int EnemiesMaxNumber = 5;
	public float range = 10.0f;
	public bool SpawnActivateRandomizer = false;
	private float SpawnActivator = 0.0f;
	public float NumberToActivate = 5.0f;
	public float MaxRange = 10.0f;

	// Use this for initialization
	void Start () {
		SpawnActivator = Random.Range (0.0f, MaxRange);
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
		if (SpawnActivateRandomizer == true) {
			Vector3 point;
			if (RandomPoint (transform.position, range, out point)) {
				//Debug.DrawRay (point, Vector3.up, Color.blue, 1.0f);
				if (EnemiesNumber <= EnemiesMaxNumber) {
					Instantiate (AIToSpawn, point, Quaternion.identity);
					EnemiesNumber++;
				}
			}
		}
	}
}

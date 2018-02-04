using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AISpawner : MonoBehaviour {
	//public GameObject AItoSpawn;
	//public Transform LocationtoSpawn;
	public float range = 10.0f;

	// Use this for initialization
	void Start () {
		//Vector3 RandomPosition = Random.insideUnitSphere * 15f;
		//NavMeshHit hit;
		//LocationtoSpawn = NavMesh.SamplePosition (AItoSpawn.position, out hit, 20f, NavMesh.AllAreas);
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
		Vector3 point;
		if(RandomPoint(transform.position, range, out point)){
			Debug.DrawRay (point, Vector3.up, Color.blue, 1.0f);
		}
	}
}

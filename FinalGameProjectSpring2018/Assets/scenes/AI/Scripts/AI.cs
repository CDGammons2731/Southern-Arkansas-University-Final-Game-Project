using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour 
{

	public Transform destination;
	public Transform DatDerBadGoober;
	private NavMeshAgent agent;
	public float range = 10.0f;
	int X = 0;


	void Start(){

		//Arron changed destination and DatDerBadGoober to look for tag instead of Hierarchy
		//GameObject. Just a note for clarification. My method did work for finding the
		//player but it is more efficent to look for the tag in the Hierarchy.

        destination = GameObject.FindGameObjectWithTag("player").transform;
        //destination = GameObject.Find("player").transform;
        DatDerBadGoober = GameObject.FindGameObjectWithTag("player").transform;
        //DatDerBadGoober = GameObject.Find ("player").transform;

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


	void Update ()
	{

		float dist = Vector3.Distance (DatDerBadGoober.position, transform.position);
		//Debug.Log ("Distance to AI: " + dist);

		Vector3 point;
		if (X != 200) {
			X++;
		} else if (dist > 20) {
			if (RandomPoint (transform.position, range, out point)) {
				float Idledist = Vector3.Distance (point, transform.position);
				agent = gameObject.GetComponent<NavMeshAgent> ();
					agent.SetDestination (point);
			}
			X = 0;
		}

		if (dist > 5 && dist <= 20) {
			agent.SetDestination (destination.position);
		} else if (dist <= 5) {
			agent.SetDestination (transform.position);
		}
		
	}
}

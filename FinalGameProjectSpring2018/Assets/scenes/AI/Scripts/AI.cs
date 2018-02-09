using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour 
{

	public Transform destination;
	public Transform DatDerBadGoober;
	private NavMeshAgent agent;


	void Start(){

		//Arron changed destination and DatDerBadGoober to look for tag instead of Hierarchy
		//GameObject. Just a note for clarification. My method did work for finding the
		//player but it is more efficent to look for the tag in the Hierarchy.

        destination = GameObject.FindGameObjectWithTag("player").transform;
        //destination = GameObject.Find("player").transform;
        DatDerBadGoober = GameObject.FindGameObjectWithTag("player").transform;
        //DatDerBadGoober = GameObject.Find ("player").transform;

    }

	void Update () 
	{

		float dist = Vector3.Distance (DatDerBadGoober.position, transform.position);
		//Debug.Log ("Distance to AI: " + dist);

		if (dist <= 8) {
			agent = gameObject.GetComponent<NavMeshAgent> ();
			if (dist > 5) {
				agent.SetDestination (destination.position);
			} else {
				agent.SetDestination (transform.position);
			}
		}
	}


}

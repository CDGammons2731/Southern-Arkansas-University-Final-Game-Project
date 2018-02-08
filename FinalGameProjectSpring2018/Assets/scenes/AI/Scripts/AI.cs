using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour 
{

	public Transform destination;
	public Transform DatDerBadGoober;
	public GameObject bullet;
	public GameObject pellet;
	private NavMeshAgent agent;
	public float Health = 100.0f;
	//public int bonusDamage = 2;

	void Start(){

		//Arron changed destination and DatDerBadGoober to look for tag instead of Hierarchy
		//GameObject. Just a note for clarification. My method did work for finding the
		//player but it is more efficent to look for the tag in the Hierarchy.

        destination = GameObject.FindGameObjectWithTag("player").transform;
        //destination = GameObject.Find("player").transform;
        DatDerBadGoober = GameObject.FindGameObjectWithTag("player").transform;
        //DatDerBadGoober = GameObject.Find ("player").transform;
		//bullet = GameObject.FindGameObjectWithTag("bullet");
		//pellet = GameObject.FindGameObjectWithTag("pellet");
    }

	void Update () 
	{
		float dist = Vector3.Distance (DatDerBadGoober.position, transform.position);
		//Debug.Log ("Distance to AI: " + dist);

		if (dist <= 5) {
			agent = gameObject.GetComponent<NavMeshAgent> ();
			agent.SetDestination (destination.position);
		}
	}

	/*void OnCollisionEnter(Collision Col){
		if (bullet) {
			if (Health >= 0) {
				Health -= 11.273846f;
			} else {
				Health = 0;
			}
		}
		if (pellet) {
			if (Health >= 0) {
				Health -= 11.273846f;
			} else {
				Health = 0;
			}
		}
	}*/


}

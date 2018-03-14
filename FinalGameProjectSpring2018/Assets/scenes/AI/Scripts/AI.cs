using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour 
{

	public Transform destination;
	public Transform DatDerBadGoober;
	public Transform BoboHead;
	public LayerMask mask = -1;
	public float LookRange = 20f;
	private NavMeshAgent agent;
	public float range = 10.0f;
	public static bool Escape;
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
		RaycastHit hit;
		Ray BoboPeekABOO = new Ray (BoboHead.position, transform.forward);
		Debug.DrawRay (BoboHead.position, transform.forward);

		if(Physics.Raycast(BoboPeekABOO, out hit, LookRange)){
			Debug.Log (hit.collider.tag);
			if (hit.collider.tag == "player") {
				Debug.Log ("Bobo sees you!");
			}
		}

		float dist = Vector3.Distance (DatDerBadGoober.position, transform.position);
		//Debug.Log ("Distance to AI: " + dist);

		if (!Physics.Raycast (BoboPeekABOO, out hit, LookRange, mask) && Escape == false) {
			if (hit.collider.tag == "player") {
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
			}
		}

		if (Physics.Raycast (BoboPeekABOO, out hit, LookRange, mask) || Escape == true) {
			if (hit.collider.tag == "player") {
				gameObject.transform.LookAt (destination);
				Escape = true;
				if (dist > 20) {
					Escape = false;
				} else if (dist > 5 && dist <= 20) {
					agent.SetDestination (destination.position);
				} else if (dist <= 5) {
					agent.SetDestination (transform.position);
				}
			}
		}
		
	}
}

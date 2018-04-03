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
	public static bool Escape = false;
	public static bool WhosYourDaddy = false;
	public static float dist;
	public float Distance;
	int X = 200;


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
		Distance = Vector3.Distance (gameObject.transform.position, destination.position);
		dist = AIDistanceCalculator.ClosestEnemyDistance;
		//Debug.Log ("Distance to AI: " + dist);

		if ((Escape == true || WhosYourDaddy == true) && Distance <= LookRange) {
				gameObject.transform.LookAt (destination);
				if (dist > LookRange) {
					Escape = false;
					WhosYourDaddy = false;
				}
			}

		RaycastHit hit;
		Ray BoboPeekABOO = new Ray (gameObject.transform.position, transform.forward * LookRange);
		Debug.DrawRay (BoboHead.position, transform.forward * LookRange, Color.red);

		Debug.Log (BoboHead.position);

		if (!Physics.Raycast (BoboPeekABOO, out hit, LookRange, mask) && (Escape == false || Distance > LookRange)) {
			Vector3 point;
			if (X != 200 && dist > 5) {
				X++;
			} else if (dist > 5) {
				if (RandomPoint (transform.position, range, out point)) {
					float Idledist = Vector3.Distance (point, transform.position);
					agent = gameObject.GetComponent<NavMeshAgent> ();
					agent.SetDestination (point);
				}
				X = 0;
			} else if (dist <= 5) {
				Escape = true;
				WhosYourDaddy = true;
			}
		}
		if (Distance <= LookRange) {
			if (Physics.Raycast (BoboPeekABOO, out hit, LookRange, mask) || Escape == true) {
				if (dist > 20) {
					Escape = false;
					WhosYourDaddy = false;
					Debug.Log ("Made It Here!");
					X = 0;
				} else if (dist > 5 && dist <= 20) {
					Escape = true;
					agent.SetDestination (destination.position);
					WhosYourDaddy = false;
					Debug.Log ("Whos Your Daddy: " + WhosYourDaddy);
				} else if (dist <= 5) {
					WhosYourDaddy = true;
					if (dist > 3) {
						agent.SetDestination (destination.position);
					} else {
						if (Distance <= 3) {
							agent.SetDestination (transform.position);
						} else {
							agent.SetDestination (destination.position);
						}
					}
				}
			} 
		}
	}
}

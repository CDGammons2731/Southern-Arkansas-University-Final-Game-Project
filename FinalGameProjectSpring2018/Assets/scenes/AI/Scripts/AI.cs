using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour 
{

	public Transform destination;

	private NavMeshAgent agent;

	void Update () 
	{
		agent = gameObject.GetComponent<NavMeshAgent>();

		agent.SetDestination(destination.position);
	}

}

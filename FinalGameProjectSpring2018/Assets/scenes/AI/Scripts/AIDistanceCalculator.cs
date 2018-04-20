using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDistanceCalculator : MonoBehaviour {
	public GameObject player;
	public GameObject[] Enemies;
	public Transform[] EnemyLocations;
	public float[] EnemyDistances;
	public static float ClosestEnemyDistance;
	public static int EnemiesCount;
	
	// Update is called once per frame
	void Update () {
		player = GameObject.FindGameObjectWithTag ("player");

		Enemies = GameObject.FindGameObjectsWithTag ("AI");
		EnemyLocations = new Transform[Enemies.Length];
		EnemyDistances = new float[Enemies.Length];
		EnemiesCount = Enemies.Length;

		for (int i = 0; i < EnemyLocations.Length; i++) {
			EnemyLocations [i] = Enemies [i].transform;
		}

		for (int i = 0; i < EnemyDistances.Length; i++) {
			EnemyDistances [i] = Vector3.Distance (EnemyLocations [i].position, player.transform.position);
		}

		ClosestEnemyDistance = Mathf.Min (EnemyDistances);
	}
}

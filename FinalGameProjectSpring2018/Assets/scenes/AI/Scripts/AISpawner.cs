﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GUN;
using PLAYER;
using GAMEMANAGER;


public class AISpawner : MonoBehaviour {
	public GameObject AIToSpawn;
	public GameObject AIToSpawnHarder;
	public GameObject AIToSpawnHardest;
	public GameObject player;
	public static int Damage;
	public GameObject[] Enemies;
	public int EnemiesNumber = 0;
	public int LevelNum;
	public int EnemiesMaxNumber = 1;
	public float range = 10.0f;
	public bool SpawnActivateRandomizer = false;
	private float SpawnActivator = 0.0f;
	public float NumberToActivate = 5.0f;
	public float MaxRange = 10.0f;
	public float Respawn = 0f;
	public float TimeToRespawn = 300f;
	public float WaitToStart = 25f;
	public static int EnemiesCount;

	// Use this for initialization
	void Start () {
		
		Enemies = new GameObject[2];
		player = GameObject.FindGameObjectWithTag ("player");

		SpawnActivator = Random.Range (0.0f, MaxRange);
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
		LevelNum = GameManager.GAME.Level;
		EnemiesCount = AIDistanceCalculator.EnemiesCount;

		/*if (player.GetComponent<Player> ().weapon.GetComponent<Gun> ().damage!=null && Input.GetKeyDown(KeyCode.F)) {
			Damage = player.GetComponent<Player> ().weapon.GetComponent<Gun> ().damage;
			Debug.Log ("Damage: " + Damage);
		}*/
		if (EnemiesCount < 20) {
			if (SpawnActivateRandomizer == true) {
				if (Respawn <= 0f) {
					Vector3 point;
					if (EnemiesNumber <= EnemiesMaxNumber) {
						if (RandomPoint (transform.position, range, out point)) {
							if (Enemies [0] == null) {
								if (LevelNum == 1 || LevelNum == 2) {
									Enemies [0] = Instantiate (AIToSpawn, point, Quaternion.identity);
								} else if (LevelNum == 3 || LevelNum == 4) {
									Enemies [0] = Instantiate (AIToSpawnHarder, point, Quaternion.identity);
								} else if (LevelNum == 5) {
									Enemies [0] = Instantiate (AIToSpawnHardest, point, Quaternion.identity);
								}
							}

                            if (Enemies [1] == null) {
								if (LevelNum == 1 || LevelNum == 2) {
									Enemies [0] = Instantiate (AIToSpawn, point, Quaternion.identity);
								} else if (LevelNum == 3 || LevelNum == 4) {
									Enemies [0] = Instantiate (AIToSpawnHarder, point, Quaternion.identity);
								} else if (LevelNum == 5) {
									Enemies [0] = Instantiate (AIToSpawnHardest, point, Quaternion.identity);
								}
							}

							EnemiesNumber++;
							Respawn = TimeToRespawn;
						}
					} else {
						Respawn = TimeToRespawn;
					}

				} else {
					if (EnemiesNumber > EnemiesMaxNumber) {
						if (Enemies [0] == null) {
							EnemiesNumber--;
						}

						if (Enemies [1] == null) {
							EnemiesNumber--;
						}
					}
				}

				Respawn = Respawn - Time.deltaTime;
				//Debug.Log (Respawn);
			}
		}
	}
}

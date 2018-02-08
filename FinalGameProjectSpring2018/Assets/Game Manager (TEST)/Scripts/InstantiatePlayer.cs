using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePlayer : MonoBehaviour {
    public GameObject Player;
    public Transform spawnPoint;
	// Use this for initialization
	void Start () {
        if (Player != null)
        {
            Instantiate(Player, spawnPoint.position, spawnPoint.rotation);
            Player.GetComponent<Camera>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

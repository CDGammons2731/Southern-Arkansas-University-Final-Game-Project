using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unparent : MonoBehaviour {
    //Creating this script for the sole purpose of unparenting the player from a prefab at the beginning of the game
    public GameObject Player;
    
	void Awake () {
        if (Player != null) {
            Player.transform.parent = null;
        }
	}
	
	void Update () {
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GUN;

public class ShootableBox : MonoBehaviour {
    //This is just a test
    public int health;
	void Start () {
        health = 10;
	}

    void Update () {
        if (health <= 0) {
            Destroy(gameObject);
        }
	}
}

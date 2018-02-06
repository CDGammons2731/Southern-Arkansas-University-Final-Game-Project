using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GUN;

public class ShootableBox : MonoBehaviour {
    //This is just a test
    public GameObject bullet;

    public int health = 50;
    
	void Start () {
      
	}

    private void OnCollisionEnter(Collision collision)
    {
      //  bullet = collision.gameObject;
    }

    void Update () {
        if (health <= 0) {
            Destroy(gameObject);
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GUN;

public class ShootableBox : MonoBehaviour {
    //This is just a test
    public Rigidbody box;
    public Rigidbody projectile;
    public Gun gun;

    public int health = 50;
    
	void Start () {

	}

    private void OnCollisionEnter(Collider other)
    {
        projectile = other.GetComponent<Rigidbody>();
        if (projectile.tag == "bullet" || projectile.tag == "pellet") {
            health -= projectile.GetComponent<Gun>().damage;
        } 
    }


    void Update () {
        if (health <= 0) {
            Destroy(gameObject);
        }
	}
}

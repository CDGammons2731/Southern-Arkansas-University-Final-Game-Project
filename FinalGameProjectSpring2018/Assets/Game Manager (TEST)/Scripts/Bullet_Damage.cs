using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GUN;

public class Bullet_Damage : MonoBehaviour {
    public GameObject parentGun;
    public GameObject entity;
    public Gun gun;
    int damage;
	// Use this for initialization
	void Start () {
        gun = GetComponentInParent<Gun>();
        damage = 1;
	}

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        entity = other.gameObject;
        damage = gun.damage;
        entity.GetComponent<ShootableBox>().health -= damage;
    }
}

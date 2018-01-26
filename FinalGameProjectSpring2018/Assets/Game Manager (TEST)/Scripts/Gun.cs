using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public GameObject weapon;
    public GameObject bullet;
    public Transform bulletSpawn;

    //Weapon Types
    public const string shotgun = "shotgun";
    public const string revolver = "revolver";
    public const string rifle = "rifle";
    public const string tommygun = "tommygun";
    public const string raygun = "raygun";

    //Audio clips:  0-fire, 1-reload, 2-any extra audio
    public AudioClip[] SHOTGUN = new AudioClip[2];
    public AudioClip[] REVOLVER = new AudioClip[2];
    public AudioClip[] RIFLE = new AudioClip[2];
    public AudioClip[] TOMMYGUN = new AudioClip[2];
    public AudioClip[] RAYGUN = new AudioClip[2];

    public bool equipped;

    public int ammo;
    public int ammoClip;
    public int ammoMax;
    public int damage; //per pullet
    public int rangeMult =2; //multiply damage for closer range? 

    // Use this for initialization
    void Start () {
		
	}

    public void FireWeapon(string type)
    {
        switch (type)
        {
            case shotgun:
                ammo = 8;
                ammoClip = 8;
                ammoMax = 48;
                damage = 35;
                //Set pos and rot in each prefab

                break;
            case revolver:
                ammo = 7;
                ammoClip = 7;
                ammoMax = 35;
                damage = 20;

                break;
            case rifle:
                ammo = 30;
                ammoClip = 30;
                ammoMax = 210;
                damage = 8;

                break;
            case tommygun:
                ammo = 50;
                ammoClip = 50;
                ammoMax = 300;
                damage = 6;

                break;
            case raygun:
                ammo = 4;
                ammoClip = 4;
                ammoMax = 24;
                damage = 50;
                break;
            default:
                ammo = 0;
                ammoClip = 0;
                ammoMax = 0;
                damage = 5;

                break;
        }

    }

    //Gun type methods for fireing and ammo amounts 
    void Raygun() { }
    void Rifle() { }
    void Revolver() { }

    void Shotgun() {
        //Make the shot spread
        var shot = (GameObject)Instantiate (bullet, bulletSpawn.position,bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;
        //play sound
        if (SHOTGUN[0] != null) {
          //play sound

        }

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }

    void TommyGun() { }

    void reload(string type)
    {
        //play animation and reset ammo to full or += ammount picked up
        switch (type) {
            case shotgun:
                break;
            case revolver:
                break;
            case rifle:
                break;
            case tommygun:
                break;
            case raygun:
                break;
            default:
                break;
        }
    }


    // Update is called once per frame
    void Update () {
		
	}
}

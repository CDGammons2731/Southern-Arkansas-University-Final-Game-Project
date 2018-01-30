using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUN{
public class Gun : MonoBehaviour {
	//Making all publics so I can mess with them in the editor
    public GameObject weapon;
    public GameObject bullet;
    public Transform bulletSpawn;

    //Weapon Types
    public const string shotgun = "shotgun";
    public const string revolver = "revolver";
    public const string rifle = "rifle";
    public const string tommygun = "tommygun";
    public const string railgun = "railgun";

	public new string CurrentWeapon; //The tag attatched to the gameobjct;

    //Audio clips:  0-fire, 1-reload, 2-any extra audio
    public AudioClip[] SHOTGUN = new AudioClip[2];
    public AudioClip[] REVOLVER = new AudioClip[2];
    public AudioClip[] RIFLE = new AudioClip[2];
    public AudioClip[] TOMMYGUN = new AudioClip[2];
    public AudioClip[] RAILGUN = new AudioClip[2];

    public bool equipped;

    public int ammo;
    public int ammoClip;
    public int ammoMax;
	public int currentAmmo;
    public int damage; //per pullet
    public int rangeMult =2; //multiply damage for closer range? 

    // Use this for initialization
    void Start () {
		CurrentWeapon = gameObject.tag;
		bulletSpawn = gameObject.GetComponentInChildren<Transform> ();
	}

    public void FireWeapon(string type)
    {
		type = CurrentWeapon;
        switch (type)
        {
		case shotgun:
			ammo = 8;
			ammoClip = 8;
			ammoMax = 48;
			damage = 35;
			currentAmmo = 24;
                //Set pos and rot in each prefab
			//testing
			if(Input.GetMouseButton(1)){
				Shotgun (ammo,ammoClip,currentAmmo);
				if (ammo <=0) {
					reload (type);
				}
			}

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
            case railgun:
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
	void Railgun(int ammo, int clip, int available) { }
	void Rifle(int ammo, int clip, int available) { }
	void Revolver(int ammo, int clip, int available) { }

	void Shotgun(int ammo, int clip, int available) {
        //Make the shot spread
		if(available>0){
        var shot = (GameObject)Instantiate (bullet, bulletSpawn.position,bulletSpawn.rotation);
				bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.up * 6;
		available -= ammo;
        //play sound
        if (SHOTGUN[0] != null) {
          //play sound

        }

        // Destroy the bullet after 2 seconds
				Destroy(shot, 2.0f);
    }
	}

	void TommyGun(int ammo, int clip, int available) { }

    void reload(string type)
    {
        //play animation and reset ammo to full or += ammount picked up
        switch (type) {
            case shotgun:
				//play reload animation and sound
				
                break;
            case revolver:
                break;
            case rifle:
                break;
            case tommygun:
                break;
            case railgun:
                break;
            default:
                break;
        }
    }


    // Update is called once per frame
    void Update () {
		
	}
}
}
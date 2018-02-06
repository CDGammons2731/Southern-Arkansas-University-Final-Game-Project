using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUN{
public class Gun : MonoBehaviour {
	//Making all publics so I can mess with them in the editor
    //Get your weapon, bullet, and where it spawns
    public GameObject weapon;
    public GameObject bullet;
    public Transform bulletSpawn; 
    

    //Weapon Types
    [Header("Weapon Types")]
    public const string shotgun = "shotgun";
    public const string revolver = "revolver";
    public const string rifle = "rifle";
    public const string tommygun = "tommygun";
    public const string railgun = "railgun";

	public new string CurrentWeapon; //The tag attatched to the gameobjct;
    
    [Header("Gun Sounds")]
    //Audio clips:  0-fire, 1-reload, 2-any extra audio
    public AudioClip[] SHOTGUN = new AudioClip[2];
    public AudioClip[] REVOLVER = new AudioClip[2];
    public AudioClip[] RIFLE = new AudioClip[2];
    public AudioClip[] TOMMYGUN = new AudioClip[2];
    public AudioClip[] RAILGUN = new AudioClip[2];

    //The source that the audio clips can be played by
    public AudioSource GunSound;

    //Check to see if weapon is equpiied
    public bool equipped; 

    //All your firearm needs...
    [Header("Gun Info")]
    public int ammo;
    public int ammoClip;
    public int ammoMax;
	public int currentAmmo; //keep track of current ammo
    public int damage; //per bullet @TODO: assign damage later
    public int rangeMult =2; //multiply damage for closer range? TBA
    public int bulletSpeed = 30;
    public float fireRate;
    public float nextFire;
    public int shotCount; //keep track of bullets shot, implement for reloading and for UI purposes

        //For shotgun
    public float spreadAgle;
	public int pelletCount;
	List<Quaternion> pellets; //For the shotgun, make a list of quaternions which will be spawn positions for pellets
	
    private LineRenderer lineOfSight; //@TODO: testing, raycasts 

        // Use this for initialization
        void Start () {
		CurrentWeapon = gameObject.tag;
            //bulletSpawn = gameObject.GetComponentInChildren<Transform> ();
            GunSound = GetComponentInParent<AudioSource>(); //Assign audiosource at start
            lineOfSight = GetComponent<LineRenderer>();
            shotCount = 0;

			pellets = new List<Quaternion> (pelletCount); //Assign your new list of pellets and loop through the number of pellets to spawn
			for (int i = 0; i < pelletCount; i++) {
				pellets.Add (Quaternion.Euler (Vector3.zero));
			}
	}

    //Choose weapon to fire from switch statements from it's selected string (the weapon's tag)
    public void FireWeapon(string type)
    {
		type = CurrentWeapon;
        switch (type)
        {
		case shotgun:
                    //Set all your values for the specified weapon
                    fireRate = 0.75f;
                    ammoClip = 8;
                    ammoMax = 48;
                    damage = 35;
                //Create shot spread
                    if (Input.GetMouseButton(0)&&Time.time> nextFire && ammo!=0){
                    nextFire = Time.time + fireRate;
				    Shotgun (ammo,ammoClip);//Call the method for the selected gun
                        ammo -= 1;
                        shotCount++;
                        if (shotCount>=ammoClip) { //or if player presses reload button...
					reload (type);
				}
			}
                    //lather, rinse, repeat for all weapons
                break;
            case revolver:
                fireRate = 0.35f;
                ammoClip = 7;
                ammoMax = 35;
                damage = 20;

                    if (Input.GetMouseButton(0) && Time.time > nextFire && ammo != 0)
                    {
                        nextFire = Time.time + fireRate;
                        Revolver(ammo, ammoClip);
                        ammo -= 1;
                        shotCount++;
                        if (shotCount >=ammoClip)
                        {
                            reload(type);
                        }
                    }

                    break;
            case rifle:
                fireRate = 0.1f;
                ammoClip = 30;
                ammoMax = 210;
                damage = 8;

                    if (Input.GetMouseButton(0) && Time.time > nextFire && ammo != 0)
                    {
                        nextFire = Time.time + fireRate;
                        Rifle(ammo, ammoClip);
                        ammo -= 1;
                        shotCount++;
                        if (shotCount >= ammoClip)
                        {
                            reload(type);
                        }
                    }

                    break;
            case tommygun:
                fireRate = 0.18f;
                ammoClip = 50;
                ammoMax = 300;
                damage = 6;

                    if (Input.GetMouseButton(0) && Time.time > nextFire && ammo != 0)
                    {
                        nextFire = Time.time + fireRate;
                        TommyGun(ammo, ammoClip);
                        ammo -= 1;
                        shotCount++;
                        if (shotCount >= ammoClip)
                        {
                            reload(type);
                        }
                    }

                    break;
            case railgun:
                fireRate = 1.2f;
                ammoClip = 4;
                ammoMax = 24;
                damage = 50;

                    if (Input.GetMouseButton(0) && Time.time > nextFire && ammo != 0)
                    {
                        nextFire = Time.time + fireRate;
                        Railgun(ammo, ammoClip);
                        ammo -= 1;
                        shotCount++;
                        if (shotCount >= ammoClip)
                        {
                            reload(type);
                        }
                    }
                    break;
            default:
                fireRate = 0.5f;
                ammo = 0;
                ammoClip = 0;
                ammoMax = 0;
                damage = 5;

                break;
        }

    }

    //Gun type methods for fireing and ammo amounts 
	void Railgun(int ammo, int clip) {
            if (ammo > 0)
            {
                var shot = (GameObject)Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
                shot.GetComponent<Rigidbody>().velocity = shot.transform.forward * bulletSpeed;


                //play sound
                if (RAILGUN[0] != null)
                {
                    //play sound

                }

                // Destroy the bullet after 2 seconds
                Destroy(shot, 2.0f);
            }
        }

        void Rifle(int ammo, int clip) {
            if (ammo > 0)
            {
                var shot = (GameObject)Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
                shot.GetComponent<Rigidbody>().velocity = shot.transform.forward * bulletSpeed;


                //play sound
                if (RIFLE[0] != null)
                {
                    //play sound

                }

                // Destroy the bullet after 2 seconds
                Destroy(shot, 2.0f);
            
        }
    }
	void Revolver(int ammo, int clip) {
            if (ammo > 0)
            {
                var shot = (GameObject)Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
                shot.GetComponent<Rigidbody>().velocity = shot.transform.forward * bulletSpeed;     

                //play sound
                if (REVOLVER[0] != null)
                {
               
                  GunSound.PlayOneShot(REVOLVER[0], 0.85f);
                  

                }

                // Destroy the bullet after 2 seconds
                Destroy(shot, 2.0f);
            
        }
    }

	void Shotgun(int ammo, int clip) {
            //Make the shot spread
			int i=0;
            //Do shotgun stuff for this section
            if (ammo > 0) { 
				foreach (Quaternion quat in pellets) {
					var shot = (GameObject)Instantiate (bullet, bulletSpawn.position, bulletSpawn.rotation);
					pellets [i] = Random.rotation;
					shot.transform.rotation = Quaternion.RotateTowards (shot.transform.rotation, pellets [i], spreadAgle); //Make sure the pellet prefab itself is set to the pellet Layer in the inspector
					shot.GetComponent<Rigidbody> ().velocity = shot.transform.forward * bulletSpeed;
					i++;
					Destroy(shot, 2.0f);
				}
        //play sound
				if (SHOTGUN [0] != null) {
					//play sound

				}

    }
	}

	void TommyGun(int ammo, int clip) {
            if (ammo > 0)
            {
                var shot = (GameObject)Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
                shot.GetComponent<Rigidbody>().velocity = shot.transform.forward * bulletSpeed;


                //play sound
                if (TOMMYGUN[0] != null)
                {
                    //play sound

                }

                // Destroy the bullet after 2 seconds
                Destroy(shot, 2.0f);
            }

        }

    void reload(string type)
    {
        //play animation and reset ammo to full or += ammount picked up
        switch (type) {
            case shotgun:
                    //play reload animation and sound
                    //Add bullets from total ammo into clip
                      shotCount = 0;
				
                break;
            case revolver:
                    //play reload animation and sound
                    shotCount = 0;
                    break;
            case rifle:
                    //play reload animation and sound
                    shotCount = 0;
                    break;
            case tommygun:
                    //play reload animation and sound
                    shotCount = 0;
                    break;
            case railgun:
                    //play reload animation and sound
                    shotCount = 0;
                    break;
            default:
                    //play reload animation and sound
                    shotCount = 0;
                    break;
        }
    }

        // Update is called once per frame
        void Update () {
            if (ammo > ammoMax) ammo = ammoMax;
        }
}
}
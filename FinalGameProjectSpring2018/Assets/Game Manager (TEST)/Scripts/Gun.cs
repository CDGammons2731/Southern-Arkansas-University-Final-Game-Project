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
    //public Animator gunAnim;
   
    //Weapon Types
    [Header("Weapon Types")]
    public const string shotgun = "shotgun";
    public const string revolver = "revolver";
    public const string rifle = "rifle";
    public const string tommygun = "tommygun"; //Not used for the moment
    public const string raygun = "raygun";
    public const string railgun = "railgun";

	public new string CurrentWeapon; //The tag attatched to the gameobjct;
    
    [Header("Gun Sounds")]
    //Audio clips:  0-fire, 1-reload, 2-any extra audio
    public AudioClip[] SHOTGUN = new AudioClip[2];
    public AudioClip[] REVOLVER = new AudioClip[2];
    public AudioClip[] RIFLE = new AudioClip[2];
    public AudioClip[] RAYGUN = new AudioClip[2];
    public AudioClip[] RAILGUN = new AudioClip[2];
    public AudioClip Empty;

    //The source that the audio clips can be played by
    public AudioSource GunSound;

    //Check to see if weapon is equpiied
    public bool equipped;
    public bool canShoot;

        //Just to allow the shots to dissapear if they hit anything
        GameObject shot;

    //All your firearm needs...
    [Header("Gun Info")]
    public int ammo;
    public int ammoClip;
    public int ammoMax;
	public int currentAmmo; //keep track of current ammo
    public int damage; //per bullet @TODO: assign damage later
    public int rangeMult =2; //multiply damage for closer range? TBA
    public int bulletSpeed = 200;
    public float reloadRate;
    public float fireRate;
    public float nextFire;
    public int shotCount; //keep track of bullets shot, implement for reloading and for UI purposes
    public int AmmoUpdate; //For fixed ammo updates

        //For shotgun
    public float spreadAgle;
	public int pelletCount;
	List<Quaternion> pellets; //For the shotgun, make a list of quaternions which will be spawn positions for pellets
	
    private LineRenderer lineOfSight; //@TODO: testing, raycasts 
    RaycastHit Ray_Hit; //@Testing---
    public Camera Player_Cam; //More @Testing
    Vector3 rayOrigin;

/*Weapon Specs defined in Game Doc:

Revolver- The weapon the player character always starts the game with. Slow firing rate with strong recoil. Starts with 30 ammo.
Damage: 10
Magazine: 6
Ammo Type: Revolver
Reload Speed: 3 seconds

Thompson Assault Rifle- Upgraded Thompson sub- machine gun fitted for higher caliber rounds while maintaining its compact size. High fire rate.
Damage: 6
Magazine: 24
Ammo Type: Ballistic
Reload Speed: 2 seconds

Garand Railgun- A modified M1 Garand with magnetic coils attached to the barrel in order to greatly accelerate the fired round. Shots penetrate up to two enemies, hitting a maximum of three. Slow fire rate.
Damage: 12
Magazine: 8
Ammo Type: Ballistic
Reload Speed: 4 seconds

Sonic Shotgun- A super weapon that fires hyper resonating sound waves which impart large amounts of kinetic energy to multiple targets at a short range. Cannot get critical damage. Medium fire rate.
Damage: 15
Magazine:6
Ammo Type: Energy Cell
Reload Speed: 4 seconds

Raygun- A super weapon that fires super-heated jets of plasma which destroys molecular bonds. Medium Fire rate.
Damage:10
Magazine:12
Ammo type: Energy Cell
Reload Speed: 2 seconds

*/




        // Use this for initialization
        void Start () {
		    CurrentWeapon = this.gameObject.tag;
            GunSound = GetComponentInParent<AudioSource>(); //Assign audiosource at start
            lineOfSight = GetComponent<LineRenderer>();
            shotCount = 0;


			pellets = new List<Quaternion> (pelletCount); //Assign your new list of pellets and loop through the number of pellets to spawn
			for (int i = 0; i < pelletCount; i++) {
				pellets.Add (Quaternion.Euler (Vector3.zero));
			}
           //gunAnim = GetComponent<Animator>();
           canShoot = true; //Just to start off able at all times
           Player_Cam=GetComponentInParent<Camera>(); //@Testing---
        }

    //Choose weapon to fire from switch statements from it's selected string (the weapon's tag)
    public void FireWeapon(string type)
    {
		type = CurrentWeapon;
        bulletSpeed = 200;
           
        switch (type)
        {
		case shotgun:
                    //Set all your values for the specified weapon
                    fireRate = 0.75f;
                    ammoClip = 6;
                    ammoMax = 48;
                    damage = 2; //or 1.5
                    reloadRate = 4;

                //Create shot spread
                    if (Input.GetMouseButton(0)&&Time.time> nextFire && ammo!=0 && canShoot==true){
                    nextFire = Time.time + fireRate;
				    Shotgun (ammo,ammoClip);//Call the method for the selected gun
                        ammo -= 1;
                        shotCount++;
                        currentAmmo -= 1;
					if (currentAmmo<=0) { 
					reload (type);
				}

                    }
                    //lather, rinse, repeat for all weapons
                break;
            case revolver:
                fireRate = 0.35f;
                ammoClip = 6;
                ammoMax = 48;
                damage = 10;
                reloadRate = 3;


                    if (Input.GetMouseButton(0) && Time.time > nextFire && ammo != 0 && canShoot == true)
                    {
                        nextFire = Time.time + fireRate;
                        Revolver(ammo, ammoClip);
                        ammo -= 1;
                        shotCount++;
                        currentAmmo -= 1;
					if (currentAmmo<=0)
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
                reloadRate = 2;

                    if (Input.GetMouseButton(0) && Time.time > nextFire && ammo != 0 && canShoot == true)
                    {
                        nextFire = Time.time + fireRate;
                        Rifle(ammo, ammoClip);
                        ammo -= 1;
                        shotCount++;
                        currentAmmo -= 1;
					if (currentAmmo<=0)
                        {
                            reload(type);
                        }
                    }

                    break;
            case raygun:
                fireRate = 1.0f;
                ammoClip = 12;
                ammoMax = 36;
                damage = 6;
                reloadRate = 2;

                    if (Input.GetMouseButton(0) && Time.time > nextFire && ammo != 0 && canShoot == true)
                    {
                        nextFire = Time.time + fireRate;
                        RayGun(ammo, ammoClip);
                        ammo -= 1;
                        shotCount++;
                        currentAmmo -= 1;
					if (currentAmmo<=0)
                        {
                            reload(type);
                        }
                    }

                    break;
            case railgun:
                fireRate = 1.25f;
                ammoClip = 8;
                ammoMax = 32;
                damage = 12;
                reloadRate = 4;

                    if (Input.GetMouseButton(0) && Time.time > nextFire && ammo != 0 && canShoot == true)
                    {
                        nextFire = Time.time + fireRate;
                        Railgun(ammo, ammoClip);
                        ammo -= 1;
                        shotCount++;
                        currentAmmo -= 1;
					if (currentAmmo<=0)
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
                reloadRate = 2;

                    break;
        }
        

    }
        //Just to make sure your pullets are destroyed
        private void OnCollisionEnter(Collision collision)
        {
            if(shot!=null) Destroy(shot);
        }


        //Gun type methods for fireing and ammo amounts 
        void Railgun(int ammo, int clip) {
            if (ammo > 0)
            {
                shot = (GameObject)Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
                shot.GetComponent<Rigidbody>().velocity = shot.transform.forward * bulletSpeed;



                //play sound
                if (RAILGUN[0] != null)
                {
                    GunSound.PlayOneShot(RAILGUN[0], 0.85f);

                }

                // Destroy the bullet after 2 seconds
                Destroy(shot, 0.35f);
                
            }

            else {
                if (Empty != null) {
                    GunSound.PlayOneShot(Empty, 0.85f);
                }
            }

        }

        void Rifle(int ammo, int clip) {
            if (ammo > 0)
            {
                shot = (GameObject)Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
                shot.GetComponent<Rigidbody>().velocity = shot.transform.forward * bulletSpeed;


                //play sound
                if (RIFLE[0] != null)
                {
                    GunSound.PlayOneShot(RIFLE[0], 0.85f);

                }

                // Destroy the bullet after 2 seconds
                Destroy(shot, 0.35f);
                

            }
            else
            {
                if (Empty != null)
                {
                    GunSound.PlayOneShot(Empty, 0.85f);
                }
            }

        }
	void Revolver(int ammo, int clip) {
            if (ammo > 0)
            {
                shot = (GameObject)Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
                shot.GetComponent<Rigidbody>().velocity = shot.transform.forward * bulletSpeed;     
                //play sound
                if (REVOLVER[0] != null)
                {
               
                  GunSound.PlayOneShot(REVOLVER[0], 0.85f);
                  

                }

                // Destroy the bullet after 2 seconds
                Destroy(shot, 0.35f);
            
        }
            else
            {
                if (Empty != null)
                {
                    GunSound.PlayOneShot(Empty, 0.85f);
                }
            }
        }

	void Shotgun(int ammo, int clip) {
            //Make the shot spread
			int i=0;
            //Do shotgun stuff for this section
            if (ammo > 0)
            {
                foreach (Quaternion quat in pellets)
                {
                    shot = (GameObject)Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
                    pellets[i] = Random.rotation;
                    shot.transform.rotation = Quaternion.RotateTowards(shot.transform.rotation, pellets[i], spreadAgle); //Make sure the pellet prefab itself is set to the pellet Layer in the inspector
                    shot.GetComponent<Rigidbody>().velocity = shot.transform.forward * bulletSpeed;
                    i++;
                    Destroy(shot, 0.35f);
                }
                //play sound
                if (SHOTGUN[0] != null)
                {
                    GunSound.PlayOneShot(SHOTGUN[0], 0.85f);

                }
            }
    
        }

	void RayGun(int ammo, int clip) {
            if (ammo > 0)
            {
                shot = (GameObject)Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
                shot.GetComponent<Rigidbody>().velocity = shot.transform.forward * bulletSpeed;


                //play sound
                if (RAYGUN[0] != null)
                {
                    GunSound.PlayOneShot(RAYGUN[0], 0.85f);

                }

                // Destroy the bullet after .35s seconds
                Destroy(shot, 0.35f);
            }
            else
            {
                if (Empty != null)
                {
                    GunSound.PlayOneShot(Empty, 0.85f);
                }
            }

        }

    void reload(string type)
    {
        //play animation and reset ammo to full or += ammount picked up
        switch (type) {
			case shotgun:
                    //play reload animation and sound
                    //Add bullets from total ammo into clip
				if (currentAmmo < ammoClip && ammo > ammoClip) {
					if (SHOTGUN [1] != null) {
						GunSound.PlayOneShot (SHOTGUN [1], 0.85f);
					}
					StartCoroutine (ReloadWaiting (reloadRate));
					currentAmmo = (ammoClip - currentAmmo) + currentAmmo;

				}
					
                break;
			case revolver:
                    //play reload animation and sound
				if (currentAmmo < ammoClip && ammo > ammoClip) {
					if (REVOLVER [1] != null) {
						GunSound.PlayOneShot (REVOLVER [1], 0.85f);
					}
					StartCoroutine (ReloadWaiting (reloadRate));
					currentAmmo = (ammoClip - currentAmmo) + currentAmmo;

				}
				
                    break;
            case rifle:
                    //play reload animation and sound
				if (currentAmmo < ammoClip && ammo > ammoClip) {
					if (RIFLE [1] != null) {
						GunSound.PlayOneShot (RIFLE [1], 0.85f);
					}
					StartCoroutine (ReloadWaiting (reloadRate));
					currentAmmo = (ammoClip - currentAmmo) + currentAmmo;

				}
                    break;
            case raygun:
                    //play reload animation and sound
				if (currentAmmo < ammoClip && ammo > ammoClip) {
					if (RAYGUN[1] != null) {
						GunSound.PlayOneShot (RAYGUN[1], 0.85f);
					}
					StartCoroutine (ReloadWaiting (reloadRate));
					currentAmmo = (ammoClip - currentAmmo) + currentAmmo;

				}
                    break;
            case railgun:
				if (currentAmmo < ammoClip && ammo > ammoClip) {
					if (RAILGUN [1] != null) {
						GunSound.PlayOneShot (RAILGUN [1], 0.85f);
					}
					StartCoroutine (ReloadWaiting (reloadRate));
					currentAmmo = (ammoClip - currentAmmo) + currentAmmo;

				}
                    //play reload animation and sound
                    shotCount = 0;
                    break;
            default:
                    //play reload animation and sound
                    shotCount = 0;
                    break;
        }
            //canShoot = true;
    }

		IEnumerator ReloadWaiting(float reloadTime){
            reloadTime = reloadRate;
            canShoot = false;
			yield return new WaitForSeconds (reloadRate);
            shotCount = 0;
            canShoot = true;
		}    
        // Update is called once per frame
        void Update () {

            if (ammo > ammoMax) ammo = ammoMax;

                if (currentAmmo <= 0 && ammo > ammoClip)
                {
                    //currentAmmo = ammoClip;
                    AmmoUpdate = ammo;
                }
                if (currentAmmo <= 0 && ammo < ammoClip)
                {
                    currentAmmo = ammo;
                }

            if (currentAmmo <=0 && ammo <=0)
            {
                currentAmmo = 0;
                AmmoUpdate = ammoClip;
            }

            if (ammo < ammoClip) {
                AmmoUpdate = ammoClip;
            }

			if (currentAmmo == 0 || currentAmmo==ammoClip) {
				AmmoUpdate = ammo;
            }


            if (Input.GetKeyDown(KeyCode.R) && equipped == true) {
                reload (weapon.tag);
               
			}

            //More RayCast Testing
           
            
        }
}
}
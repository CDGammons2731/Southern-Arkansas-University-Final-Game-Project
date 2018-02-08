using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using GAMEMANAGER;
using GUN;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    public GameManager GM;
    public Camera PlayerCam;

    [Header("Player Sounds")]
    public AudioClip PlayerHurt;
    public AudioClip PlayerDied;
    public AudioClip Healed;
    public AudioClip HeartBeat;
    public AudioClip Ticking;

    private int health;
    private int armor;
    private int score;
    private string name;
     
	public Vector3 gunOffset;
    public GameObject holdingPosition;
    public GameObject weapon;
	public Gun gun;

    public bool weaponInRange;
	public bool hasWeapon;
    public string currentGun;

	GunTransitions gunTran;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = 0;
        armor = 0;
        score = 0;
        name = "Player";

        weaponInRange = false;
		gunOffset= new Vector3(rb.transform.position.x-.05f,rb.transform.position.y-.5f,rb.transform.position.z+2f); //Delete this later
		gunTran= GetComponentInChildren<GunTransitions>();
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Health"))
        {
            GM.HealthUp();
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Armor"))
        {
            GM.ArmorUp();
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Ammo"))
        {
            gun.ammo +=50;
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Damage"))
        {
            GM.HealthDown();
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("shotgun") ||other.gameObject.CompareTag("revolver") || other.gameObject.CompareTag("rifle") ||other.gameObject.CompareTag("tommygun") || other.gameObject.CompareTag("railgun"))
		{
            gun = other.GetComponent<Gun>();
            weapon = other.gameObject;
            weaponInRange = true;
            GM.yourGun = other.GetComponent<Gun>();
            currentGun = weapon.tag;
            gun.currentAmmo = gun.ammoClip;

        }   
    }

    void OnTriggerExit(Collider other) {
        weaponInRange = false;
        
    }


    void PickUpWeapon(GameObject weapon) {
        //Play animation for weapon pick up
        hasWeapon = true;
		weaponInRange = false;
        if (Input.GetMouseButton(1)){
			gun.FireWeapon (weapon.tag);
		}

	}
	GameObject g;

    void Update()
    {
       

        if (health > 100) health = 100;
        if (weaponInRange == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                PickUpWeapon(weapon);

            }

            if (GM.pickupText != null) { }
            GM.pickupText.text = "Press F to pick up " + currentGun;
       
        }
       

		if(hasWeapon==true){
			g = GameObject.FindGameObjectWithTag (currentGun);
			g.transform.position = holdingPosition.transform.position;
			g.transform.rotation = holdingPosition.transform.rotation;
			gun.FireWeapon (currentGun);

        }

        if (Input.GetKeyDown(KeyCode.B)) {
            hasWeapon = false;
        }

        //if player health <=30 play heart beat

    }
}

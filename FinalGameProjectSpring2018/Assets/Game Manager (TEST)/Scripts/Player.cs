using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility; //Testing HeadBob
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
	public Gun gun;

	bool hasWeapon;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = 0;
        armor = 0;
        score = 0;
        name = "Player";

		gunOffset= new Vector3(rb.transform.position.x-.2f,rb.transform.position.y-.5f,rb.transform.position.z);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Health"))
        {
            GM.playerHealth += 50;
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Armor"))
        {
            GM.playerArmor += 25;
            other.gameObject.SetActive(false);
        }

        /*if (other.gameObject.CompareTag("Score"))
        {
            GM.score+= 300;
            other.gameObject.SetActive(false);
        }*/

        if (other.gameObject.CompareTag("Damage"))
        {
            GM.playerHealth -= 25;
            other.gameObject.SetActive(false);
        }

		if (other.gameObject.CompareTag("shotgun") ||other.gameObject.CompareTag("revolver") || other.gameObject.CompareTag("rifle") ||other.gameObject.CompareTag("tommygun") || other.gameObject.CompareTag("railgun"))
		{
			GameObject weapon = other.gameObject;
			PickUpWeapon (weapon);
			hasWeapon = true;

		}
    }

    void Shoot() { }

	void PickUpWeapon(GameObject weapon) { 
		//Play animation for weapon pick up
		if(Input.GetMouseButton(1)){
			gun.FireWeapon (weapon.tag);
		}

	}
	GameObject g;

    void Update()
    {
        GM.playerHealth = health;
        GM.playerArmor = armor;
        GM.score= score;

        if (health > 100) health = 100;

		if(hasWeapon==true){
			g = GameObject.FindGameObjectWithTag ("shotgun");
			gun.FireWeapon ("shotgun");
			g.transform.position = rb.transform.position+gunOffset;

		}
        //if player health <=30 play heart beat

    }
}

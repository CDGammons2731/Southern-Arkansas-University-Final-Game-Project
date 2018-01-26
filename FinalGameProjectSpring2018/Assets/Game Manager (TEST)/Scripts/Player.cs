using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility; //Testing HeadBob
using GAMEMANAGER;

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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = 0;
        armor = 0;
        score = 0;
        name = "Player";

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
    }

    void Shoot() { }

    void PickUpWeapon() { }


    void Update()
    {
        GM.playerHealth = health;
        GM.playerArmor = armor;
        GM.score= score;

        if (health > 100) health = 100;

        //if player health <=30 play heart beat

    }
}

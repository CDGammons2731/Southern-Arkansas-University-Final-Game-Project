﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using GAMEMANAGER;
using GUN;

namespace PLAYER
{


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
        public AudioSource PlayerSound;

        //Integrating player health for player object to work with game manager
        public int player_health;
        public int score;
        public string player_name;

        public Vector3 gunOffset;
        public GameObject holdingPosition;
        public GameObject weapon;
        public Gun gun;

        public bool weaponInRange;
        public bool hasWeapon;
        public bool isWeapon;
        public string currentGun;

        public GameObject UI;

        GunTransitions gunTran;

        void Start()
        {
            //Need to recognize player body
            rb = GetComponent<Rigidbody>();

            //Start setting player data
            player_health = 100;
            player_name = "Player";
            score = 0;
            //Get your audiosource to play sound 
            PlayerSound = GetComponent<AudioSource>();


            weaponInRange = false;

            gunTran = GetComponentInChildren<GunTransitions>();
            GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();

            UI = GameObject.FindGameObjectWithTag("UI");

        }

        //Work on changing some of this 
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Health"))
            {
                player_health += 50;
                other.gameObject.SetActive(false);
            }
            //Armor will be deleted if not used 
            if (other.gameObject.CompareTag("Armor"))
            {
                GM.ArmorUp();
                other.gameObject.SetActive(false);
            }

            //Later change this to pick up specific types of ammo 
            if (other.gameObject.CompareTag("Ammo"))
            {
                gun.ammo += 50;
                other.gameObject.SetActive(false);
            }
            //Damage testing, change to take damage from bullets, traps, and hits
            if (other.gameObject.CompareTag("Damage"))
            {
                GM.HealthDown();
                other.gameObject.SetActive(false);
            }

            if (other.gameObject.CompareTag("shotgun") || other.gameObject.CompareTag("revolver") || other.gameObject.CompareTag("rifle") || other.gameObject.CompareTag("raygun") || other.gameObject.CompareTag("railgun"))
            {
                weapon = other.gameObject;
                weaponInRange = true;
                UI.GetComponent<hud>().pickUpText.text = ("Press F to pickup " + weapon.tag);

            }
        }

        void OnTriggerExit(Collider other)
        {
            weaponInRange = false;
            UI.GetComponent<hud>().pickUpText.text = ("");

        }


        void PickUpWeapon(GameObject weapon)
        {
            //Play animation for weapon pick up
            hasWeapon = true;
            weaponInRange = false;
            gun.equipped = true;
            if (Input.GetMouseButton(1))
            {
                gun.FireWeapon(weapon.tag);
            }

        }
        GameObject g;

        void Update()
        {
            if (player_health > 100) player_health = 100;

            if (weaponInRange == true)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    currentGun = weapon.tag;
                    gun = weapon.GetComponent<Gun>();
                    GM.yourGun = weapon.GetComponent<Gun>();
                    PickUpWeapon(weapon);

                }

                if (GM.pickupText != null) { }


            }

            if (hasWeapon == true)
            {
                g = GameObject.FindGameObjectWithTag(currentGun);
                g.transform.position = holdingPosition.transform.position;
                g.transform.rotation = holdingPosition.transform.rotation;
                gun.FireWeapon(currentGun);
                // GM.pickupText.text = "";
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                hasWeapon = false;
                gun.equipped = false;
            }

            if (player_health <= 30)
            {
                PlayerSound.clip = HeartBeat;
                PlayerSound.Play();
            }

        }
    }
}

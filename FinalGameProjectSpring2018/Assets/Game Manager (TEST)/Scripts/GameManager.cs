using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using System.IO;
using System;
using GUN;
using PLAYER;


namespace GAMEMANAGER 
{
    /*PLAYER- dectective. Make NewGame() method to enter the players name to create the saved data
     -player spawns at the beginning (Level 1) in an elevator going down with a REVOLVER with 30 ammo
     -Save states happen at the beginning of each level (Automatic)
     -Saved values include health,armor,score,level
     -Multiple saved games allowed, no repeates of the same saved data
     
      MODES: Endless (Till you die) or Escape (15 levels min) 
      -Increase difficulty (Robot abilities increas/ number spawned increases)
      
      Fixed game Timer: after a certain amount of time, security increases
      
      Collectables- Evidence that can be picked up
            *UI referenced* 
      Retain Instantiation of Player at checkpoints/startlevel: RespawnPlayer() method
      
      */

    [Serializable]
    public class GameManager : MonoBehaviour
    {
        [Header("Game Attatchments")]
        public Text stats;
        public static GameManager GAME;
        public GameObject PlayerObj;
        public Camera PlayerCam;
        public Camera DeathCam;
   


        [Header("Game Sounds")]
        public AudioClip ThemeMusic;
        public AudioClip ActionMusic;
        public AudioClip VictoryMusic;
        public AudioClip DeathMusic;
        public AudioClip CreditMusic;
        public AudioSource audiosource;

        //Not going to be used anymore 
        [Header("Player Info")]
        public int playerHealth = 100;
        public int playerArmor = 0;
        public int score = 0;
        public new string defaultName = "Player"; //Default Name
        public new string defualtSaved = "/PlayerInfo.dat"; //Defualt Saved Data
        public new string playerSaved; //To set the new player's data. Data saved will simply be the players name + "_Info.dat"
        bool pickup = false;
		public int evidencePickedUp;
		public int levelsCompleted;

        // Gameplay (TEST)
        [Header("Enemy Info")]
        public int enemyCount;
        //public int[] enemyhealth = { 30, 50, 80, 100, 120, 150, 200, 250, 300 }; //Different health amounts of your enemy

        private float timer; //Game Timer
        private bool escaped; //If the player escapes, next level or win's game, show stats and score

        //Testing GUN
        public Gun yourGun;
        public Text gunText, gunStat, pickupText;

        public int curAmmo;
        public int maxAmmo;

        public GameObject UI;
        public Player player;

        //Level Counter
        public int Level = 0;


        //Other saved info: level, lives, upgrade bool + upgrade type, enemy count / enemies spawned, etc
        void Awake()
        {
            this.enabled = true;
            if (GAME == null)
            {
                DontDestroyOnLoad(gameObject);
                GAME = this;
                
            }
            else if (GAME != this)
            {
                Destroy(gameObject);
            }
            audiosource = GetComponent<AudioSource>();
            UI = GameObject.FindGameObjectWithTag("UI");
            player = GameObject.FindGameObjectWithTag("player").GetComponent<Player>();
            

        }

        private void Start()
        {
          
        }

        void PlayBackground() {
            audiosource.PlayOneShot(ThemeMusic);
        }

        void PlayWinMusic() {
            audiosource.PlayOneShot(VictoryMusic);
        }

        void PlayDeathMusic()
        {
            audiosource.PlayOneShot(DeathMusic);
        }



        [Serializable]
        class PlayerData
        {
            public int health, armor, score, level;
            public int[] evidence = new int[15]; //Evidence that can be collected
            public string name;
            //Make automatic at beginning of level
        }

        

       
        void Update()
        {
            enabled = true;

            if (yourGun.ammoClip < 0)
            {
                UI.GetComponent<hud>().ammoDisplay.text = yourGun.currentAmmo + "/" + yourGun.AmmoUpdate;
            
            }

           
        }


    }
}


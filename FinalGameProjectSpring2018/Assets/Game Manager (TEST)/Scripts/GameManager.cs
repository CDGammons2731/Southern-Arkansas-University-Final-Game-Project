using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using System.IO;
using System;
using GUN;

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
		public InputField NewSaveName; //Use this to allow the user to type their Player Name
        public static GameManager GAME;
        public GameObject PlayerObj;
        public Camera PlayerCam;


        [Header("Game Sounds")]
        public AudioClip ThemeMusic;
        public AudioClip ActionMusic;
        public AudioClip VictoryMusic;
        public AudioClip DeathMusic;
        public AudioClip CreditMusic;
        public AudioSource audiosource;


        [Header("Player Info")]
        public int playerHealth = 100;
        public int playerArmor = 0;
        public int score = 0;
        public new string defaultName = "Player"; //Default Name
        public new string defualtSaved = "/PlayerInfo.dat"; //Defualt Saved Data
        public new string playerSaved; //To set the new player's data. Data saved will simply be the players name + "_Info.dat"
        bool pickup = false;

        // Gameplay (TEST)
        [Header("Enemy Info")]
        public int enemyCount;
        public int[] enemyhealth = { 30, 50, 80, 100, 120, 150, 200, 250, 300 }; //Different health amounts of your enemy

        private float timer; //Game Timer
        private bool escaped; //If the player escapes, next level or win's game, show stats and score

        //Testing GUN
        public Gun yourGun;
        public Text gunText, gunStat, pickupText;



        struct Robot
        {
            public int health;
            public int pointValue;//if needed
            
            //Make type

        }



        //Other saved info: level, lives, upgrade bool + upgrade type, enemy count / enemies spawned, etc
        void Awake()
        {
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
            audiosource.PlayOneShot(ThemeMusic, 0.5f);

            
        }
			

        //----------------------------------------GAME SAVING-------------------------------------------

        [Serializable]
        class PlayerData
        {
            public int health, armor, score, level;
            public int[] evidence = new int[15]; //Evidence that can be collected
            public string name;
            //Make automatic at beginning of level
        }

        public void Save()
        {
           
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/PlayerInfo.dat");
            PlayerData pd = new PlayerData();
            pd.health = playerHealth;
            pd.armor = playerArmor;
            pd.score = score;

            if (name== null) {
                name = defaultName;
            }

            bf.Serialize(file, pd);
            file.Close();
            Debug.Log("Save successful h/a/s: " + playerHealth + " " + playerArmor + " " + score);

			//playerSaved = NewPlayer;

        }

		public void Load() //Just load at Continue Game (Load on Saved Game Selection)
        {
			
            if (File.Exists(Application.persistentDataPath + "/Player_Info.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/Player_Info.dat", FileMode.Open);
                PlayerData pd = (PlayerData)bf.Deserialize(file);
                file.Close();

                playerHealth = pd.health;
                score = pd.score;
                playerArmor = pd.armor;

                Debug.Log("Load Successful h/a/s: " + playerHealth + " " + playerArmor + " " + score);


            }
            else
            {
                Debug.Log("No saved file found");
            }
        }

        //Create ResetData() method / DeleteSavedFile()

        //--------------------------------------------------------------------------------------------------------




        // @TESTING 
        public void HealthUp()
        {
            playerHealth += 20;
        }

        public void HealthDown()
        {
            playerHealth -= 20;
        }
        public void ArmorUp()
        {
            playerArmor += 50;
        }
        public void ScoreUp()
        {
            score += 250;
        }
        public void NameChange()
        {
			
			NewSaveName.enabled = true;
			NewSaveName.textComponent.enabled = true;
			NewSaveName.image.enabled = true;

			string newName;
			newName=NewSaveName.text;
			NewGame (newName);
        }

        public void NewGame(string NewPlayer) {
			
            if (NewPlayer == null)
            {
                NewPlayer = defaultName;
            }

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/"+NewPlayer+"_Info.dat");
            PlayerData pd = new PlayerData();
            pd.health = playerHealth;
            pd.armor = playerArmor;
            pd.score = score;
            //pd.name=name
          
            bf.Serialize(file, pd);
            file.Close();
			playerSaved = NewPlayer;

        }

        private void RespawnPlayer()
        {
            //Respawn player at a position and orientation if they restart
            //reset score and health

        }

        //If the player escapes....
        private void Successs()
        {
            //Do some game winning thing here
        }


        void Update()
        {
          
			if (yourGun.CurrentWeapon != null&& gunText!=null) {
				gunText.text = yourGun.CurrentWeapon;
			}
			if (yourGun.ammoClip != 0) {
				gunStat.text = (yourGun.currentAmmo) + "/" + yourGun.AmmoUpdate;
			}
            stats.text = "Health " + playerHealth + " Armor: " + playerArmor + " Score: " + score;

            if (playerHealth > 100)
            {
                playerHealth = 100;
            }
         
        }


    }
}


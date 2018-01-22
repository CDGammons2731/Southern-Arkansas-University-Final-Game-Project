using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using System.IO;
using System;

[Serializable]
public class GameManager : MonoBehaviour {
    [Header("Game Attatchments")]
    public Text stats;
    public static GameManager GAME;
    public GameObject PlayerObj;
    public Camera PlayerCam;

    [Header("Game Sounds")]
    public AudioClip ThemeMusic;
    public AudioClip ActionMusic;
    public AudioClip VictoryMusic;
    public AudioClip DeathMusic;
    public AudioClip CreditMusic;

    
    [Header("Player Info")]
    public int playerHealth = 100;
    public int playerArmor = 0;
    public int score = 0;
    public new string name = "Player"; //Default Name
    bool pickup = false;

    // Gameplay (TEST)
    [Header("Enemy Info")]
    public int enemyCount;
    public int[] enemyhealth = { 30, 50, 80, 100, 120, 150, 200, 250, 300 }; //Different health amounts of your enemy

    private float timer; //Game Timer
    private bool escaped; //If the player escapes, next level or win's game, show stats and score

    struct Robot {
        public int health;
        public int pointValue;//if needed
 
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
        //Display Stats
    }
		
    //----------------------------------------GAME SAVING-------------------------------------------

    [Serializable]
    class PlayerData
    {
        public int health, armor, score;
        public string name;
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/PlayerInfo.dat");
        PlayerData pd = new PlayerData();
        pd.health = playerHealth;
        pd.armor = playerArmor;
        pd.score = score;

        bf.Serialize(file, pd);
        file.Close();
        Debug.Log("Save successful h/a/s: " + playerHealth + " " + playerArmor + " " + score);
        
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/PlayerInfo.dat", FileMode.Open);
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
	public void HealthUp(){
        playerHealth += 100;
	}
	public void ArmorUp(){
        playerArmor += 50;
	}
	public void ScoreUp(){
		score += 250;
	}
	public void NameChange(){

	}



    private void RespawnPlayer() {
        //Respawn player at a position and orientation if they restart
        //reset score and health
    
    }

    //If the player escapes....
    private void Successs() {
        //Do some game winning thing here
    }



 void Update()
    {
        stats.text = "Health " + playerHealth + " Armor: " + playerArmor + " Score: " + score;

		if (playerHealth > 100) {
			playerHealth = 100;
		}
    }

}


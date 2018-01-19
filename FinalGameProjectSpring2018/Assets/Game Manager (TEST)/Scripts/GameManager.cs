using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using System.IO;
using System;

[Serializable]
public class GameManager : MonoBehaviour {
	public Text stats;
    public static GameManager GAME;
    public GameObject PlayerObj;
    public Camera PlayerCam;
    public AudioClip run, jump, speak, hurt, death;

    //Stats / Info
    public int playerHealth = 100;
    public int playerArmor = 0;
    public int score = 0;
    public new string name = "Player"; //Default Name
    bool pickup = false;
    



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

    [Serializable]
    class PlayerData
    {
        public int health, armor, score;
        public string name;
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

            //name = playerInfo.name;

        }
        else
        {
            Debug.Log("No saved file found");
        }
    }

	// @TESTING -----------------------------------------
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

 void Update()
    {
        stats.text = "Health " + playerHealth + " Armor: " + playerArmor + " Score: " + score;
    }

}


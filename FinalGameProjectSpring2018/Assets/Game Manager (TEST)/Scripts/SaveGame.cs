using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//For saving and serialization
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
namespace SAVE{
[Serializable]
public class SaveGame : MonoBehaviour {

	public static SaveGame SAVE;
	public int health=0,armor=0,score=0;
	public Text display;
	//Other saved info: level, lives, upgrade bool + upgrade type, enemy count / enemies spawned, etc
	void Awake(){
		if (SAVE == null) {
			DontDestroyOnLoad (gameObject);
			SAVE = this;
		}
		else if(SAVE!=this){
			Destroy (gameObject);
		}
		//Display Stats
}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/PlayerInfo.dat");
		PlayerData pd = new PlayerData ();
		pd.health=health;
		pd.armor=armor;
		pd.score=score;

		bf.Serialize(file,pd);
		file.Close ();
			Debug.Log ("Save successful h/a/s: "+ health + " " + armor + " " + score);
	}

	[Serializable]
	class PlayerData{
		public int health, armor, score;
		public string name;
	}

	public void Load(){
			if (File.Exists (Application.persistentDataPath + "/PlayerInfo.dat")) {
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file = File.Open (Application.persistentDataPath + "/PlayerInfo.dat", FileMode.Open);
				PlayerData pd = (PlayerData)bf.Deserialize (file);
				file.Close ();

				health = pd.health;
				score = pd.score;
				armor = pd.armor;

				Debug.Log ("Load Successful h/a/s: " + health + " " + armor + " " + score);

				//name = playerInfo.name;

			}
			else {
				Debug.Log ("No saved file found");
			}
	}
	
		void Update(){
			display.text = "Health " + health + " Armor: " + armor + " Score: " + score;
		}
}
}
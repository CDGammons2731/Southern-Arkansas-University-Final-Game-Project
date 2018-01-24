using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour {
	/*Make a GameObject, attatch a Text Mesh to it
	Adjust Character size (0-1 recommended for testing) and adjust Font Size
	Choose a font
	Add a Box Collider
	*/
	public GAMEMANAGER.GameManager GM;

	void Update () {
		
	}

	void OnMouseOver(){
		//GetComponent<Transform> ().localScale = new Vector3 (1.2f, 1.2f, 1);
		GetComponent<TextMesh> ().color = new Color (0.5f, 0.2f, 0.5f,1);
	}
	void OnMouseExit(){
		GetComponent<Transform> ().localScale = new Vector3 (1, 1, 1);
		GetComponent<TextMesh> ().color = new Color (1, 1, 1,1);
	}

	void OnMouseDown(){
		//GetComponent<TextMesh> ().text = "Your function";
		//Place function
		GM.NameChange();
		GetComponent<TextMesh> ().text =GM.playerSaved+ " Level: 0 " + " Score: " + GM.score + " Health: " + GM.playerHealth + " Armor: "+ GM.playerArmor; //Set a text box next to each to show scores?

	}
}

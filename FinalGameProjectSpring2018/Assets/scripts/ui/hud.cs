using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GAMEMANAGER;
using GUN;

public class hud : MonoBehaviour {
    public GameObject hubScreen;

    public Text timer;
    float countDown=30f;

    //Ammo Display
    Gun ammoAmt;
    public Text ammoDisplay;

    //Player health bar
    GameManager hlth;
    float maxHealth = 100.0f;
    public GameObject needle;
    public float smooth = 2.0f;

	
	// Update is called once per frame
	void Update () {
        countDown-=Time.deltaTime;
        int count=(int)countDown%60;
        timer.text=count.ToString();

        loseHealth();
        if (ammoAmt.ammoClip != 0) {
			ammoDisplay.text= (ammoAmt.currentAmmo) + "/" + ammoAmt.AmmoUpdate;
		}
        
	}

    void loseHealth(){
        float rotationZ = 270.0f * (hlth.playerHealth / maxHealth)-270.0f;
        Quaternion target = Quaternion.Euler(0,0,rotationZ);
        needle.transform.rotation = Quaternion.Slerp(needle.transform.rotation, target, smooth * Time.deltaTime);

        
    }
}

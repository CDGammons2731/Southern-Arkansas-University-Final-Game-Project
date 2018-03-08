using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GAMEMANAGER;
using GUN;


public class hud : MonoBehaviour {
    public GameObject hubScreen;
    public Player playerScript;
    public Text pickUpText;
    public Text yourWeapon;
    GameManager gm;
    public Text timer;
    float countDown=300f;

    //Ammo Display

    public Text ammoDisplay;



    //Player health bar

    float maxHealth = 100.0f;
    public GameObject needle;
    public float smooth = 2.0f;


    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        playerScript = GameObject.FindGameObjectWithTag("player").GetComponent<Player>();
        

    }

    void ShowAmmo() {
        ammoDisplay.text = gm.curAmmo + "/" + gm.maxAmmo;

        //pickUpText.text = gm.pickupText.text;

    }

    // Update is called once per frame
    void Update () {

        countDown-=Time.deltaTime;
        int sec=(int)countDown%60;
        int min = (int)countDown / 60;
        timer.text=min.ToString()+ ":"+sec.ToString();

        loseHealth();

        if (gm.yourGun != null)
        {
            ShowAmmo();
        }

     
            yourWeapon.text = gm.yourGun.CurrentWeapon;
        
    }

    void loseHealth(){
        float rotationZ = 270.0f * (gm.playerHealth / maxHealth)-270.0f;
        Quaternion target = Quaternion.Euler(0,0,rotationZ);
        needle.transform.rotation = Quaternion.Slerp(needle.transform.rotation, target, smooth * Time.deltaTime);

        
    }
}

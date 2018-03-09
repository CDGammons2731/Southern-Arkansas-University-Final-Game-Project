using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GAMEMANAGER;
using GUN;


public class hud : MonoBehaviour {
    public Text timer;
    float countDown=300f;

    //Ammo Display

    public Player playerScript;
    public Text pickUpText;
    public Text yourWeapon;
    GameManager gm;
    public Text ammoDisplay;

    //Items Player Collect
    public Text keyText;
    public Text evidText;

    //Player health bar
    float maxHealth = 100.0f;
    public GameObject needle;
    public float smooth = 2.0f;

    //Gun Icon
    public Image gunIcon;
    public Sprite[] icons;


    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        playerScript = GameObject.FindGameObjectWithTag("player").GetComponent<Player>();


        

    }

    void ShowAmmo() {
        ammoDisplay.text = gm.curAmmo + "/" + gm.maxAmmo;

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


        if(gm.yourGun.CurrentWeapon=="revolver"){
            gunIcon.sprite = icons[0];
        }
        if (gm.yourGun.CurrentWeapon == "railgun")
        {
            gunIcon.sprite = icons[1];
        }
        if (gm.yourGun.CurrentWeapon == "tommygun")
        {
            gunIcon.sprite = icons[2];
        }
        if (gm.yourGun.CurrentWeapon == "shotgun")
        {
            gunIcon.sprite = icons[3];
        }




        
    }

    void loseHealth(){
        float rotationZ = 270.0f * (gm.playerHealth / maxHealth)-270.0f;
        Quaternion target = Quaternion.Euler(0,0,rotationZ);
        needle.transform.rotation = Quaternion.Slerp(needle.transform.rotation, target, smooth * Time.deltaTime);

        
    }
}

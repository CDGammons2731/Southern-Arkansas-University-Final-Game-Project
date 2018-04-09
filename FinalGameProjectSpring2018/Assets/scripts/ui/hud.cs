using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GAMEMANAGER;
using GUN;
using PLAYER;


public class hud : MonoBehaviour {
    public Text timer;
    float count=0f;
    float maxCount = 3600f;
    public GameObject clockNeedle;

    //Ammo Display
    Player playerScript;
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

    //GameOver
    public GameObject gameOver;

    //Gun Icon
    public Image gunIcon;
    public Sprite[] icons;

    //Evidence Text
    string[] txt = new string[16];
    public Text evidDes;




    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();

        //playerScript = GameObject.FindGameObjectWithTag("player").GetComponent<Player>();
        

        txt[1] = "Public Announcement To All Staff... As I have been so gracious and kind to allow you to keep your jobs in my company, you understand there will be some changes in how we run things. Firstly, my son Jonathan, is now the new CEO because the Board of Directors and I have an “understanding” you might say. Secondly, from hence forth, anytime you refer to myself, I will be called Godfather. Thirdly, if you value your life and your family’s lives, you will do anything I or my son says, trust me, my sons robots can be quite dangerous if you disobey my orders. Your managers have been briefed quite clearly on your new roles in the company. Now if you’ll excuse me, I must go and have lunch with President Woodrow, I do not like how he is running my war, Your Godfather,Maddox Richter";
        txt[2] = "Public Announcement To All Staff... This is your new CEO Johnathan, I have reviewed the company policy and have decided you all don’t need lunch breaks, you need more work.These guns won’t manufacture themselves you know. Therefore, starting today, your expected daily quota is tripled and there will no longer be any lunch breaks.Also, those who don’t meet their quota will be whacked by my robots on the head till they either die or are put in a coma. Honestly, you people just don’t understand how hard it is to be the CEO of a company. That is all for now, Johnathan Richter";
        txt[3] = "Public Announcement To All Staff... This is your CEO Johnathan, YOU IDIOTS!These guns are a mess!The customers are demanding their money back. I have several “complaints” on my desk of sleep deprivation and I quite agree there needs to be time change. From now on, the company stays open 24 / 7 and the person to quit over being sleepy will be killed on spot.Man, you people have it easy compared to me, you should be thanking me, I had to type this letter out myself because my secretary and I had a disagreement and she found herself out the window on a one - way trip. That is all for now, Johnathan Richter";
        txt[4] = "Father, these buffoons keep messing up my company. Some guy named Steve even had the nerve to die from exhaustion yesterday, we had to throw his body in the dumpster and everything. How ungrateful can you get? They keep complaining that our supplier of gun parts is lousy but Big Al’s Salvage Gun parts Shop is the cheapest anywhere around and I like cheap. I wish you would come to the company and help me tighten the noose, Your son, Johnathan Richter";
        txt[5] = "To my son whom I love, suck it up buttercup and tighten the noose yourself or be cut off from the money. Also, for goodness sake, don’t botch up delivering the guns again, the last shipment didn’t arrive, and I have had “take care” of a few customers who threatened to turn us in. Make sure the drugs are hidden securely in the vehicles.Now if you’ll excuse me, I must go and have lunch with George Joseph Smith, I know three women that need to go away permanently. The Godfather and your personal father, Maddox Richter";
        txt[6] = "Public Announcement To All Staff... This is your CEO Johnathan, NO ONE IS ALLOWED TO USE THE RESTROOMS WHILE WORKING! Do you think I pay you 3 bucks an hour to go on bathroom breaks? As of today, all bathrooms will be removed from the facility, and anyone I catch using the restroom in public will be shot on spot. Honestly, you people just don’t understand the strain it puts on a CEO to run his company.I don’t even get to go out and live my life because I’m stuck here signing your timesheets.I’m tempted to have my robots eliminate a third of you just, so I don’t have to strain my hand muscles so much.I mean, to think that some of you believe I should pay you worthless mongrels the minimum wage of FIVE dollars? You all must be out of your minds. That is all for now, Johnathan Richter";
        txt[7] = "Public Announcement To All Staff... This is your CEO Johnathan, the kitchens menu is being reduced to two items, stale bread and water to save on costs. After you finish your quota for the day, you may collect your one slice of stale bread and your tablespoon of water and go take your fifteen - minutes of time off to spend with family. No one can ever say I didn’t care for family time as CEO.Failure to return after fifteen minutes will result in the permanent disposal of you and your family. That is all for now, Johnathan Richter";
        txt[8] = "To my son whom I love, you’re stupid. I give you one simple task, I say, don’t botch up the guns delivery job. You know how many customers I’ve had to kill and take their money because you didn’t get them their shipment in time, sixteen.I love you, do better or I’ll cut you off from the money. Now if you’ll excuse me, I must have lunch with Howell Hansel, he owes me money for that film, the Million Dollar Mystery, but the only mystery to be discovered is why that million dollars is not in my pocket. The Godfather and your personal father, Maddox Richter";
        txt[9] = "Public Announcement To All Staff... This is your CEO Johnathan, due the number of employees who fail to comply with living and working, everyone’s pay is reduced by half.Also, THE GUNS ARE NOT SUITABLE ENOUGH!You must all redo every single gun of your quota before you get your fifteen minutes of family time. Remember to dispose of all evidence so we don’t leave unnecessary traces behind. That is all for now, Johnathan Richter";
        txt[10] = "Public Announcement To All Staff... This is your CEO Johnathan, I want all documented evidence of the guns to be hidden immediately, I just got an anonymous email from a man who claims the police believe we have suspicious activity in the building and will be coming to investigate. When I find out who sent me this email, I’m going to make you wish you were never born. That is all for now, Johnathan Richter";
        txt[11] = "To my son whom I love, I’ve heard about the disaster of the police scandal, it will be dealt with, we have the best lawyer money can buy in the family. This thing will be swept under the rug, the chief of police and I are meeting for lunch, and he owes me a favor for taking mercy on him one time and not killing his wife, The Godfather and your personal father, Maddox Richter";
        txt[12] = "Father, I have identified the people responsible for the email, five board of director members, Sammy McGillicutty, Greg Abernathy, Amanda Lowes, Jennifer Doubts, and Fredwick Goober.They collectively sent pieces of evidence and traces to the police, trying to rat us out. What would you have me do? Shall I have the robots go and kill them or do you have a more “special” fate in mind? Your son, Johnathan Richter";
        txt[13] = "To my son whom I love, don’t bother, I’ve already arranged for these deadbeat traitors and my self to have lunch. Just sit tight son, hide all the evidence, the police chief was compliant, and he won’t be bothering us again.However, because of all the suspicion, it is best if you guard the evidence of the guns with your life.If someone finds anything that might indicate that you or I are involved, then I will cut you off the money and let the police take you to jail for all of it, The Godfather and your personal father, Maddox Richter";
        txt[14] = "Father, YOU CAN’T DO THAT! WE ARE IN THIS TOGETHER! The evidence is hidden, my robots are on high alert, but know this father!You gave me this dumb job and if you dare so much as think of letting me take the fall for this company or these guns, and I’ll drag you down with me.Mark my words, do not betray me father! Your son, Johnathan Richter";
        txt[15] = "To my son whom I love, your stupid. You have no idea how to run a company, you are a disgrace to the family, to your Mama, and especially your Grandmama, may she rest in peace.After that last letter, I am cutting you off on the money and when you die I will spit on your grave.If you get caught then it is on your head, I am done covering up for your lousy mistakes.I would offer you to come to lunch with me but your no longer worth my time, The Godfather and your personal father, Maddox Richter";
    }

    // Update is called once per frame
    void Update () {
        //Aaron: you're telling it to set the player script if a player script is already there. THat's why the Hud stopped

        /*if (playerScript != null)
        {
            playerScript = GameObject.FindGameObjectWithTag("player").GetComponent<Player>();
        }*/

        //Try this instead
        if (gm != null) {
            playerScript = GameObject.FindGameObjectWithTag("player").GetComponent<Player>();
        }


        //prints out the timer
        count+=Time.deltaTime;
        //int sec=(int)countDown%60;
        //int min = (int)countDown / 60;
        //timer.text=min.ToString()+ ":"+sec.ToString();
        float rotZ = -235.0f *count/maxCount+235.0f;
        Quaternion tar = Quaternion.Euler(0, 0, rotZ);
        clockNeedle.transform.rotation = Quaternion.Slerp(clockNeedle.transform.rotation, tar, smooth * Time.deltaTime);


        if(count <= 0){
            count = 300f;
        }

        if (playerScript != null)
        {
            //updates health meter
            loseHealth();


            //evidence display
            evidText.text = playerScript.Evidence.ToString();


            //Pop up GameOver
            if (playerScript.player_health <= 0)
            {
                gameOver.SetActive(true);
            }
        }


        /*for (int i = playerScript.Evidence; i < 15; i++){
            evidDes.text = txt[i];  
        }*/


        //Updates Ammo player has
        if (gm.yourGun != null)
        {
            ShowAmmo();

            //tells which gun you have
            yourWeapon.text = gm.yourGun.CurrentWeapon;



            //figures which icon to use
            if (gm.yourGun.CurrentWeapon == "revolver")
            {
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
        


    }

    //Health
    void loseHealth(){
        //rotating needle to represent health
        if (playerScript != null)
        {
            float rotationZ = 270.0f * ((float)playerScript.player_health / maxHealth) - 270.0f;
            Quaternion target = Quaternion.Euler(0, 0, rotationZ);
            needle.transform.rotation = Quaternion.Slerp(needle.transform.rotation, target, smooth * Time.deltaTime);
        }
    }


    //Ammo
    void ShowAmmo()
    {
       ammoDisplay.text = gm.yourGun.currentAmmo+ "/" + gm.yourGun.AmmoUpdate;

    }
}

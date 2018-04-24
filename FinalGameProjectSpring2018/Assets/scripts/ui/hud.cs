using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GAMEMANAGER;
using GUN;
using PLAYER;
using UnityStandardAssets.Characters.FirstPerson;


public class hud : MonoBehaviour {
    public Text timer;
    float count=0f;
    float maxCount = 3600f;
    public GameObject clockNeedle;

    //Ammo Display
    Player playerScript;
    public Text pickUpText;
    public Text yourWeapon;
    public Text yourWeapon2;
    public Text yourWeapon3;
    GameManager gm;
    public Text ammoDisplay;



    //Player health bar
    float maxHealth = 100.0f;
    public GameObject needle;
    public float smooth = 2.0f;

    //GameOver
    public GameObject gameOver;

    //Gun Display
    public Image gunIcon;
    public Sprite[] icons;
    public int currentWeapon;
    public GameObject file1;
    public GameObject file2;
    public GameObject file3;


    //Items Player Collect
    //Evidence
    public Text evidText;
    string[] txt = new string[16];
    public Text evidDes;
    public GameObject evidPopUp;
    public bool evid;

    //Key
    public Text keyText;
    public Text keyTxt;
    public bool key;
    public int keyAmt=0;

    //unlock Mouse
    FirstPersonController curLock;
    bool canLock=false;
    bool lockCursor = true;

    //Multi Endings
    string[] end=new string[3];
    public int endingsNum;
    public Text endText;
    public GameObject endings;
    public bool youEnd=false;


   




    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();

        //playerScript = GameObject.FindGameObjectWithTag("player").GetComponent<Player>();
        
        //Evidence text
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

        end[0] = "NEWS HEADLINES: Crazy Detective Spreading Ludacris Lies Of Mafia Uprising." +
            " Local Detective was found trespassing on private property claiming to be searching for evidence linking famous Millionaire Maddox Richter and his son Johnathan Richter to a Mafia Cult.Police apprehended the detective as he was attempting to escape Apollyon Co.claiming he was being chased down by killer robots while waving a gun in Chief of Police officer Nicholas Cisneros’s face. After six months of long grueling trials, the jury determined that the detective was crazy and found guilty of manslaughter of the dead bodies found in Apollyon Co. Mayor Joey Terrell issued out a formal apology calling the Detective quote, “disgraceful and worthless”, and gave both Maddox Richter and his son Johnathan keys to the city. The Detective has been committed to Irrenhaus Sanitorium, where he will spend the remainder of his life with people of similar mental illnesses. " +
            "THE END";
        end[1] = "NEWS HEADLINES: Maddox Richter And Son Apart Of Mafia Uprising? " +
            "Local Detective has brought forward documents to police which may link famous Millionaire Maddox Richter and his son Johnathan to a Mafia Cult.After reviewing the evidence, police apprehended Maddox and Johnathan and brought them in for questioning.Afterwards the state set a court date for the investigation into the legality of practices pertaining to Apollyon Co.After reviewing all the evidence, the Court found there was just not enough evidence to convict Maddox and Johnathan Richter of any crimes related to the mafia. Mayor Joey Terrell had this to say quote, “alarming yet dissatisfying”, and proceeded to ban the Detective from being a detective within city limits. Former Detective now works as a television producer in Hollywood, directing a show about a detective who fights killer robots in the Mafia. " +
            "THE END";
        end[2] = "NEWS HEADLINES: New Owners of Apollyon Co. Secretly Leading Mafia Uprising!" +
            "Local Detective has brought forward substantial evidence linking famous Millionaire Maddox Richter and his son Johnathan to serious Mafia related crimes concerning illegal weapons manufacturing.Maddox Richter attempted to gun down the police but was accidentally shot in the head by a nearby malfunctioning robot.Johnathan Richter has received the death penalty for manslaughter of the of his employees and is now in prison awaiting death.When the Detective was called into Mayor Joey Terrell’s office, the Mayor said the following quote, “Incredible but intense” and proceeded to give the detective a key to the city.Mayor Joey liked the detective so much, he fired Chief of Police Nicholas Cisneros and gave the job to the Detective. Nicholas Cisneros has since gotten a job as a computer game designer and is in the process of designing a game where mafia robots kill a detective for endless hours of “kid friendly” fun. " +
            "THE END";
    }

    // Update is called once per frame
    void Update () {
       
        if (playerScript==null) {
            playerScript = GameObject.FindGameObjectWithTag("player").GetComponent<Player>();
        }
        if (playerScript != null)
        {
            playerScript = GameObject.FindGameObjectWithTag("player").GetComponent<Player>();
        }
        //unlock Mouse
        if (curLock == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("player");
            if (playerObject != null)
            {
                curLock = playerObject.GetComponent<FirstPersonController>();
            }
        }

        //prints out the timer
        count+=Time.deltaTime;
        float rotZ = -235.0f *count/maxCount+235.0f;
        Quaternion tar = Quaternion.Euler(0, 0, rotZ);
        clockNeedle.transform.rotation = Quaternion.Slerp(clockNeedle.transform.rotation, tar, smooth * Time.deltaTime);

        //key = playerScript.hasKey;
        if(count <= 0){
            count = 300f;
        }

        //Updates Ammo and Gun Icon/Name player has
        if (gm.yourGun != null)
        {
            ShowAmmo();

            //figures which icon to use
            if (gm.yourGun.CurrentWeapon == "revolver")
            {
                gunIcon.sprite = icons[0];
            }
            if (gm.yourGun.CurrentWeapon == "railgun")
            {
                gunIcon.sprite = icons[1];
            }
            if (gm.yourGun.CurrentWeapon == "rifle")
            {
                gunIcon.sprite = icons[2];
            }
            if (gm.yourGun.CurrentWeapon == "shotgun")
            {
                gunIcon.sprite = icons[3];
            }
            if(gm.yourGun.CurrentWeapon == "raygun"){
                gunIcon.sprite = icons[4];
            }

        }

        //key count display
        keyTxt.text = keyAmt.ToString();

        if (playerScript != null)
        {
            //updates health meter
            loseHealth();


            //evidence count display
            evidText.text = playerScript.Evidence.ToString();

            //evidence PopUp
            evid = playerScript.hasEvidence;
            if (evid == true)
            {
                evidPopUp.SetActive(true);
                curLock.m_MouseLook.SetCursorLock(canLock);
                evidDes.text = txt[playerScript.Evidence];
                Time.timeScale = 0;
            }

            //Pop up GameOver
            if (playerScript.player_health <= 0)
            {
                gameOver.SetActive(true);
                curLock.m_MouseLook.SetCursorLock(canLock);
            }

            //key count increase
            key = playerScript.hasKey;
            if (key == true)
            {
                keyAmt += 1;
                playerScript.hasKey=false;

            }

            //Weapon Display
            currentWeapon = playerScript.currentPick;
            switch(currentWeapon){
                case 1:
                    file1.SetActive(true);
                    file2.SetActive(false);
                    file3.SetActive(false);
                    break;
                case 2:
                    file1.SetActive(false);
                    file2.SetActive(true);
                    file3.SetActive(false);
                    break;
                case 3:
                    file1.SetActive(false);
                    file2.SetActive(false);
                    file3.SetActive(true);
                    break;
                default:
                    break;
            }
            
            yourWeapon.text = playerScript.Inventory[0].tag;
            yourWeapon2.text = playerScript.Inventory[1].tag;
            yourWeapon3.text = playerScript.Inventory[2].tag;
       
           if(playerScript.Inventory[0]==null){
                yourWeapon.text = " ";
                yourWeapon2.text = " ";
                yourWeapon3.text = " ";
            }

            if (youEnd == true)
            {
                if (endingsNum == 0)
                {
                    curLock.m_MouseLook.SetCursorLock(canLock);
                    endings.SetActive(true);
                    endText.text = end[0];
                }
                else if (endingsNum == 1)
                {
                    curLock.m_MouseLook.SetCursorLock(canLock);
                    endings.SetActive(true);
                    endText.text = end[1];
                }
                else if (endingsNum == 2)
                {
                    curLock.m_MouseLook.SetCursorLock(canLock);
                    endings.SetActive(true);
                    endText.text = end[2];
                }
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

    //Resume Button
    public void Resume(){
        curLock.m_MouseLook.SetCursorLock(lockCursor);
        playerScript.hasEvidence = false;
        youEnd = false;
        evidPopUp.SetActive(false);
        Time.timeScale = 1;
    }

    public void Quit(){
        SceneManager.LoadScene("startMenu", LoadSceneMode.Single);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using GAMEMANAGER;
using GUN;

namespace PLAYER
{


    public class Player : MonoBehaviour
    {
        private Rigidbody rb;
        public GameManager GM;
        public Camera PlayerCam;

        [Header("Player Sounds")]
        public AudioClip PlayerHurt; //got it
        public AudioClip PlayerDied;
        public AudioClip Healed;
        public AudioClip HeartBeat;
        public AudioClip Ticking;
        public AudioClip PickupAmmo;
        public AudioClip GetFolder;
        public AudioClip SwapWeapon;
        public AudioClip Chalk;
        public AudioClip Item;
        public AudioClip DropWeapon;

        public AudioSource PlayerSound;


        //Integrating player health for player object to work with game manager
        public int player_health;
        public int score;
        public string player_name;

        public Vector3 gunOffset;
        public GameObject holdingPosition;
        public GameObject Pos1, Pos2, Pos3;
        public GameObject weapon;
        public Gun gun;

        public bool weaponInRange;
        public bool hasWeapon;
        public bool isWeapon;
        public string currentGun;
		public static string AIDAMAGCURRENTGUNINFO; //Added To collect current gun for AI damage -- Caleb

        public GameObject UI;
        public GameObject xmark;
        public Vector3 surface_location;
        public Vector3 player_location;
        GameObject[] Markable;
        Collider col;

        GunTransitions gunTran;
        //The guns the player can hold
        public List<GameObject> Inventory = new List<GameObject>();
        public bool hasKey = false;
        public bool isKey = false;
        public int currentPick = 0;
        public bool dying = false;

        public int Evidence=0;
        public bool isEvidence = false;
        public bool hasEvidence = false;

        //Have to do this for stupid reasons
        GameObject YourEvidence;
        GameObject Key;

        //May need to put all player hands in here...
        public GameObject Tommygun_Hands;
        public GameObject Revolver_Hands;
        public GameObject Railgun_Hands;
        public GameObject Raygun_Hands;
        public GameObject Sonicgun_Hands;
        //it worked...

        void Start()
        {
            Time.timeScale = 1;
            //Need to recognize player body
            rb = GetComponent<Rigidbody>();

            //Start setting player data
            player_health = 100;
            player_name = "Player";
            score = 0;
            //Get your audiosource to play sound 
            PlayerSound = GetComponent<AudioSource>();
            

            weaponInRange = false;

            gunTran = GetComponentInChildren<GunTransitions>();
            GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
            GM.enabled = true;

            UI = GameObject.FindGameObjectWithTag("UI");
            Markable = GameObject.FindGameObjectsWithTag("markable");

            for (int i = 0; i < Markable.Length; i++) {
                 col = Markable[i].GetComponent<Collider>();
                
            }

            //GM.DeathCam.enabled = false;

        }

        void HealthSoundFX() {
            if (Healed != null)
            {
                PlayerSound.clip = Healed;
                PlayerSound.Play();
            }
        }
        void SwapFX()
        {
            if (SwapWeapon != null)
            {
                PlayerSound.PlayOneShot(SwapWeapon);
            }
        }

        void GetFolderFX()
        {
            if (GetFolder != null)
            {
                PlayerSound.clip = GetFolder;
                PlayerSound.Play();
            }
        }

        void AmmoSoundFX() {
            if (PickupAmmo != null) {
                PlayerSound.clip = PickupAmmo;
                PlayerSound.Play();
            }
        }

        void HurtSoundFX()
        {
            if (PlayerHurt != null)
            {
                PlayerSound.clip = PlayerHurt;
                PlayerSound.Play();
            }
        }

        void DeathSoundFX()
        {
            if (PlayerDied != null)
            {
                PlayerSound.clip = PlayerDied;
                PlayerSound.Play();
            }
        }

        //Work on changing some of this 
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Health"))
            {
                player_health += 50;
                HealthSoundFX();
                Destroy(other.gameObject);
            }
            //Armor will be deleted if not used 
            if (other.gameObject.CompareTag("Armor"))
            {

                Destroy(other.gameObject);
            }

            //Later change this to pick up specific types of ammo 
            if (other.gameObject.CompareTag("Ammo"))
            {
                gun.ammo += 50;
                AmmoSoundFX();
                Destroy(other.gameObject);
            }
            //Damage testing, change to take damage from bullets, traps, and hits
            if (other.gameObject.CompareTag("AIbullet"))
            {
                player_health -= 2;
                Destroy(other.gameObject);
                if (PlayerHurt != null) {
                    PlayerSound.PlayOneShot(PlayerHurt);
                }

            }

            if (other.gameObject.CompareTag("fork"))
            {
                player_health -= 10;
                Debug.Log("Player got slapped with a fork");
        
            }


            if (other.gameObject.CompareTag("shotgun"))
            {
                //other.gameObject.GetComponentInChildren<GameObject>().gameObject;
                weapon = Sonicgun_Hands;
                weaponInRange = true;
                UI.GetComponent<hud>().pickUpText.text = ("Press F to pickup " + weapon.tag);

            }
            
            if (other.gameObject.CompareTag("rifle") || other.gameObject.CompareTag("tommygun"))
            {
                //other.gameObject.GetComponentInChildren<GameObject>().gameObject;
                weapon = Tommygun_Hands;
                weaponInRange = true;
                UI.GetComponent<hud>().pickUpText.text = ("Press F to pickup Tommygun");

            }
            
            if (other.gameObject.CompareTag("revolver"))
            {
                //other.gameObject.GetComponentInChildren<GameObject>().gameObject;
                weapon = Revolver_Hands;
                weaponInRange = true;
                UI.GetComponent<hud>().pickUpText.text = ("Press F to pickup " + weapon.tag);
                Destroy(other.gameObject);

            }

            if (other.gameObject.CompareTag("railgun"))
            {
                //other.gameObject.GetComponentInChildren<GameObject>().gameObject;
                weapon = Railgun_Hands;
                weaponInRange = true;
                Destroy(other.gameObject);

                UI.GetComponent<hud>().pickUpText.text = ("Press F to pickup " + weapon.tag);
                

            }

            if (other.gameObject.CompareTag("raygun"))
            {
                //other.gameObject.GetComponentInChildren<GameObject>().gameObject;
                weapon = Raygun_Hands;
                weaponInRange = true;
                UI.GetComponent<hud>().pickUpText.text = ("Press F to pickup " + weapon.tag);
           
            }

            if (other.gameObject.CompareTag("evidence")) {
                isEvidence = true;
                YourEvidence = other.gameObject;
                 UI.GetComponent<hud>().pickUpText.text = ("Press F to pickup evidence");

            }

            if (other.gameObject.CompareTag("Key")) {
                isKey = true;
                Key = other.gameObject;
                UI.GetComponent<hud>().pickUpText.text = ("Press F to pickup lockpick");
            }
        }

        void OnTriggerExit(Collider other)
        {
            weaponInRange = false;
            isKey = false;
            isEvidence = false;
            UI.GetComponent<hud>().pickUpText.text = ("");
            
            

        }

        IEnumerator WaitForDeath() {
            yield return new WaitForSeconds(2.0f);
            Destroy(gameObject);
        }
        void CarryWeapons(int current) {
            current = currentPick;
            switch (current){
                case 1:
                    
                    if (Inventory[1] != null) Inventory[1].transform.position = Pos2.transform.position;
                    if (Inventory[2] != null) Inventory[2].transform.position = Pos3.transform.position;
                    
                    break;
                case 2:

                    if (Inventory[0] != null) Inventory[0].transform.position = Pos2.transform.position;
                    if (Inventory[2] != null) Inventory[2].transform.position = Pos3.transform.position;
                  

                    break;
                case 3:

                    if (Inventory[0] != null) Inventory[0].transform.position = Pos2.transform.position;
                    if (Inventory[1] != null) Inventory[1].transform.position = Pos3.transform.position;
                 
                    break;
                default:

                    break;
            }
        }

        void SwitchWeapon() {
            if (Input.GetKeyUp(KeyCode.Alpha1)) {
                currentGun = Inventory[0].tag;
                gun = Inventory[0].GetComponent<Gun>();
                GM.yourGun = Inventory[0].GetComponent<Gun>();
                PickUpWeapon(Inventory[0]);
                currentPick = 1;
                SwapFX();
                

            }

            if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                currentGun = Inventory[1].tag;
                gun = Inventory[1].GetComponent<Gun>();
                GM.yourGun = Inventory[1].GetComponent<Gun>();
                PickUpWeapon(Inventory[1]);
                currentPick = 2;
                SwapFX();
            }

            if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                currentGun = Inventory[2].tag;
                gun = Inventory[2].GetComponent<Gun>();
                GM.yourGun = Inventory[2].GetComponent<Gun>();
                PickUpWeapon(Inventory[2]);
                currentPick = 3;
                SwapFX();
            }


        }


        void PickUpWeapon(GameObject weapon)
        {
            //Play animation for weapon pick up
            hasWeapon = true;
            weaponInRange = false;
            gun.equipped = true;
            if (Input.GetMouseButton(1) && gun.canShoot==true)
            {
                gun.FireWeapon(weapon.tag);
            }

            if (Item != null) {
                PlayerSound.PlayOneShot(Item);
            }

        }
        GameObject g;
        void PlayHeatBeat(bool isDying) {
            isDying = dying;
            if(isDying==true) PlayerSound.PlayOneShot(HeartBeat);

        }

        void PlaceMark() {

            if (!col)
                return; // nothing to do without a collider
            Vector3 closestPoint = col.ClosestPoint(surface_location);
            Instantiate(xmark, transform.position,transform.rotation);

            if (Chalk != null) PlayerSound.PlayOneShot(Chalk);
            //use instantiate
        }

        void Update()
        {
            if (player_health > 100) player_health = 100;

            if (weaponInRange == true)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    currentGun = weapon.tag;
					AIDAMAGCURRENTGUNINFO = currentGun; //To collect name of current gun so that appropriate damage can be applied -- Caleb.
                    gun = weapon.GetComponent<Gun>();
                    GM.yourGun = weapon.GetComponent<Gun>();
                    PickUpWeapon(weapon);
                    Inventory.Add(weapon);
                }

                if (GM.pickupText != null) { }


            }
            if (isKey == true && Input.GetKeyDown(KeyCode.F)) {
                hasKey = true;
                //Temporary unless there is no animation for unlocking the door
                Destroy(Key);

            }
         
            if (Inventory.Count > 3) {
                Inventory.Remove(Inventory[0]);
                
            }
            SwitchWeapon();

            if (hasWeapon == true)
            {
                g = GameObject.FindGameObjectWithTag(currentGun);
                g.transform.position = holdingPosition.transform.position;
                g.transform.rotation = holdingPosition.transform.rotation;
                gun.FireWeapon(currentGun);
                // GM.pickupText.text = "";
            }

            if (isEvidence == true && Input.GetKeyDown(KeyCode.F)) {
                Evidence += 1;
                GetFolderFX();
                Destroy(YourEvidence);
                hasEvidence = true;
                isEvidence = false;

            }


            if (Input.GetKeyDown(KeyCode.B)&& hasWeapon==true)
            {
                
                Inventory.Remove(Inventory[currentPick]);
                hasWeapon = false;
                gun.equipped = false;
                if (DropWeapon != null) {
                    PlayerSound.PlayOneShot(DropWeapon);
                }

            }
            if (player_health <= 30)
            {
                //dying = true;  
                //PlayHeatBeat(dying);
            }

            if (dying == true) {
               
            }
            //Update players pos for placement purposes 
            player_location = gameObject.transform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(transform.position, player_location, 1)) {
                //highlight circle
                if (Input.GetKeyDown(KeyCode.X))
                {
                    PlaceMark();
                }
            }

            if (Inventory.Count == 1)
            {
                currentPick = 0;
            }

            if (player_health <= 0)
            {

                //GM.DeathCam.enabled = true;
               StartCoroutine(WaitForDeath());
               Time.timeScale = 0;
            }

            CarryWeapons(currentPick);

            if (player_health > 0)
            {
                Time.timeScale = 1;
            }
            
        }
    }
}

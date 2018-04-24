using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GUN;
using PLAYER;

public class AIDAMAG : AISpawner {

	Animator anim;
	public int Health = 30;
	public int Samage = 0;
	public static int Dmge = 0;
	public Transform BoboHead;
	public LayerMask mask = -1;
	public Transform Bullet;
	public Transform play;
	public GameObject plyr;
	public int LookRange = 20;
	public bool EnemyHasDied = false;
	public static bool shootHIM = false;
	public static bool MuhFaceHurt = false;
	public static float dist;
	public float Distance;
	public string curGun;
    //Aaron is adding this




 
    void Awake(){
		play = GameObject.FindGameObjectWithTag ("player").transform;
		plyr = GameObject.FindGameObjectWithTag ("player");

		anim = GetComponent<Animator> ();

	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet" || collision.gameObject.tag == "pellet")
        {

            if (Health > 0)
            {
                Health -= Samage;
                Debug.Log("Health: " + Health);
            }
            else
            {
                DestroyObject(gameObject);
            }
        }
        else if (collision.gameObject.tag == "pellet")
        {
            //AI.Escape = true;
            if (Health >= 0)
            {
                Health -= Samage;
            }
            else
            {
                DestroyObject(gameObject);
            }
        }

       // AI.Escape = true;

    }

    /*void GetDamage(string weaponName){
		switch (weaponName) {
		case "shotgun":
			Samage = plyr.GetComponent<Player> ().weapon.GetComponent<Gun> ().damage;
			break;
		case"revolver":
			Samage = plyr.GetComponent<Player> ().weapon.GetComponent<Gun> ().damage;
			break;
		case "railgun":
			Samage = plyr.GetComponent<Player> ().weapon.GetComponent<Gun> ().damage;
			break;
		case"raygun":
			Samage = plyr.GetComponent<Player> ().weapon.GetComponent<Gun> ().damage;
			break;
		case "rifle":
			Samage = plyr.GetComponent<Player> ().weapon.GetComponent<Gun> ().damage;
			
			break;
		default:
			//do nothing
			break;
		}

	}
    */

    void Update(){
        dist = AIDistanceCalculator.ClosestEnemyDistance;
        Distance = Vector3.Distance(gameObject.transform.position, play.position);
        shootHIM = AI.Escape;
        MuhFaceHurt = AI.WhosYourDaddy;

        curGun = Player.AIDAMAGCURRENTGUNINFO;
		Debug.Log (curGun);

		if(curGun == "railgun"){
            Samage = 12;
        }
        if (curGun == "raygun") {
			Samage = 10;
		}
        if (curGun == "shotgun") {
			Samage = 15;
		}
        if (curGun == "revolver") {
			Samage = 10;
		}
        if (curGun == "tommygun"|| curGun =="rifle") {
			Samage = 6;
		}
        if(Health<=0) DestroyObject(gameObject);

       // RaycastHit hit;
        //Ray BoboPeekABOO = new Ray (BoboHead.position, transform.forward);
		//Debug.DrawRay (BoboHead.position, transform.forward);
		if (shootHIM == true && Distance <= LookRange) {
			if (MuhFaceHurt == false) {
				anim.SetTrigger ("IsFiring");
				anim.ResetTrigger ("IsHitting");
			} else {
				Debug.Log ("I'm here!");
				anim.ResetTrigger ("IsFiring");
				anim.SetTrigger ("IsHitting");
			}
		} else {
			anim.ResetTrigger ("IsFiring");
		}

		//Samage = plyr.GetComponent<Player>().weapon.GetComponent<Gun>().;

        //GetDamage (plyr.GetComponent<Player> ().currentGun);
      
		/*if (plyr.GetComponent<Player> ().weapon.GetComponent<Gun> ().damage != null && Input.GetKeyDown (KeyCode.F)) {
			Samage = AISpawner.Damage;
			Debug.Log (Samage);
		}*/
	}


	

}

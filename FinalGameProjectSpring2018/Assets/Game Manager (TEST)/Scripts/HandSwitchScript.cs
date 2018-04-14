using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GUN;
using PLAYER;

public class HandSwitchScript : MonoBehaviour {

	public GameObject IdleHands;
	public GameObject RevolverHands;
	public GameObject TommygunHands;
	public GameObject ShotgunHands;
	public GameObject RailGunHands;
	public GameObject RayGunHands;
	public GameObject SpawnCurHands;
	public GameObject CurrentHands;
	public GameObject PreviousHands;
	public string CurReleventGun = "";

	void Awake() {
		CurrentHands = IdleHands;
		PreviousHands = IdleHands;
		Instantiate (CurrentHands, SpawnCurHands.transform);
	}
	
	// Update is called once per frame
	void Update () {
		if (PreviousHands != CurrentHands) {
			Instantiate (CurrentHands, SpawnCurHands.transform);
			PreviousHands = CurrentHands;
		}

		CurReleventGun = Player.AIDAMAGCURRENTGUNINFO;

		if(CurReleventGun == "railgun"){
			Destroy (GameObject.FindGameObjectWithTag("IdleHands"));
			Destroy (GameObject.FindGameObjectWithTag("RayGunHands"));
			Destroy (GameObject.FindGameObjectWithTag("ShotGunHands"));
			Destroy (GameObject.FindGameObjectWithTag("RifleHands"));
			Destroy (GameObject.FindGameObjectWithTag("RevolverHands"));
			CurrentHands = RailGunHands;
		}
		if (CurReleventGun == "raygun") {
			Destroy (GameObject.FindGameObjectWithTag("IdleHands"));
			Destroy (GameObject.FindGameObjectWithTag("RailGunHands"));
			Destroy (GameObject.FindGameObjectWithTag("ShotGunHands"));
			Destroy (GameObject.FindGameObjectWithTag("RifleHands"));
			Destroy (GameObject.FindGameObjectWithTag("RevolverHands"));
			CurrentHands = RayGunHands;
		}
		if (CurReleventGun == "shotgun") {
			Destroy (GameObject.FindGameObjectWithTag("IdleHands"));
			Destroy (GameObject.FindGameObjectWithTag("RayGunHands"));
			Destroy (GameObject.FindGameObjectWithTag("RailGunHands"));
			Destroy (GameObject.FindGameObjectWithTag("RifleHands"));
			Destroy (GameObject.FindGameObjectWithTag("RevolverHands"));
			CurrentHands = ShotgunHands;
		}
		if (CurReleventGun == "revolver") {
			Destroy (GameObject.FindGameObjectWithTag("IdleHands"));
			Destroy (GameObject.FindGameObjectWithTag("RayGunHands"));
			Destroy (GameObject.FindGameObjectWithTag("ShotGunHands"));
			Destroy (GameObject.FindGameObjectWithTag("RifleHands"));
			Destroy (GameObject.FindGameObjectWithTag("RailGunHands"));
			CurrentHands = RevolverHands;
		}
		if (CurReleventGun == "rifle") {
			Destroy (GameObject.FindGameObjectWithTag("IdleHands"));
			Destroy (GameObject.FindGameObjectWithTag("RayGunHands"));
			Destroy (GameObject.FindGameObjectWithTag("RailGunHands"));
			Destroy (GameObject.FindGameObjectWithTag("ShotGunHands"));
			Destroy (GameObject.FindGameObjectWithTag("RevolverHands"));
			CurrentHands = TommygunHands;
		}
		if (CurReleventGun == "") {
			Destroy (GameObject.FindGameObjectWithTag("RifleHands"));
			Destroy (GameObject.FindGameObjectWithTag("RayGunHands"));
			Destroy (GameObject.FindGameObjectWithTag("RailGunHands"));
			Destroy (GameObject.FindGameObjectWithTag("ShotGunHands"));
			Destroy (GameObject.FindGameObjectWithTag("RevolverHands"));
			CurrentHands = IdleHands;
		}
	}
}

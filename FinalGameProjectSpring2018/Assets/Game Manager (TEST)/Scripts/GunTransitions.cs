using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GUN;
using PLAYER;

public class GunTransitions : MonoBehaviour {
	public Animator animator;
    public Animation GunRecoil;
    public GameObject scopeOverlay;
    public GameObject weaponCamera;
    public GameObject crossHair;
    public Camera playerCam; //for referenced scopping
    public float scopeFOV = 15f; //field of view to be zoomed in
    private float normalFOV; //original fov
  
	private bool Scoped=false;
	public bool Reloading = false;
    public bool isRifle = false;
    public bool isShotgun= false;
    public bool isShooting = false;
    public bool isRevolver = false;
    public bool isRaygun= false;
    public bool isRailgun = false;

    public float reloadTime;
    public float duration; //to tweak how long the generic animation plays (Temporary: Need to have one day of work devoted to getting all animations and setting up all States)
    public string weapon;
    public string weaponBool;
    bool isRel;

   

    void Update () {
        reloadTime = GetComponentInParent<Player>().weapon.GetComponent<Gun>().reloadRate;

        /*if (Input.GetMouseButtonDown(1))
        {
                Scoped = !Scoped;
                animator.SetBool("isScoped", Scoped);
                if (Scoped)
                    StartCoroutine(OnScoped());
                else
                    OnUnscoped();
        }*/

		if (Input.GetKeyDown(KeyCode.R)&& weapon!=null) {
			Reloading = !Reloading;
            //GunRecoil["GunRecoil"].speed = reloadTime;
            ReloadGun(weapon);
            ReloadWaitTime(reloadTime);
			
		}

        if (Input.GetMouseButtonDown(0)){
            weapon = gameObject.GetComponentInParent<Camera>().gameObject.GetComponentInParent<Player>().currentGun;
            if (weapon == "shotgun")
            {
                isShotgun = true;
                weaponBool = "isShotgun";
            }

            if (weapon == "revolver")
            {
                isRevolver = true;
                weaponBool = "isRevolver";
            }

            if (weapon == "rifle")
            {
                isRifle = true;
                weaponBool = "isRifle";
            }

            if (weapon == "raygun")
            {
                isRaygun = true;
                weaponBool = "isRaygun";
            }

            if (weapon == "railgun")
            {
                isRailgun = true;
                weaponBool = "isRailgun";
            }

            Recoil();
        }
    }
    //To set the reload animation based on the current weapon
    void ReloadGun(string gun) {
        gun = weapon;
        switch (gun) {
            case "railgun":
                animator.SetBool(weaponBool, true);
                break;
            case "raygun":
                animator.SetBool(weaponBool, true);
                break;
            case "revolver":
                animator.SetBool(weaponBool, true);
                break;
            case "rifle":
                animator.SetBool(weaponBool, true);
                break;
            case "shotgun":
                animator.SetBool(weaponBool, true);
                break;
            default:
               
                break;
        }

    }

    void Recoil() {
        //Can't do switch with booleans here so I'll do a bunch of ifs
        if (isShotgun == true)
        {
        }
        if (isRifle == true)
        {

        }
        if (isShotgun == true)
        {

        }
        if (isRevolver == true) {

        }
        if (isRailgun == true) {
        }
        if (isRaygun == true) {

        }

    }
    IEnumerator ReloadWaitTime(float time)
    {
        yield return new WaitForSeconds(time);
        animator.SetBool(weaponBool, false);
    }

    IEnumerator OnScoped() {

        yield return new WaitForSeconds(0.15f);
        scopeOverlay.SetActive(true);
        weaponCamera.SetActive(false);
        crossHair.SetActive(false);

        normalFOV = playerCam.fieldOfView;
        playerCam.fieldOfView = scopeFOV;
        
    }

    void OnUnscoped() {
        scopeOverlay.SetActive(false);
        weaponCamera.SetActive(true);
        crossHair.SetActive(true);

        playerCam.fieldOfView = normalFOV;
    }
}

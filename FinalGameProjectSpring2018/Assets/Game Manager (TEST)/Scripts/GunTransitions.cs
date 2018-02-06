using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTransitions : MonoBehaviour {
	public Animator animator;
    public GameObject scopeOverlay;
    public GameObject weaponCamera;
    public GameObject crossHair;
    public Camera playerCam; //for referenced scopping
    public float scopeFOV = 15f; //field of view to be zoomed in
    private float normalFOV; //original fov

    
	private bool Scoped=false;
	// Use this for initialization
	void Start () {
       

	}

	void Update () {
        if (Input.GetMouseButtonDown(1))
   
        {
            
                Scoped = !Scoped;
                animator.SetBool("isScoped", Scoped);
                if (Scoped)
                    StartCoroutine(OnScoped());
                else
                    OnUnscoped();

            
        }

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

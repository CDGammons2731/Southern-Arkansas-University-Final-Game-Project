using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftPick : MonoBehaviour {

    Animator anim;
    public bool BoxIsOpen = false;
    public string lift = "lift";

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    IEnumerator LiftIt()
    {
        yield return new WaitForSeconds(3f);
        anim.SetBool(lift, true);
    }
    // Update is called once per frame
    void Update()
    {
        //To Test
        if (Input.GetKeyUp(KeyCode.E))
        {
            StartCoroutine(LiftIt());
            BoxIsOpen = !BoxIsOpen;
            anim.SetBool(lift, BoxIsOpen);
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBox : MonoBehaviour {
    Animator anim;
    public bool OpenIt=false;
    public string Open = "OpenIt";

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
    }

    IEnumerator StopIt() {
        yield return new WaitForSeconds(2f);
        anim.SetBool(Open,true);
    }
	// Update is called once per frame
	void Update () {
        //To Test
        if (Input.GetKeyUp(KeyCode.E)) {
            OpenIt = !OpenIt;
            anim.SetBool(Open, OpenIt);
            //StartCoroutine(StopIt());
        }
	}
}

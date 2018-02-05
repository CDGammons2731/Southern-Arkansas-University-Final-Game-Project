using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTransitions : MonoBehaviour {
	public Animator animator;
	private bool Scoped=false;
	// Use this for initialization
	void Start () {
		
	}

	void Update () {
		if (Input.GetKeyUp(KeyCode.LeftShift)) {
			Scoped = !Scoped;
			animator.SetBool ("isScoped",Scoped);
		}

	}
}

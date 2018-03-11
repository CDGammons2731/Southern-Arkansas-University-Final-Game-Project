using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationPlayer : MonoBehaviour {

	Animator anim;
	int jumpHash = Animator.StringToHash("RobotIdle");
	int RunHash = Animator.StringToHash("Base Layer.Run");

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		AnimatorStateInfo StateInfo = anim.GetCurrentAnimatorStateInfo (0);
		Debug.Log (StateInfo);
		anim.Play (jumpHash);

	}
}

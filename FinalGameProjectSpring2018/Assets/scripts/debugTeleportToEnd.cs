using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugTeleportToEnd : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Tab)) {
			GameObject plr = GameObject.FindGameObjectWithTag ("player");
			plr.transform.position = transform.position + new Vector3 (0, 99, 0);
		}
	}
}

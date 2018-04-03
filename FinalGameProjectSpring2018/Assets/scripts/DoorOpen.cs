using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour {
	GameObject plr;
	public float openDist = 5f;
	public float openSpeed = 1f;
	public float openHeight = 2f;
	Vector3 origPos;
	void Start() {
		origPos = transform.position;
	}
	// Update is called once per frame
	void FixedUpdate () {
		if (plr == null) {
			plr = GameObject.FindGameObjectWithTag ("player");
		}
		if (plr != null && Vector3.Distance (new Vector3 (plr.transform.position.x, 0, plr.transform.position.z), new Vector3 (transform.position.x, 0, transform.position.z)) < openDist) {
			transform.position = Vector3.MoveTowards (transform.position, origPos + new Vector3 (0, openHeight, 0), openSpeed * Time.deltaTime);
		} else {
			transform.position = Vector3.MoveTowards (transform.position, origPos, openSpeed * Time.deltaTime);
		}
	}
}

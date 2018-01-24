using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onceAudio : MonoBehaviour {


	public AudioClip sound;

	void Update () {
		
	}

	public void OnTriggerEnter (Collider col) {
		if (col.CompareTag ("Player")) {
			GetComponent<AudioSource> ().PlayOneShot (sound);
		}
	}
}

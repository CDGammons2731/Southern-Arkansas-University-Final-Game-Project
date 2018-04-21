using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePlayerUntilLoaded : MonoBehaviour {

	MonoBehaviour[] scripts;
	LevelGenerator gen;
	bool disabled = false;
	void Start () {
		scripts = GetComponentsInChildren<MonoBehaviour>();
		gen = GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<LevelGenerator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (gen != null) {
			if (gen.loaded) {
				foreach (MonoBehaviour script in scripts) {
					if (script != this) {
						script.enabled = true;
					}
				}
				Destroy(this);
			} else {
				if (!disabled) {
					foreach (MonoBehaviour script in scripts) {
						if (script != this) {
							script.enabled = false;
						}
					}
					disabled = true;
				}
			}
		}
	}
}

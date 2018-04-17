using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GAMEMANAGER;

public class LevelExit : MonoBehaviour {

	[SerializeField]
	GameManager go;

	void Start() {
		go = FindObjectOfType<GameManager> ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("player")) {
			int placeholder = 0;
			//increment level completion counter by 1
			if (placeholder == 6) {
				if (placeholder == 15) {
					// ending 15

				} else if (placeholder <= 14 && placeholder >= 10) {
					// ending 10-14

				} else {
					// ending 0-9

				}
			} else {
				SceneManager.LoadScene ("main", LoadSceneMode.Single);
			}
		}
	}
}

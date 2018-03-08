using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("player")) {
			SceneManager.LoadScene("main", LoadSceneMode.Single);
		}
	}
}

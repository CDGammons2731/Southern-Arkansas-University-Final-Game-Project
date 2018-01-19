using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Pickup : MonoBehaviour {

    public GameManager GM;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Health"))
        {
            GM.playerHealth += 50;
            other.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}

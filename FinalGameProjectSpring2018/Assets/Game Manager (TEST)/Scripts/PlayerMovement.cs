using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody rb;
    public bool isJumping = false;
    public GameManager GM;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Health"))
        {
            GM.playerHealth += 50;
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Armor"))
        {
            GM.playerArmor+= 25;
            other.gameObject.SetActive(false);
        }

        /*if (other.gameObject.CompareTag("Score"))
        {
            GM.score+= 300;
            other.gameObject.SetActive(false);
        }*/
    }

 

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        if (Input.GetKey(KeyCode.Space)) {
            gameObject.GetComponent<Rigidbody>().AddForce(transform.up * 5.0f, ForceMode.Impulse);
            isJumping = true;
        }
       
	}
}

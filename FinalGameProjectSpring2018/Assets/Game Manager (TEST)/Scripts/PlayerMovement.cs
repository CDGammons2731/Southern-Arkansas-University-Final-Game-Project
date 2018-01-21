using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody rb;
    public bool isJumping = false;
    public GameManager GM;
	public float fallmultiplayer = 2.5f;
	public float jumpforce = 55f;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
		fallmultiplayer = 2.5f;
		jumpforce = 55f;
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

		if (other.gameObject.CompareTag("Damage"))
		{
			GM.playerHealth -= 25;
			other.gameObject.SetActive(false);
		}
    }
		
    // Update is called once per frame
    void Update()
	{
		var x = Input.GetAxis ("Horizontal") * Time.deltaTime * 150.0f;
		var z = Input.GetAxis ("Vertical") * Time.deltaTime * 3.0f;

		transform.Rotate (0, x, 0);
		transform.Translate (0, 0, z);

		//Debug.Log ("Y velocity:" + gameObject.GetComponent<Rigidbody> ().velocity.y); /Just to check the change in y-vel as the player moves
		if (Input.GetKeyDown (KeyCode.Space) && (gameObject.GetComponent<Rigidbody> ().velocity.y >= -.1 && gameObject.GetComponent<Rigidbody> ().velocity.y<=.1)) {
			rb.GetComponent<Rigidbody> ().AddForce (transform.up * jumpforce, ForceMode.Impulse); //Initial jump force in the up direction
			rb.velocity += Vector3.up * Physics.gravity.y * (fallmultiplayer - 1) * Time.deltaTime; //Added a little extra gravity for a less floaty jump
				//isJumping = true;
		}
	}
}

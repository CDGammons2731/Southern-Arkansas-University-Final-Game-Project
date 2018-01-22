using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {
    private Rigidbody rb;
    public GameManager GM;
    public Camera PlayerCam;

    [Header("Sound FX")]
    public AudioClip walk;
    public AudioClip run;
    public AudioClip jump;
    public AudioClip hurt;
    public AudioClip die;
    public AudioClip heal;


    [Space (20)]
	public float fallmultiplayer = 2.5f;
	public float jumpforce = 55f;
    public bool isJumping = false;

    //Mouse look
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 12F;
    public float sensitivityY = 12F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    float rotationX = 0F;
    float rotationY = 0F;
    Quaternion originalRotation;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
		fallmultiplayer = 2.5f;
		jumpforce = 55f;

        if (rb)
            rb.freezeRotation = true;
        originalRotation = transform.localRotation;
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

    void Shoot() { }
    void PickUpWeapon() { }
    
		
    // Update is called once per frame
    void Update()
	{
        //Modify to play walking sounds, possibly use movement methods instead of update
		var x = Input.GetAxis ("Horizontal") * Time.deltaTime * 150.0f;
		var z = Input.GetAxis ("Vertical") * Time.deltaTime * 3.0f;

		transform.Rotate (0, x, 0);
		transform.Translate (0, 0, z);
        //Later change this to work with methods
        //Debug.Log ("Y velocity:" + gameObject.GetComponent<Rigidbody> ().velocity.y); /Just to check the change in y-vel as the player moves
        if (Input.GetKeyDown(KeyCode.Space) && (gameObject.GetComponent<Rigidbody>().velocity.y >= -.1 && gameObject.GetComponent<Rigidbody>().velocity.y <= .1))
        {
            rb.GetComponent<Rigidbody>().AddForce(transform.up * jumpforce, ForceMode.Impulse); //Initial jump force in the up direction
            isJumping = true;
            //play jump sound
            rb.velocity += Vector3.up * Physics.gravity.y * (fallmultiplayer - 1) * Time.deltaTime; //Added a little extra gravity for a less floaty jump
        }
        if (gameObject.GetComponent<Rigidbody>().velocity.y >= -.1 && gameObject.GetComponent<Rigidbody>().velocity.y <= .1) isJumping = false;

        if (axes == RotationAxes.MouseXAndY)
        {
            // Read the mouse input axis
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
            transform.localRotation = originalRotation * xQuaternion * yQuaternion;
        }
        else if (axes == RotationAxes.MouseX)
        {
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation = originalRotation * xQuaternion;
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
            transform.localRotation = originalRotation * yQuaternion;
        }
    }
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle <-360F)
         angle += 360F;
        if (angle >360F)
         angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}

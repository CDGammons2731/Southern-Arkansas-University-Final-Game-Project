using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hud : MonoBehaviour {
    public GameObject hubScreen;
    float timeLeft = 10.0f;
    bool timerIsActive = true;
    public float health = 15.0f;
    public float maxHealth = 30.0f;
    public GameObject needle;
    public float smooth = 2.0f;


	// Use this for initialization
	void Start () {
        
	
	}
	
	// Update is called once per frame
	void Update () {
        /*if (timerIsActive)
        {
            timeLeft -= Time.deltaTime;
            Debug.Log(+timeLeft);
            if (timeLeft < 0)
            {
                timeLeft = 0;
                timerIsActive = false;
                hubScreen.SetActive(false);

            }
        }*/
        loseHealth();
	}

    void loseHealth(){
        float rotationZ = 90.0f * (health / maxHealth)-90.0f;
        Quaternion target = Quaternion.Euler(0,0,rotationZ);
        needle.transform.rotation = Quaternion.Slerp(needle.transform.rotation, target, smooth * Time.deltaTime);

        
    }
}

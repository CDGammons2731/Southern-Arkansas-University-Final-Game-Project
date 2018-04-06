using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniMapMovement : MonoBehaviour
{

    public GameObject player;

    void Start()
    {
        
    }
    void Update(){
        player = GameObject.FindGameObjectWithTag("player");
    }
    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 newPosition = player.transform.position;
            newPosition.y = this.transform.position.y;
            this.transform.position = newPosition;
        }

    }
}

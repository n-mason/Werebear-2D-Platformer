using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        //store current camera's position in variable temp
        Vector3 temp = transform.position;

        // set the camera's position x to be equal to the player's position x
        temp.x = playerTransform.position.x;

        // set back the camera's temp position to the camera's current position
        transform.position = temp;
    }
} // class

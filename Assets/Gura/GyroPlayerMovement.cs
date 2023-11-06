using System.Collections;
using System.Collections.Generic;
//using System.Security.Cryptography;

using UnityEngine;

public class GyroPlayerMovement : MonoBehaviour
{
    Rigidbody2D playerRB;
    float dirX;
    float dirY;
    float dirZ;
    float moveSpeed = 25f;

    float shakingForce;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        dirX = Input.acceleration.x * moveSpeed;
        dirY = Input.acceleration.y * moveSpeed;
        dirZ = Input.acceleration.z * moveSpeed;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -8f, 8f), Mathf.Clamp(transform.position.y, -18f, 18f), 0f);

        //shakingForce = Input.acc
        
    }

    void FixedUpdate()
    {
        playerRB.velocity = new Vector3(dirX, dirY, dirZ);
    }
}

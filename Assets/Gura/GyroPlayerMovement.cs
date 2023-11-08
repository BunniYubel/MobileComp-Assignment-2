using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

using UnityEngine;

public class GyroPlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRB;
    //private float initialRotationX;
    //private float initialRotationY;
    //private float initialRotationZ;


    private float dirX;
    private float dirY;
    private float dirZ;
    private float moveSpeed = 25f;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        //initialRotationX = Input.acceleration.x;
        //initialRotationY = Input.acceleration.y;
        //initialRotationZ = Input.acceleration.z;
    }


    // Update is called once per frame
    void Update()
    {
        //float currentAccelerationZ = Input.acceleration.z;
        //float accelerationChagneZ = Mathf.Abs(currentAccelerationZ - initialRotationZ);
        //float currentAccelerationX = Input.acceleration.x;
        //float accelerationChagneX = Mathf.Abs(currentAccelerationX - initialRotationX);
        //float currentAccelerationY = Input.acceleration.y;
        //float accelerationChagneY = Mathf.Abs(currentAccelerationY - initialRotationY);
        
        
        dirX = Input.acceleration.x * moveSpeed;
        dirY = Input.acceleration.y * moveSpeed;
        dirZ = Input.acceleration.z * moveSpeed;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -8f, 8f), Mathf.Clamp(transform.position.y, -18f, 18f), 0f);
    }

    void FixedUpdate()
    {
        playerRB.velocity = new Vector3(dirX, dirY, dirZ);
    }
}

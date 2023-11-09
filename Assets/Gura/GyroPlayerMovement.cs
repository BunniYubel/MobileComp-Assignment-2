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

    private Vector3 dirInitial = Vector3.zero;

    

    private float dirX;
    private float dirY;
    private float dirZ;
    private float moveSpeed = 25f;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        dirInitial.x = Input.acceleration.x;
        //Debug.Log("dirInitial: " + dirInitial.x);


        dirInitial.y = Input.acceleration.y;
        //dirInitial.z = Input.acceleration.z;
        dirInitial.z = Input.acceleration.y; //-0.5f
        Debug.Log("dirInitial: " + dirInitial.z);
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
        Vector3 dir = Vector3.zero;

        //dirX = Input.acceleration.x * moveSpeed;
        //dirY = Input.acceleration.y * moveSpeed;
        //dirZ = Input.acceleration.z * moveSpeed;
        
        // input X controll left and right direction
        dir.x = Input.acceleration.x - dirInitial.x;

        float inputZ = Input.acceleration.z;
        
        Debug.Log("Input Z = " + Input.acceleration.z);
        if (inputZ > dirInitial.z)
        {
            if (inputZ < 0)
            {
                dir.y = -Mathf.Abs(inputZ - dirInitial.z);
            }
            else
            {
                dir.y = (inputZ - dirInitial.z) * -1;
            }
            
            Debug.Log("Moving backward");
            Debug.Log("Z changed = " + dir.y);
        }
        else
        {
            dir.y = Mathf.Abs(inputZ  - dirInitial.z);
            Debug.Log("Moving forward");
            Debug.Log("Z changed = " + dir.y);

        }

        //Debug.Log("Input Z = " + Input.acceleration.z);
        //dir.y = Input.acceleration.z * - 1.0f - dirInitial.z;
        //dir.z = -Input.acceleration.z - dirInitial.z;

        //dir.z = Input.acceleration.y - dirInitial.y;
        //Debug.Log("X changed = " + dir.x);
        //Debug.Log("Input X = " + Input.acceleration.x);
        //Debug.Log("Z changed = " + dir.y);
        
        //Debug.Log("Y changed = " + dir.y);
        //Debug.Log("Input Y = " + Input.acceleration.y);
        if (dir.sqrMagnitude > 1)
        {
            dir.Normalize();
        }

        dir *= Time.deltaTime;
        transform.Translate(dir * moveSpeed);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -8f, 8f), Mathf.Clamp(transform.position.y, -18f, 18f), 0f);
    }

    //void fixedUpdate()
    //{
    //    playerRB.velocity = new Vector3(dirX, dirY, dirZ);
    //}
}

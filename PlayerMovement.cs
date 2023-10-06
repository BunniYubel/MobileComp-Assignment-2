using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed;
    public float horizontalBorder = 2.6f;
    public float verticalBorder = 5f;
    public Vector3 moveAmount;

    // Update is called once per frame
    void Update()
    {
        moveAmount += new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * movespeed, Input.GetAxisRaw("Vertical") * Time.deltaTime * movespeed, 0);
        Vector3 moveDiff = moveAmount * Time.deltaTime * 8;
        transform.position += moveDiff;
        moveAmount -= moveDiff;

        if (transform.position.x < -horizontalBorder) transform.position = new Vector3(-horizontalBorder, transform.position.y, transform.position.z);
        if (transform.position.x > horizontalBorder) transform.position = new Vector3(horizontalBorder, transform.position.y, transform.position.z);
        if (transform.position.y < -verticalBorder) transform.position = new Vector3(transform.position.x, -verticalBorder, transform.position.z);
        if (transform.position.y > verticalBorder) transform.position = new Vector3(transform.position.x, verticalBorder, transform.position.z);
    }
}

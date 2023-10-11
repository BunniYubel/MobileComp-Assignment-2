using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KroniiMovement : MonoBehaviour
{
    public float speed; // Adjust the movement speed as needed.
    public float leftBoundary = -8.0f; // Set the left boundary position.
    public float rightBoundary = 8.0f; // Set the right boundary position.
    private int direction = 1; // 1 for right, -1 for left.

    // Update is called once per frame
    void Update()
    {
        // Move the object in the current direction.
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

        // Check if the object has reached the left or right boundary.
        if (transform.position.x <= leftBoundary || transform.position.x >= rightBoundary)
        {
            // Change the direction when it hits a boundary.
            direction *= -1;
        }
    }
}

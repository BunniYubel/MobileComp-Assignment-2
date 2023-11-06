using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class _KroniiBulletScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction;
    private float speed = 5.0f;
    public int power = 10;

    private void Awake()
    {
        rb = GetComponent <Rigidbody2D>();
    }

    private void Update()
    {
        // Move the bullet in the set direction with a constant speed.
        rb.velocity = direction.normalized * speed;
    }

    // Set the direction and speed for the bullet.
    public void SetDirectionAndSpeed(Vector2 newDirection, float newSpeed)
    {
        direction = newDirection;
        speed = newSpeed;
    }

    // Deactivate the bullet upon collision with the player.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Assuming the player has a "Player" tag.
        {
            gameObject.SetActive(false); // Deactivate the bullet.
        }
    }

}

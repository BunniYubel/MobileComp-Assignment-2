using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 10f; // Adjust the bullet's speed as needed.
    public Transform PlayerBulletSpawner;
    public GuraPoolManager bulletPool;

    private Rigidbody2D rb;
    private Vector3 startPosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    void OnEnable()
    {
        rb.velocity = transform.up * speed;
    }

    void OnBecameInvisible()
    {
        DeactivateAndReset();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Handle the collision with "BossKronii" here.

            // For example, you can play an impact effect or deal damage to "BossKronii."

            // Deactivate and reset the player bullet and return it to the pool.
            DeactivateAndReturnToPool();
        }
    }

    void DeactivateAndReturnToPool()
    {
        // Deactivate the player bullet.
        gameObject.SetActive(false);

        // Reset the bullet's properties if needed (e.g., position, rotation).

        // Return the bullet to the pool.
        bulletPool.ReturnBullet(gameObject);
    }

    public void ResetBullet()
    {
        // Reset the position to the starting position (bullet spawner).
        transform.position = startPosition; // Reset to initial position.
    }
    public void DeactivateAndReset()
    {
        gameObject.SetActive(false); // Deactivate the bullet.
        ResetBullet(); // Reset the bullet's properties.
        bulletPool.ReturnBullet(gameObject); // Return the bullet to the pool.
    }
}

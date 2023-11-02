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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuraShoot : MonoBehaviour
{
    public GuraPoolManager bulletPool;
    
    public float fireRate; // Adjust this for your desired fire rate.
    public float bulletSpeed;

    private float nextFireTime;

    void Start()
    {
        nextFireTime = Time.time + fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if it's time to fire a bullet.
        if (Time.time >= nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + fireRate;
        }
    }

    void FireBullet()
    {
        // Get a bullet from the pool manager.
        GameObject bullet = bulletPool.GetBullet();

        if (bullet != null)
        {
            // Set the bullet's position.
            bullet.transform.position = transform.position;
            bullet.SetActive(true);

            // Set the bullet's velocity for vertical movement (upward).
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.up * bulletSpeed; // Vertical direction with speed.
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuraShoot : MonoBehaviour
{
    public GuraPoolManager bulletPool;
    public Transform PlayerBulletSpawner;
    public float fireRate; // Adjust this for your desired fire rate.

    private float nextFireTime;

    void Start()
    {
        nextFireTime = Time.time + fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }
    void Shoot()
    {
        GameObject bullet = bulletPool.GetBullet();

        if (bullet != null)
        {
            bullet.transform.position = PlayerBulletSpawner.position;
            bullet.SetActive(true);
            // Customize bullet behavior here (e.g., speed, damage).
        }
    }
}

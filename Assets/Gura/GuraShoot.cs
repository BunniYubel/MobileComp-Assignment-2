using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuraShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GuraPoolManager bulletPool;
    public MicrophoneInput microphoneinput;
    public float fireRate; // Adjust this for your desired fire rate.
    public float bulletSpeed = 3f;

    private float nextFireTime;

    void Start()
    {
        nextFireTime = Time.time + fireRate;
        microphoneinput=GameObject.Find("MicrophoneInput").GetComponent<MicrophoneInput>();
    }

    // Update is called once per frame
    void Update()
    {        // Check if it's time to fire a bullet.
        if (Time.time >= nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + fireRate;
        }
    }

    void FireBullet()
    {
        GameObject bullet = bulletPool.GetBullet();
        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.SetActive(true);

            // ��ȡ��ǰ������˷�������ӵ��ٶ�
            float currentBulletSpeed = microphoneinput.GetCurrentBulletSpeed();
            currentBulletSpeed = Mathf.Max(currentBulletSpeed, 3.5f);

            // ��ȡ�ӵ��� NewBulletScript ����������ٶ�
            NewBulletScript bulletScript = bullet.GetComponent<NewBulletScript>();
            if (bulletScript != null)
            {
                bulletScript.SetBulletSpeed(currentBulletSpeed);
            }

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.up * currentBulletSpeed;
        }
    }



}

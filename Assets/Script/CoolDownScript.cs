using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownScript : MonoBehaviour
{
     
    public GameObject SuperBullet;
    public float cooldownTime = 2;

    private float nextFireTime = 0;

    private float initialRotationX; 

    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
        initialRotationX = Input.gyro.rotationRateUnbiased.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFireTime)
        {
            if (Input.gyro.rotationRateUnbiased.x < -6f)
            {
                Instantiate(SuperBullet, transform.position, transform.rotation);
                print("ability used, cooldown started");
                
                nextFireTime = Time.time + cooldownTime;
            }
        }
    }
}

using UnityEngine;

public class _KroniiBulletSpawner1 : MonoBehaviour
{
    public _KroniiPoolManager bulletPoolManager;
    public float bulletSpeed = 10.0f;

    private float timeSinceLastFire = 0.0f;
    private float fireRate = 0.8f; // Fire a bullet every 0.5 seconds

    void Update()
    {
        timeSinceLastFire += Time.deltaTime;
        if (timeSinceLastFire >= fireRate)
        {
            FireBullet();
            timeSinceLastFire = 0.0f; // Reset the timer.
        }
    }

    void FireBullet()
    {
        GameObject bullet = bulletPoolManager.GetKroniiBullet();
        if (bullet != null)
        {
            bullet.transform.position = transform.position;

            // Set the bullet's direction and speed to move straight down.
            _KroniiBulletScript bulletScript = bullet.GetComponent<_KroniiBulletScript>();
            bulletScript.SetDirectionAndSpeed(Vector2.down, bulletSpeed);

            // Set the bullet as active.
            bullet.SetActive(true);
        }
    }
}

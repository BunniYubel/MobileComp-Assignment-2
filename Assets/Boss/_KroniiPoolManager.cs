using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _KroniiPoolManager : MonoBehaviour
{
    public GameObject kroniiBullet;
    public int poolSize = 10;

    private List<GameObject> bulletPool;

    // Start is called before the first frame update
    void Start()
    {
        InitializeBulletPool();
    }

    void InitializeBulletPool()
    {
        bulletPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(kroniiBullet, transform.position, Quaternion.identity);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    public GameObject GetKroniiBullet()
    {
        foreach (GameObject bullet in bulletPool)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }
        return null;
    }
    public void ReturnKroniiBullet(GameObject kroniiBullet)
    {
        kroniiBullet.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuraPoolManager : MonoBehaviour
{
    public GameObject NewPlayerBulletPrefab;
    public int poolSize;

    private List<GameObject> bulletPool = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        InitializePool();
    }

    void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(NewPlayerBulletPrefab);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    public GameObject GetBullet()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].activeInHierarchy)
            {
                return bulletPool[i];
            }
        }

        return null; // Return null if all bullets are in use.
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
    }
}

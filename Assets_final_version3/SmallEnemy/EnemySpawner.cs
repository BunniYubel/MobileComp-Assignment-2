using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public int hit_cnt = 0;
    public GameObject boss;
    public GameObject enemyPrefab; // 小兵预制件
    public float spawnInterval = 1.5f; // 生成间隔时间
    private float nextSpawnTime;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        // 在Spawner位置生成小兵
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
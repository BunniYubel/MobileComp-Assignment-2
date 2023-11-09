using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public int hit_cnt = 0;
    public GameObject boss;
    public GameObject enemyPrefab; // С��Ԥ�Ƽ�
    public float spawnInterval = 1.5f; // ���ɼ��ʱ��
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
        // ��Spawnerλ������С��
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
using System.Collections;
using UnityEngine;
public class SmallEnemyBehaviour : MonoBehaviour
{
    public EnemySpawner enmeyspwan;
    public Playermanager playermanager;

    public float minSpeed = 2f;
    public float maxSpeed = 5f;
    public float horizontalAmplitude = 8.0f;
    public float minFrequency = 1.0f;
    public float maxFrequency = 2.0f;
    public int power = 10; // 敌人对玩家的伤害
    public int scoreValue = 10; // 击败敌人获得的分数

    private float speed;
    private float frequency;
    private float originalX;

    void Start()
    {
        playermanager= GameObject.Find("Player").GetComponent<Playermanager>();
        enmeyspwan = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        originalX = transform.position.x;
        speed = Random.Range(minSpeed, maxSpeed);
        frequency = Random.Range(minFrequency, maxFrequency);
    }

    void Update()
    {
        float x = originalX + Mathf.Sin(Time.time * frequency) * horizontalAmplitude;
        float y = transform.position.y - speed * Time.deltaTime;
        transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.tag);
        Debug.Log("Colliding object name: " + collision.gameObject.name);
        if (collision.gameObject.tag == "PlayerBullet" )
        {
            enmeyspwan.hit_cnt+=1;
            Gamemanager.Instance.AddScore(10); // 击败小兵增加10分
            Destroy(gameObject); // 碰撞后销毁小兵
            if (enmeyspwan.hit_cnt >= 10)
            {
                enmeyspwan.boss.SetActive(true);
            }
            Debug.Log("SmallEnemy hit by: " + collision.gameObject.tag);
           
        }
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collided with player, Playermanager is null? " + (playermanager == null));
            playermanager.m_life -= power;
            playermanager.m_slider_hp.value = playermanager.m_life;
            Debug.Log("Player life after collision: " + playermanager.m_life);
            Destroy(gameObject); // 碰撞后销毁小兵
        }

    }
    void Die()
    {
        Destroy(gameObject); // 销毁小兵
    }


}


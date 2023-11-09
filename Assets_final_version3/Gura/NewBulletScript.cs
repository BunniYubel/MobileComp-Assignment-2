using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewBulletScript : MonoBehaviour
{
    private float bulletSpeed = 10f;  
    private Rigidbody2D rb;
    public GameObject m_explosionFX;  
    public TextMeshProUGUI scoreText; 

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        rb.velocity = transform.up * bulletSpeed;  
    }

    public void SetBulletSpeed(float speed)
    {
        bulletSpeed = speed;
        //Debug.Log("New bullet speed set: " + bulletSpeed);
    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false); 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            KroniiMovement kroni = other.GetComponent<KroniiMovement>();
            if (kroni != null)
            {
                Handheld.Vibrate(); // 震动
                Gamemanager.Instance.AddScore(1);
                kroni.life -= 1;
                kroni.m_slider_hp.value -= 1;
                if (kroni.life <= 0)
                {
                    Instantiate(m_explosionFX, transform.position, Quaternion.identity);
                    for (int i = 0; i < 5; i++)
                    {
                        if (Gamemanager.score_list[i] == 0)
                        {
                            Gamemanager.score_list[i] = Gamemanager.Instance.m_score;
                            break;
                        }
                    }
                    Gamemanager.Instance.objs[2].SetActive(true);
                    Destroy(other.gameObject); // 销毁敌人而不是子弹
                }
            }

            // Deactivate the bullet when it hits an enemy.
            gameObject.SetActive(false);

            int currentScore = PlayerPrefs.GetInt("Score", 0); // Get the current score (default to 0).
            currentScore++; // Increment the score.
            PlayerPrefs.SetInt("Score", currentScore); // Save the updated score.

            // Update the display (if you have a TextMeshPro Text element).
            if (scoreText != null)
            {
                scoreText.text = "Score: " + currentScore;
            }
        }
        
    }
}

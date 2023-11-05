using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewBulletScript : MonoBehaviour
{
    public float speed;
    public int power = 1;
    private Rigidbody2D rb;
    public GameObject m_explosionFX;

    // Reference to the TextMeshProUGUI Text component.
    public TextMeshProUGUI scoreText;

   
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnBecameInvisible()
    {
        // Deactivate the bullet when it goes out of the camera's view.
        gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

            KroniiMovement kroni = collision.gameObject.GetComponent<KroniiMovement>();
            if (kroni != null)
            {
                Handheld.Vibrate();//Υπ¶―
                Gamemanager.Instance.AddScore(1);
                kroni.life -= power;
                kroni.m_slider_hp.value -= power;
                if(kroni.life<=0)
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
     
                    Destroy(this.gameObject);
                }
            }

            // Handle the collision with an enemy or boss here.
            // For example, you can play an impact effect or deal damage to the enemy.

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

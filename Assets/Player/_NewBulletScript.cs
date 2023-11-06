using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class _NewBulletScript : MonoBehaviour
{
    public float speed;
    public float gianPoint;
    private Rigidbody2D rb;
    //public PlayerScoreCounter counter;

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
            // Handle the collision with an enemy or boss here.
            // For example, you can play an impact effect or deal damage to the enemy.

            // Deactivate the bullet when it hits an enemy.
            gameObject.SetActive(false);
            //counter.HitTarget(gianPoint);
            GameObject.Find("Player").GetComponent<PlayerScoreCounter>().HitTarget(gianPoint);
            
            //int currentScore = PlayerPrefs.GetInt("Score", 0); // Get the current score (default to 0).
            //currentScore++; // Increment the score.
            //PlayerPrefs.SetInt("Score", currentScore); // Save the updated score.

            // Update the display (if you have a TextMeshPro Text element).
            //if (scoreText != null)
            //{
            //    scoreText.text = "Score: " + currentScore;
            //}
        }
    }
}
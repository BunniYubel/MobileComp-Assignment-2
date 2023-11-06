using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public float health, maxHealth;
    public GameObject m_explosionFX;

    public static event Action OnPlayerDamaged;
    public static event Action  OnPlayerDeath;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamge(float damage)
    {
        health -= damage;
        OnPlayerDamaged?.Invoke();
        Debug.Log("player damaged. HP: " + health);

        if (health <= 0)
        {
            health = 0;
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
            OnPlayerDeath?.Invoke();
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public float health, maxHealth;

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
            OnPlayerDeath?.Invoke();
        }

    }
}

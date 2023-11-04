using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerScoreCounter : MonoBehaviour
{
    public float score;

    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    public void HittedByBoss(float loosePoint)
    {
        score -= loosePoint;
        OnPlayerDamaged?.Invoke();
        Debug.Log("player score down: " + score);

    }
    public void HitTarget(float gain)
    {
        score += gain;
        OnPlayerDamaged?.Invoke();
        Debug.Log("player score down: " + score);

    }
}

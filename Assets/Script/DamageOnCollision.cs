using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    public int bossDamage = 1;
    public float loosePoint;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("damge to player");
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamge(bossDamage);
            collision.gameObject.GetComponent<PlayerScoreCounter>().HittedByBoss(loosePoint);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseControll : MonoBehaviour
{
    public Enemy_Flying[] enemyArray;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (FlyingEnemyScript enemy in enemyArray)
            {
                enemy.chase = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (FlyingEnemy enemy in enemyArray)
            {
                enemy.chase = false;
            }
        }
    }
}

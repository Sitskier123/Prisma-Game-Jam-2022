using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Global Variable (Can be accessed anywhere)
// int numEnemies = 0;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 5;
    int currentHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        // numEnemies++;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Play hurt animation --> LATER

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");

        //Die animation

        //Disable the enemy
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

}

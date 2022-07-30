using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int health;

    public PlayerHealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

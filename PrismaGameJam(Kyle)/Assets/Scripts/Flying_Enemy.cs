using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying_Enemy : MonoBehaviour
{
    public EnemyFollowPlayer enemyFollowPlayer;
    public Animator animator;

    Rigidbody2D rb;

    //public HealthbarBehavior Healthbar;
    public int maxHealth = 5;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        //Healthbar.SetHealth(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");

        animator.SetTrigger("IsDead");
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        this.enabled = false;
        enemyFollowPlayer.enabled = false;


        //bat.mass = 5; // Set the GO's mass to 5 via the Rigidbody.
    }

}

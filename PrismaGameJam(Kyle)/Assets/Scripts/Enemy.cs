using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public AIPatrol aiPatrol;

    public Animator animator;

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

        BoxCollider2D[] myColliders = gameObject.GetComponents<BoxCollider2D>();
        foreach (BoxCollider2D bc in myColliders) bc.enabled = false;
        this.enabled = false;
        aiPatrol.enabled = false;

        //GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

        //GetComponents(typeof(Collider2D)).enabled = false;
        //this.enabled = false;
    }

}

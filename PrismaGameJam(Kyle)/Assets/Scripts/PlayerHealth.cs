using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 10.0f;
    public float health;


    public PlayerHealthBar healthBar;

    [SerializeField] LayerMask groundLayer;
    const float groundCheckRadius = 0.2f;
    [SerializeField] Transform groundCheckCollider;
    bool isGrounded = true;

    private int ticks = 0;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        StartCoroutine(tickDamage());
    }

    void GroundCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position,groundCheckRadius,groundLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;
        }else
        {
            //isGrounded = false;
            //Debug.Log("Health Bonus");
            if (health <= 9.5f)
            {
                health += 0.5f;
            }
        }
        healthBar.SetHealth(health);
    }

    IEnumerator tickDamage()
    {
        while (isGrounded == true)
        {
            //Debug.Log("tickDamage enabled");
            // ticks increments 60 times per second, as an example
            ticks++;
            // Condition is true once every second
            if (ticks % 2 == 0)
            {
                TakeDamage(0.5f); //Damage being taken per second
            }
            yield return new WaitForSeconds(1.0f);
        }
    }

    void Update()
    {
        GroundCheck();
        //Debug.Log("Entered Update");
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.SetHealth(health);

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerController playerController;

    public Animator animator;

    public int maxHealth = 10;
    public int health;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Hit");
        health -= damage;
        if(health <= 0)
        {
            animator.SetTrigger("IsDead");

            BoxCollider2D[] myBoxColliders = gameObject.GetComponents<BoxCollider2D>();
            foreach (BoxCollider2D bc in myBoxColliders) bc.enabled = false;

            CircleCollider2D[] myCircleColliders = gameObject.GetComponents<CircleCollider2D>();
            foreach (CircleCollider2D cc in myCircleColliders) cc.enabled = false;

            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            this.enabled = false;
            playerController.enabled = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerController playerController;


    public int maxHealth = 10;
    public int health;
    public bool isHit = false;

    private bool tookDamage = false;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    /*
    void Update()
    {
        tookDamage = false;
    }
    */

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        health -= damage;
        tookDamage = true;
        StartCoroutine(HitAnimationDelay()); // Calls HitAnimationDelay() function
        if (health <= 0)
        {
            BoxCollider2D[] myBoxColliders = gameObject.GetComponents<BoxCollider2D>();
            foreach (BoxCollider2D bc in myBoxColliders) bc.enabled = false;

            CircleCollider2D[] myCircleColliders = gameObject.GetComponents<CircleCollider2D>();
            foreach (CircleCollider2D cc in myCircleColliders) cc.enabled = false;

            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            this.enabled = false;
            playerController.enabled = false;
        }
    }

    IEnumerator HitAnimationDelay()
    {
        //Debug.Log("ENUM");
        if (tookDamage == true)
        {
            isHit = true;
            yield return new WaitForSeconds(2); //Lets Player_Hit animation play for 2 seconds
        }
        else
            isHit = false;

        tookDamage = false;
    }
}

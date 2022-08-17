using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 1;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public bool mele = false;

    // Update is called once per frame
    void Update()
    {
        mele = false;
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        mele = true;
        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage them
        //foreach(Collider2D enemy in hitEnemies)
        //{
        //    enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        //}

        foreach (Collider2D bat in hitEnemies)
        {
            bat.GetComponent<Flying_Enemy>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Animator animator;
    public Transform firePoint;
    public int damage = 1;
    public GameObject impactEffect;
    public LineRenderer lineRenderer;

    float rangeAttackRate = 1f;
    float nextRangeAttackTime = 0f;

    // Update is called once per frame
    void Update ()
    {
        if(Time.time >= nextRangeAttackTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                StartCoroutine(Shoot());
                nextRangeAttackTime = Time.time + 1f / rangeAttackRate;
            }
        }
    }

    IEnumerator Shoot ()
    {
        // shooting logic
        // NOTE: To change which button to press for firing select Edit --> Project Settings --> Input Manager --> Axes --> Fire1 --> Alt Positive Button (enter value) in Unity
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);

        //lineRenderer.enabled = false;

        animator.SetTrigger("Shoot");

        if (hitInfo)
        {
            // Debug.Log("Shots Fired!");
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Instantiate(impactEffect, hitInfo.point, Quaternion.identity);

            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, firePoint.position + firePoint.right * 100);
        }

        lineRenderer.enabled = true;

        // wait one frame
        yield return new WaitForSeconds(0.5f);

        lineRenderer.enabled = false;
    }

}

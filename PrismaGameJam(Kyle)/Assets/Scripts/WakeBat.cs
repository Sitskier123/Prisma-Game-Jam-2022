using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class WakeBat : MonoBehaviour
{
    public Animator animator;
    public Collider2D trigger;

    public bool ReachedEndOfPath;


    void Start()
    {
        //aiPath = GetComponent<AIPath>();
        ReachedEndOfPath = true;
    }

    void Update()
    {
        OnTriggerEnter2D(trigger);
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.tag == "Player")
        {
            //GetComponent(aiPath).enabled = false;
            ReachedEndOfPath = false;
            animator.SetBool("isSleep", false);
        }
        else
            ReachedEndOfPath = true;
    }
}
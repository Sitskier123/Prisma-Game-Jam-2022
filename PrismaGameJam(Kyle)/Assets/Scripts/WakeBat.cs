using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public Animator animator;
public Collider2D trigger;

private bool toggle == false;

public class WakeBat : MonoBehaviour
{
    void OnTriggerEnter(Collider2D trigger, bool toggle)
    {
        if (trigger.tag == "Player")
        {
            bool.toggle == true;
            animator.SetBool("isSleep", false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    PlayerController playerController; // Creates a reference to the PlayerController script
    // [SerializeField] makes a game object editable within the editor but keeps the variable private
    [SerializeField] GameObject player; //references the game object PlayerController.cs is attatched to

    PlayerCombat playerCombat;
    Weapon weapon;
    PlayerHealth playerHealth;
    

    private Animator animator;
    private string currentState;

    // Animation States
    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_RUN = "Player_Run";
    const string PLAYER_HIT = "Player_Hit";
    const string PLAYER_JUMP = "Player_Jump";
    const string PLAYER_MELE = "Player_Mele";
    const string PLAYER_SHOOT = "Player_Shoot";
    const string PLAYER_DEAD = "Player_Dead";
    // Use ChangeAnimationState(ANIMATION_NAME); to change the state of the animation


    // Awake is used to assign initial component data similar to Start() (it's a safety protocal)
    void Awake()
    {
        // If you want to access a function within PlayerController.cs, call playerController.MethodName();
        playerController = player.GetComponent<PlayerController>(); // Assigns playerController to PlayerController.cs
        playerCombat = player.GetComponent<PlayerCombat>(); // Ditto^
        weapon = player.GetComponent<Weapon>(); //Ditto
        playerHealth = player.GetComponent<PlayerHealth>(); //Ditto
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Function changes the currentState animation to a newState
    void ChangeAnimationState(string newState)
    {
        //stop the same animation from interrupting itself (this is a guard)
        if (currentState == newState) return;

        //play the animation
        animator.Play(newState);

        //reassign the current state
        currentState = newState;
    }

    // Animations are to be set and played here
    private void Update()
    {
        //Checks if Run() is true and if so, plays the PLAYER_RUN animation
        if (playerController.isGrounded) //true
        {
            if (playerController.run)
            {
                ChangeAnimationState(PLAYER_RUN);
            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
            if (playerCombat.mele) //true
            {
                //Debug.Log("MELE");
                ChangeAnimationState(PLAYER_MELE);
            }
        }

        if (!playerController.isGrounded) //false
        {
           //Debug.Log("JUMP");
           ChangeAnimationState(PLAYER_JUMP);
        }

        if (weapon.fired) //true
        {
            //Debug.Log("SHOT");
            ChangeAnimationState(PLAYER_SHOOT);
        }

        if (playerHealth.isHit) //true
        {
            ChangeAnimationState(PLAYER_HIT);
        }
    }
}
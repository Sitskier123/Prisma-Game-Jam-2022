using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData data;

    //State parameters
    public bool isFacingRight { get; private set; }
    public bool isJumping { get; private set; }
    public float lastTimeOnGround { get; private set; }

    //Input parameters
    public float lastTimePressedJump { get; private set; }

    //Components
    private Rigidbody2D rb2d;
    Animator animator;

    //Check parameters
    [Header("Checks")]
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private Vector2 _groundCheckSize;
    [Space(5)]

    //Layers and tags
    [Header("Layers and Tags")]
    [SerializeField] private LayerMask ground;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        //Input setup
        InputHandler.instance.OnJumpPressed += args => OnJump(args);
        InputHandler.instance.OnJumpReleased += args => OnJumpUp(args);

        SetGravityScale(data.gravityScale);
        isFacingRight = true;
    }

    void Update()
    {
        #region TIMERS
        lastTimeOnGround -= Time.deltaTime;
        lastTimePressedJump -= Time.deltaTime;
        #endregion

        #region GENERAL CHECKS
        if (InputHandler.instance.MoveInput.x != 0)
            CheckDirectionToFace(InputHandler.instance.MoveInput.x > 0);
        #endregion

        #region PHYSICS CHECKS
        if (!isJumping)
        {
            //Ground Check
            if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, ground))
                lastTimeOnGround = data.coyoteTime; //sets lastGrounded to coyoteTime
        }
        #endregion

        #region GRAVITY
        if (rb2d.velocity.y >= 0)
            SetGravityScale(data.gravityScale);
        else if (InputHandler.instance.MoveInput.y < 0)
            SetGravityScale(data.gravityScale * data.quickFallGravityMult);
        else
            SetGravityScale(data.gravityScale * data.fallGravityMult);
        #endregion

        #region JUMP CHECKS
        if (isJumping && rb2d.velocity.y < 0)
        {
            isJumping = false;
        }

        //Jump
        if (CanJump() && lastTimePressedJump > 0)
        {
            isJumping = true;
            Jump();
        }
        #endregion
    }

    void FixedUpdate()
    {
        #region DRAG
        if (lastTimeOnGround <= 0)
            Drag(data.amountDrag);
        else
            Drag(data.amountFriction);
        animator.SetBool("IsMoving", false);
        #endregion

        #region RUN
        Run(1);
        #endregion
    }

    #region INPUT CALLBACKS
    public void OnJump(InputHandler.InputArgs args)
    {
        lastTimePressedJump = data.jumpBufferTime;
    }

    public void OnJumpUp(InputHandler.InputArgs args)
    {
        if (CanJumpCut())
            JumpCut();
    }
    #endregion

    public void SetGravityScale(float scale)
    {
        rb2d.gravityScale = scale;
    }

    private void Drag(float amount)
    {
        Vector2 force = amount * rb2d.velocity.normalized;
        force.x = Mathf.Min(Mathf.Abs(rb2d.velocity.x), Mathf.Abs(force.x)); //slows player when they are not pressing button
        force.y = Mathf.Min(Mathf.Abs(rb2d.velocity.y), Mathf.Abs(force.y));
        force.x *= Mathf.Sign(rb2d.velocity.x); //gives direction to apply force
        force.y *= Mathf.Sign(rb2d.velocity.y);

        rb2d.AddForce(-force, ForceMode2D.Impulse); //applies a force against direction of movement
    }

    private void Run(float lerpAmount)
    {
        float targetSpeed = InputHandler.instance.MoveInput.x * data.runMaxSpeed; //calculate wanted direction and desired velocity
        float speedDifference = targetSpeed - rb2d.velocity.x; //calculate difference between current velocity and desired velocity

        #region Acceleration Rate
        float accelRate;

        //gets an acceleration value based on if we are accelerating (including turning) or trying to stop. As well as applying an airborne multiplier.
        if (lastTimeOnGround > 0)
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? data.runAccel : data.runDeccel;
        else
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? data.runAccel * data.accelInAir : data.runDeccel * data.deccelInAir;

        //if we want to run but already going faster than max run speed
        if (((rb2d.velocity.x > targetSpeed && targetSpeed > 0.01f) || (rb2d.velocity.x < targetSpeed && targetSpeed < 0.01f)) && data.doKeepRunMomentum)
        {
            accelRate = 0; //prevent deceleration and conserve current momentum
        }
        #endregion

        #region Velocity Power
        float velPower;
        if (Mathf.Abs(targetSpeed) < 0.01f)
        {
            velPower = data.stopPower;
        }
        else if (Mathf.Abs(rb2d.velocity.x) > 0 && (Mathf.Sign(targetSpeed) != Mathf.Sign(rb2d.velocity.x)))
        {
            velPower = data.turnPower;
        }
        else
        {
            velPower = data.accelPower;
        }
        #endregion

        //applies acceleration to speed difference, then raised to power so acceleration increases with higher speeds, finally multiplies by sign to preserve direction
        float movement = Mathf.Pow(Mathf.Abs(speedDifference) * accelRate, velPower) * Mathf.Sign(speedDifference);
        movement = Mathf.Lerp(rb2d.velocity.x, movement, lerpAmount); //lerp so that we prevent Run from immediataly slowing the player down in situations like wall jump/dash

        rb2d.AddForce(movement * Vector2.right); //applies force to rigidbody, multiplied by Vector2.right so it only affect X axis

        if (InputHandler.instance.MoveInput.x != 0)
        {
            animator.SetBool("IsMoving", true);
            CheckDirectionToFace(InputHandler.instance.MoveInput.x > 0);
        }
    }

    private void Turn()
    {
        // Replacing so FirePoint flips with sprite:
        //Vector3 scale = transform.localScale; //stores scale and flips x axis of the entire gameobject
        //scale.x *= -1;
        //transform.localScale = scale;

        isFacingRight = !isFacingRight;

        // Replacement here:
        transform.Rotate(0f, 180f, 0f);


    }

    private void Jump()
    {
        //ensures we can't call jump multiples times from one press
        lastTimePressedJump = 0;
        lastTimeOnGround = 0;

        #region Perform Jump
        float force = data.jumpForce;
        if (rb2d.velocity.y < 0)
            force -= rb2d.velocity.y;

        rb2d.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        #endregion
    }

    private void JumpCut()
    {
        //applies force down when jump button is released
        rb2d.AddForce(Vector2.down * rb2d.velocity.y * (1 - data.jumpCutMultiplier), ForceMode2D.Impulse);
    }

    #region CHECK METHODS
    public void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != isFacingRight)
            Turn();
    }

    private bool CanJump()
    {
        return lastTimeOnGround > 0 && !isJumping;
    }

    private bool CanJumpCut()
    {
        return isJumping && rb2d.velocity.y > 0;
    }
    #endregion
}

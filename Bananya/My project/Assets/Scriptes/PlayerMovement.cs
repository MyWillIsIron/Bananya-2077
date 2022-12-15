using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerBody;
    private Animator animator;
    private SpriteRenderer sprite;

    private float horizontalDirection;
    [SerializeField] private float moveSpeed = 7f;

    private bool isFacingRight = true;

    [SerializeField] private Transform checkground;
    [SerializeField] private float checkJumpRadius = 0.2f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float jumpForce = 16f;

    private int extraJumps;
    [SerializeField] private int extraJumpsValue = 2;
    [SerializeField] private float extraJumpsForce = 10.0f; // = 0.5f

    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;
    [SerializeField] private Transform Wallcheck;
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private float checkWallRadius = 0.2f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.05f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    private enum MovementState { idle, run, jump, falling }; // Делаем переменную которая имеет все типы анимации, чтобы не писть кучу когда, т.к не может работать сразу 2 анимации падения и бега.
                                                             // private MovementState state = MovementState.idle; // по умолчанию анимация афк

    [SerializeField] private AudioSource jumpSoundEffect; // подключаем звуковые эффекты

    private void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        if (!isWallJumping)
        { 
            playerBody.velocity = new Vector2(moveSpeed * horizontalDirection, playerBody.velocity.y); 
        }
    }

    void Flip()
    {
        if ((horizontalDirection > 0f && isFacingRight == false) || (horizontalDirection < 0f && isFacingRight == true))
        {
            isFacingRight = !isFacingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        }
    }

    private void Update()
    {
        Jumps();
        ExtraJump();
        WallSLide();
        WallJump();

        if (!isWallJumping)
        {
            Flip();
        }
        //UpdateAnimationState();
    }

    private void Jumps()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
        {
            jumpSoundEffect.Play();
            playerBody.velocity = new Vector2(playerBody.velocity.x, jumpForce);
        }

        if (Input.GetKeyUp(KeyCode.Space) && playerBody.velocity.y > 0)
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, playerBody.velocity.y * 0.5f);
        }
    }


    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(checkground.position, checkJumpRadius, whatIsGround);
    }

    void ExtraJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0 && IsGrounded() == false)
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, extraJumpsForce);
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && IsGrounded() == true)
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, extraJumpsForce);
        }
        if (IsGrounded() || IsWalled())
        {
            extraJumps = extraJumpsValue;
        }
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(Wallcheck.position, checkWallRadius, whatIsWall);
    }

    private void WallSLide()
    {
        if (IsWalled() && !IsGrounded() && horizontalDirection != 0f)
        {
            isWallSliding = true;
            playerBody.velocity = new Vector2(playerBody.velocity.x, Mathf.Clamp(playerBody.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }

        else 
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            playerBody.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    { 
        isWallJumping = false;
    }

    //private void UpdateAnimationState()
    //{
    //    MovementState state;

    //    if (dirX > 0f)
    //    {
    //        state = MovementState.run;
    //     //   sprite.flipX = false;

    //    }
    //    else if (dirX < 0f)
    //    {
    //        state = MovementState.run;
    //      //  sprite.flipX = true;
    //    }
    //    else
    //    {
    //        state = MovementState.idle;
    //    }

    //    if (rb.velocity.y > .01f)
    //    {
    //        state = MovementState.jump;
    //    }
    //    else if (rb.velocity.y < -.01f)
    //    {
    //        state = MovementState.falling;
    //    }

    //    anim.SetInteger("state", (int)state);
    //}
    //private CircleCollider2D coll;
    //private bool isGrounded()
    //{
    //    // создаем коробку размером с бокс перса и опускам на 1 ниже чтобы этим кусоком касался перс земли. 
    //    return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, whatIsGround);

    //}

    public bool IsDeath()
    {
        return playerBody.bodyType != RigidbodyType2D.Static;
    }

}

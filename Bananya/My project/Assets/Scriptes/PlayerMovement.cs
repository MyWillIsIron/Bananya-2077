using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
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

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    [SerializeField] private TrailRenderer tailDash;

    private enum MovementState { idle, run, jump, falling }; // Делаем переменную которая имеет все типы анимации, чтобы не писть кучу когда, т.к не может работать сразу 2 анимации падения и бега.
                                                             // private MovementState state = MovementState.idle; // по умолчанию анимация афк

    [SerializeField] private AudioSource jumpSoundEffect; // подключаем звуковые эффекты

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        if (!isWallJumping)
        { 
            rb.velocity = new Vector2(moveSpeed * horizontalDirection, rb.velocity.y); 
        }
    }

    void Flip()
    {
        if ((horizontalDirection > 0f && isFacingRight == false) || (horizontalDirection < 0f && isFacingRight == true))
        {
            isFacingRight = !isFacingRight;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);

        }
    }

    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        Jumps();
        ExtraJump();
        WallSLide();
        WallJump();
        Dashing();

        if (!isWallJumping)
        {
            Flip();
        }
        UpdateAnimationState();
    }

    private void Jumps()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(checkground.position, checkJumpRadius, whatIsGround);
    }

    private void ExtraJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0 && IsGrounded() == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, extraJumpsForce);
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && IsGrounded() == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, extraJumpsForce);
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
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
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
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
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

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f); //transform.localScale.y * dashingPower
        tailDash.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tailDash.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void Dashing()
    {
        if ((Input.GetKeyDown(KeyCode.LeftShift)) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (horizontalDirection > 0f)
        {
            state = MovementState.run;
            //   sprite.flipX = false;

        }
        else if (horizontalDirection < 0f)
        {
            state = MovementState.run;
            //  sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .01f)
        {
            state = MovementState.jump;
        }
        else if (rb.velocity.y < -.01f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    public bool IsDeath()
    {
        return rb.bodyType != RigidbodyType2D.Static;
    }

}

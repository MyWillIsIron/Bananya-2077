using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayersMovement : MonoBehaviour
{
    [SerializeField] private Transform checkground;
    [SerializeField] private float checkJumpRadius = 0.2f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float jumpForce = 14f;

    private float jumpTimeCounter;
    [SerializeField] private float jumpTime;
    private bool isJumping;

    private int extraJumps;
    [SerializeField] private int extraJumpsValue;
    [SerializeField] private float extraJumpsForce; // = 0.5f

    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;
    [SerializeField] private Transform Wallcheck;
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private float checkWallRadius = 0.2f;

    private void Update()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal"); //GetAxisRaw мнгновенно возвращается к 0, а GetAxis постепенно
        

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
        {
            jumpSoundEffect.Play();
            jumpTimeCounter = jumpTime;
            isJumping = true;
            playerBody.velocity = new Vector2(playerBody.velocity.x, jumpForce);
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                
                playerBody.velocity = new Vector2(playerBody.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;

            }
            else 
            {
                isJumping = false;
            }
            
        }
        if (Input.GetKeyUp(KeyCode.Space))
            {
                isJumping = false;
            }
            

        ExtraJump();
        WallSLide();

        Flip();
        UpdateAnimationState();
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
        if (IsGrounded() == true)
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
        if (IsWalled() && !IsGrounded()) // && horizontalDirection != 0f
        {
            isWallSliding = true;
            playerBody.velocity = new Vector2(playerBody.velocity.x, Mathf.Clamp(playerBody.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else 
        {
            isWallSliding = false;
        }
    }


}
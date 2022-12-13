using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayersMovement : MonoBehaviour
{
    private Rigidbody2D playerBody;
    private Animator animator;
    private SpriteRenderer sprite;

    private float horizontalDirection;
    [SerializeField] private float moveSpeed = 7f;

    private bool isFacingRight = true;

    private enum MovementState { idle, run, jump, falling }; // ������ ���������� ������� ����� ��� ���� ��������, ����� �� ����� ���� �����, �.� �� ����� �������� ����� 2 �������� ������� � ����.
                                                             // private MovementState state = MovementState.idle; // �� ��������� �������� ���

    [SerializeField] private AudioSource jumpSoundEffect; // ���������� �������� �������

    private void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        if (!IsWalled())
        { 
            playerBody.velocity = new Vector2(moveSpeed * horizontalDirection, playerBody.velocity.y); 
        };
    }

    void Flip()
    {
        if ((horizontalDirection > 0f && isFacingRight == false) || (horizontalDirection < 0f && isFacingRight == true))
        {
            isFacingRight = !isFacingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            
        }
    }
    private void UpdateAnimationState()
    {
        MovementState state = MovementState.idle;
        // ��� �������� �� �� ������ �������� ������ ��������� - �� ������ ��� �������
        //if (xDirection > 0f)
        //{
        //    state = MovementState.run;
        //    sprite.flipX = false;

        //}
        //else if (xDirection < 0f)
        //{
        //    state = MovementState.run;
        //    sprite.flipX = true;
        //}
        //else
        //{
        //    state = MovementState.idle;
        //}

        if (playerBody.velocity.y > .01f)
        {
            state = MovementState.jump;
        }
        else if (playerBody.velocity.y < -.01f)
        {
            state = MovementState.falling;
        }

        animator.SetInteger("state", (int)state);
    }
}

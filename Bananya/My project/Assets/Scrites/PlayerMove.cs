using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    //private BoxCollider2D coll;
    private CircleCollider2D coll;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    [SerializeField] private LayerMask jumpableGround;

    //    [SerializeField] ��� �������� ��������� ���� �����



    private enum MovementState { idle, run, jump, falling }; // ������ ���������� ������� ����� ��� ���� ��������, ����� �� ����� ���� �����, �.� �� ����� �������� ����� 2 �������� ������� � ����.
                                                             // private MovementState state = MovementState.idle; // �� ��������� �������� ���

    [SerializeField] private AudioSource jumpSoundEffect; // ���������� �������� �������
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

//        coll = GetComponent<BoxCollider2D>();
        coll = GetComponent<CircleCollider2D>();
    }


    // private int doubleJump = 1;

    private bool doubleJump = true;

    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal"); //GetAxisRaw ���������� ������������ � 0, � GetAxis ����������

        if (isDeath()) 
        rb.velocity = new Vector2(moveSpeed * dirX, rb.velocity.y);

        //if (Input.GetButtonDown("Jump") && doubleJump > 0)
        //{
        //        rb.velocity = new Vector2(0, jumpForce);
        //        doubleJump--;
        //        Debug.Log("������ " + doubleJump);
        //}

        //if (isGrounded())
        //{
        //    doubleJump = 1;
        //}


        //Flip player when moving left-right
        if (dirX > 0.01f)
            transform.localScale = Vector3.one;
        else if (dirX < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);






        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(0, jumpForce);
          
           
        }
        if (Input.GetButtonDown("Jump") && !isGrounded() && doubleJump)
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(0, jumpForce);

            doubleJump = false;
           
        }
        if (isGrounded())
        {
            doubleJump = true;

        }
       


        UpdateAnimationState();

    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.run;
         //   sprite.flipX = false;

        }
        else if (dirX < 0f)
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

    private bool isGrounded()
    {
        // ������� ������� �������� � ���� ����� � ������� �� 1 ���� ����� ���� ������� ������� ���� �����. 
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);

    }

    public bool isDeath()
    {
        return rb.bodyType != RigidbodyType2D.Static;
    }

}

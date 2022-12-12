using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayesMove : MonoBehaviour
{
    //тест, что работает гитхаб
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    private BoxCollider2D coll;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    [SerializeField] private LayerMask jumpableGround;
    //    [SerializeField] дл€ внесени€ изменений черз юнити



    private enum MovementState { idle, run, jump, falling }; // ƒелаем переменную котора€ имеет все типы анимации, чтобы не писть кучу когда, т.к не может работать сразу 2 анимации падени€ и бега.
                                                             // private MovementState state = MovementState.idle; // по умолчанию анимаци€ афк

    [SerializeField] private AudioSource jumpSoundEffect; // подключаем звуковые эффекты
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        coll = GetComponent<BoxCollider2D>();
    }

    private bool doubleJump = true;
    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal"); //GetAxisRaw мнгновенно возвращаетс€ к 0, а GetAxis постепенно
        rb.velocity = new Vector2(moveSpeed * dirX, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(0, jumpForce);
          
            Debug.Log("ѕыржок 1 " + doubleJump);
        }
        if (Input.GetButtonDown("Jump") && !isGrounded() && doubleJump)
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(0, jumpForce);

            doubleJump = false;
            Debug.Log("ѕыржок 2 " + doubleJump);
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
            sprite.flipX = false;

        }
        else if (dirX < 0f)
        {
            state = MovementState.run;
            sprite.flipX = true;
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
        // создаем коробку размером с бокс перса и опускам на 1 ниже чтобы этим кусоком касалс€ перс земли. 
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator anim;

    public int health = 3;

    [SerializeField] private AudioSource audioBox;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform checkground;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        anim.SetTrigger("Enemy_GetDamage");

        Debug.Log('2');
    }

    private void Update()
    {
        if (health <= 0)
        {
            //anim death
            //disable enemy
            Destroy(gameObject);
        }
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioBox.Play();
        }
        if (IsGrounded())
        {
            audioBox.Play();
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(checkground.position, whatIsGround);
    }
}

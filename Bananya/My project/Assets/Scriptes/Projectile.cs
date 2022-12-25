using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Animator anim;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    [SerializeField] private int damage = 1;
    [SerializeField] private float speed = 2f;
    private float direction;
    private float lifetime;
    private bool hit = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
    }

    private void FixedUpdate()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * -direction;
        transform.Translate(-movementSpeed, 0, 0);


        if (direction == 1)
            sprite.flipX = true;
        else
            sprite.flipX = false;


        lifetime += Time.deltaTime;
        if (lifetime > 5)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.gameObject.CompareTag("item") || collision.gameObject.CompareTag("Orange") || collision.gameObject.CompareTag("HearthItem"))
        {
            hit = false;
            boxCollider.enabled = true;
        } else
        {
            hit = true;
            boxCollider.enabled = false;
            if (collision.gameObject.tag == "Arrow")
            {
                Destroy(gameObject);
            } else
            {
                anim.SetTrigger("exp");
            }
            if (collision.gameObject.CompareTag("Enemy"))
            {
                //collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
       
    }

   
    private void Deactivate()
    {
        Destroy(gameObject);

    }
    
    
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  [SerializeField] private float speed = 2f;
    private float direction;
    private float lifetime;
    private bool hit = false;

    private Animator anim;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private int damage = 1;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        sprite = GetComponent<SpriteRenderer>();

    }

    public void SetDirection(float _direction) // сторона запуска 
    {
        lifetime = 0;
        direction = _direction;
    
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;
    }
    

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * -direction;
        transform.Translate(-movementSpeed, 0, 0);
        
        if (direction == 1)
        {
            sprite.flipX = true;
        }
        if (direction == -1)
        {
             sprite.flipX = false;
        }

        lifetime += Time.deltaTime;
        if (lifetime> 5) {
            Destroy(gameObject);
        }

        if (gameObject.CompareTag("Enemy")){
            Debug.Log('1');
            gameObject.GetComponent<EnemyLife>().TakeDamage(damage);
           
        }
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" || collision.gameObject.CompareTag("item") || collision.gameObject.CompareTag("Orange"))
        {
            hit = false;
            boxCollider.enabled = true;
        } 
        else
        {
            hit = true;
            boxCollider.enabled = false;

            anim.SetTrigger("exp");
            collision.gameObject.GetComponent<EnemyLife>().TakeDamage(damage);
        }
    }

    private void Deactivate()
    {
        Destroy(gameObject);

    }
}

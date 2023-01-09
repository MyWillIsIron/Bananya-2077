using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRend;
    [SerializeField] private AudioSource AudioDeath;
    [SerializeField] private int numberOfFlashes;
    [SerializeField] private float iFramesDuration;

    [SerializeField] public int health = 3;
    public int healthMax;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        healthMax = health;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("TrapSpike"))
        {
            health -= health;
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        AudioDeath.Play();  
        health -= damage;
       
        if (health <= 0 )
        {
            Die();    
        } 
        else
        {
            // Анимация урона
            anim.SetTrigger("Player_TakeDamage");
            if (car)
            {
                StartCoroutine(damageSlowing());
            }
            
            
        }

    }
    public void AddHealth()
    {
        if (health < healthMax)
            health++;
    }

    private void Die()
    {
        AudioDeath.Play();
        anim.SetTrigger("death");
        rb.bodyType = RigidbodyType2D.Static;
     //  GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
       // GetComponent<BoxCollider2D>().enabled = false;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private bool car = true;

    private IEnumerator damageSlowing()
    {
        car = false;
        //PlayerMovement speed = new PlayerMovement();
        PlayerMovement speed = GetComponent<PlayerMovement>();
        //Physics2D.IgnoreLayerCollision(10, 11, true);
        float originalSpeed = speed.MoveSpeed;
        speed.MoveSpeed = originalSpeed / 2;
        yield return new WaitForSeconds(0.3f);
        speed.MoveSpeed = originalSpeed;
        //Physics2D.IgnoreLayerCollision(10, 11, false);

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 1, 1, 0.509804f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        car = true;
    }
}

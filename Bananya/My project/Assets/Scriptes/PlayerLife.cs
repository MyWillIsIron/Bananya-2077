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
    [SerializeField] private AudioSource deathSoundEffect;

    public int health = 3;


    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("TrapSpike"))
        {
            deathSoundEffect.Play();
            health -= health;
            Die();
        }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log(health);

        if (health <= 0)
        {
            deathSoundEffect.Play();

            Die();
        } 
        else
        {
            Slowing();
        }

    }

    private IEnumerator damageSlowing()
    {
        //PlayerMovement speed = new PlayerMovement();
        PlayerMovement speed = GetComponent<PlayerMovement>();
        //Physics2D.IgnoreLayerCollision(10, 11, true);
        float originalSpeed = speed.MoveSpeed;
        speed.MoveSpeed = originalSpeed / 2;
        yield return new WaitForSeconds(0.3f);
        speed.MoveSpeed = originalSpeed;
        //Physics2D.IgnoreLayerCollision(10, 11, false);
    }

    private void Slowing()
    {
        StartCoroutine(damageSlowing());
    }

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

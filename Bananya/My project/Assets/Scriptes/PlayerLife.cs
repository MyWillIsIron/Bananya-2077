using System.Collections;
using System.Collections.Generic;
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

  

    // ���� � ������� ��
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Trap"))
    //    {
    //        health--;
    //        Debug.Log(health);
    //        if(health <= 0)
    //        {
    //            deathSoundEffect.Play();
    //            Die();
    //        }

    //    }
    //}

    // ���� ��� ������� ����� �������
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Trap"))
    //    {
    //        health--;
    //        Debug.Log(health);
    //        if (health <= 0)
    //        {
    //            deathSoundEffect.Play();
    //            Die();
    //        }

    //    }
    //}

    // ���� �� �����
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
        } else
        {
            // �������� �����
            anim.SetTrigger("Player_TakeDamage");
        }

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

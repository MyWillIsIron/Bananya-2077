using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    private Animator anim;

    public int health = 3;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        anim.SetTrigger("Enemy_GetDamage");
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
}

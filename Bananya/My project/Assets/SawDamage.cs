using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SawDamage : MonoBehaviour
{
    private int damage = 1;
    private float damageCooldown = 0.45f;
    private float cooldownTimer = Mathf.Infinity;

    private void FixedUpdate()
    {
        cooldownTimer += Time.deltaTime;

       

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (cooldownTimer > damageCooldown)
            {
                cooldownTimer = 0;

                collision.gameObject.GetComponent<PlayerLife>().TakeDamage(damage);
            }
        }
    }
}

    

    



    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.name == "Player")
    //    {
    //        if (cooldownTimer > damageCooldown)
    //        {
    //            cooldownTimer = 0;
    //            //звук резки
    //            collision.GetComponent<PlayerLife>().TakeDamage(damage);
    //        }
    //    }
    //    while (damageOn)
    //    {
    //        collision.GetComponent<PlayerLife>().TakeDamage(damage);
    //    }
    //}





    








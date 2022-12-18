using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawDamage : MonoBehaviour
{
    private int damage = 1;
    private float damageCooldown = 0.25f;
    private float cooldownTimer = Mathf.Infinity;

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    //�������� �� isTrigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (cooldownTimer > damageCooldown)
            {
                cooldownTimer = 0;
                //���� �����
                collision.GetComponent<PlayerLife>().TakeDamage(damage);
            }
       

        }
    }

    //�������� �� �������
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
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class SawDamage : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    private float damageCooldown = 0.23f;
    private float cooldownTimer = Mathf.Infinity;

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

      //  StartCoroutine(damageSlowing());
    }

    //�������� �� isTrigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
           if (cooldownTimer > damageCooldown)
            {
                //�������� ���� �����
                cooldownTimer = 0;
                collision.GetComponent<PlayerLife>().TakeDamage(damage);
                //StartCoroutine(imortalPlayer());
           }
       

        }
    }

    // ��� ����� ������� ���������� ����� �� ������� ���� ����� ��������.
    //IEnumerator imortalPlayer()
    //{
    //    Physics2D.IgnoreLayerCollision(28, 29, true);
    //    yield return new WaitForSeconds(0.24f);
    //    Physics2D.IgnoreLayerCollision(28, 29, false);
    //}



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
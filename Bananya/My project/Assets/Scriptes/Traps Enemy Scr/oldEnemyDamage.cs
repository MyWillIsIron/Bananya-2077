using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oldEnemyDamage : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            collision.GetComponent<PlayerLife>().TakeDamage(damage);
        }
    }
}

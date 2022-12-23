using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    [SerializeField] private float speed = 1;
    [SerializeField] private float resetTime = 2;
    private int damage = 1;
    private float lifetime = 2;
    public void ActivateProjectile()
    {
        lifetime = 0;
        gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {

        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
        if (collision.gameObject.name == "Player" )
            collision.GetComponent<PlayerLife>().TakeDamage(damage);
    }





}

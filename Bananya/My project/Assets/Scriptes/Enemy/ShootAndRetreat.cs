using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAndRetreat : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform target;
    [SerializeField] private float minimumDistance;

    [SerializeField] private GameObject projectile;
    [SerializeField] private float cooldownShots;
    [SerializeField] private float nextShotTime;

    void Update()
    {
        if (Time.time > nextShotTime)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            nextShotTime = Time.time + cooldownShots;
        }


        if (Vector2.Distance(transform.position, target.position) < minimumDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
        }
        else
        {
            //Atack script
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowing : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform target;
    [SerializeField] private float minimumDistance;

    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > minimumDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else 
        {
            //Atack script
        }
    }
}

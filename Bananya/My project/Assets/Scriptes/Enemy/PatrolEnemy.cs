using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float waitTime;
    private int currentPointIndex;

    [SerializeField] private Transform target;

    private bool once;

    void Update()
    {
        if (transform.position.x + 5 < target.position.x)
        {
            if (transform.position != patrolPoints[currentPointIndex].position)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);

            }
            else
            {
                if (once == false)
                {
                    once = true;
                    StartCoroutine(Wait());
                }
            }
        }
        else 
        {
            //if (Vector2.Distance(transform.position, target.position))
            //{
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            //}
        }
    }

    private IEnumerator Wait()
    { 
        yield return new WaitForSeconds(waitTime);

        if (currentPointIndex + 1 < patrolPoints.Length)
        {
            currentPointIndex++;
        }
        else
        {
            currentPointIndex = 0;
        }
        once = false;
    }
}

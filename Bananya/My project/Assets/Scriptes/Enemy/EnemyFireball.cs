using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireball : MonoBehaviour
{
    private Vector3 targetPosition;
    [SerializeField] private float speed;

    void Start()
    {
        targetPosition = FindObjectOfType<PlayerMovement>().transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            Destroy(gameObject);
        }
    }
}

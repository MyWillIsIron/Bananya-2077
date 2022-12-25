using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class EnemyMovementPatrol : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    [SerializeField] private float speed;
    [SerializeField] private float distance;
    private int horizontalDirection = 1;
    [SerializeField] private Transform groundcheck;

    private bool isWaiting;
    [SerializeField] private float waitTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        //if (isWaiting == true)
        //{
        //    return;
        //}

        rb.velocity = new Vector2(speed * horizontalDirection, rb.velocity.y);

        RaycastHit2D groundinfo = Physics2D.Raycast(groundcheck.position, Vector2.down, distance);
        RaycastHit2D wallinfo = Physics2D.Raycast(groundcheck.position, new (horizontalDirection, 0), distance, LayerMask.GetMask("Ground"));

        if ((groundinfo.collider == false) || (wallinfo.collider == true))
        {
            Waiting();
            horizontalDirection = -1 * horizontalDirection;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    private IEnumerator Wait()
    {
        isWaiting = true;
        int original = horizontalDirection;
        horizontalDirection = 0;
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
        horizontalDirection = -original;
    }

    private void Waiting()
    {
        StartCoroutine(Wait());
    }
}

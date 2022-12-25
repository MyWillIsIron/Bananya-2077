using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float ColliderDistance;
    [SerializeField] private int damage;
    private float cooldownTimer = Mathf.Infinity;

    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask PlayerMask;

    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (PlayerInSight())
        {
            if (attackCooldown >= cooldownTimer)
            {

            }
        }


    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = 
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * ColliderDistance, 
            new Vector3(boxCollider.bounds.size.x, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 
            0, Vector2.left, 0, PlayerMask);
        return hit.collider != null;
    }

    private void DrawOnGizmos()
    { 
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * ColliderDistance, new Vector3(boxCollider.bounds.size.x, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}

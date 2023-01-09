using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator anim;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float attackCooldown = 0.55f;
    [SerializeField] private int damage = 1;
    [SerializeField] AudioSource AudioMelee;
    private float cooldownTimer = Mathf.Infinity;


    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && cooldownTimer > attackCooldown && playerMovement.IsDeath())
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        //anim
        AudioMelee.Play();  
        cooldownTimer = 0;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
            Debug.Log("We hit " + enemy.name);
        }

    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

         Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

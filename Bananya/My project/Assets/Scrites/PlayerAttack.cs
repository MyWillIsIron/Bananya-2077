using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 0.25f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject fireballs;
    private Animator anim;
    private PlayerMove PlayerMove;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        PlayerMove = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && PlayerMove.isDeath())
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
      //anim.SetTrigger("attack");
      cooldownTimer = 0;

      GameObject fireballsMake = Instantiate(fireballs, firePoint.position, transform.rotation);
      fireballsMake.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

}

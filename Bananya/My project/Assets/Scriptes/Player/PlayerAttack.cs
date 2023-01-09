using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 0.25f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject fireballs;
    [SerializeField] private AudioSource AudioShoot;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.IsDeath())
        {
            AudioShoot.Play();
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {

      
      anim.SetTrigger("Player_attack");
      cooldownTimer = 0;

      GameObject fireballsMake = Instantiate(fireballs, firePoint.position, transform.rotation);
      fireballsMake.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

}

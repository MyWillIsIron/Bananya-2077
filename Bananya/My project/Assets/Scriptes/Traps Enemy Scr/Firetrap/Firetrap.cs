using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spriteRend;

    [SerializeField] private int damage;
    [SerializeField] private GameObject FireTrapAnim;
    [SerializeField] private AudioSource AudioFire;

    private float actuveTime = 2;
    private float cooldownActivation = 3;
    private Collider2D collisionSave = null;




    private bool activeTrap;
    private float cooldownDamage = 2f;

    private void Awake()
    {
        anim = FireTrapAnim.GetComponentInParent<Animator>();
        spriteRend = FireTrapAnim.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        cooldownActivation -= Time.deltaTime;
        if (cooldownActivation <= 0)
        {
            AudioFire.Play();
            StartCoroutine(ActivateFiretrap());
        }

        cooldownDamage -= Time.deltaTime;
        if (activeTrap && cooldownDamage < 0)
        {
            if (collisionSave != null)
            {
                collisionSave.gameObject.GetComponent<PlayerLife>().TakeDamage(damage);
                cooldownDamage = 2f;
            }
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collisionSave = collision;
            if (activeTrap)
            {
                collision.gameObject.GetComponent<PlayerLife>().TakeDamage(damage);
                cooldownDamage = 2f;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collisionSave = null;
        cooldownDamage = .3f;
    }
    private IEnumerator ActivateFiretrap()
    {
        cooldownActivation = 3;

        spriteRend.color = Color.white;
        activeTrap = true;
        anim.SetBool("activated", true);
        

        yield return new WaitForSeconds(actuveTime);
        activeTrap = false;
        anim.SetBool("activated", false);
    }


}

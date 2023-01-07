using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int countOrange = 0;

    [SerializeField] private Text orangeText;
    [SerializeField] private AudioSource collectorOrangeSound;
    [SerializeField] private AudioSource collectorHearthSound;
    [SerializeField] private PlayerLife playerHeath;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Orange"))
        {
            collectorOrangeSound.Play();
            Destroy(collision.gameObject);
            countOrange++;

            orangeText.text = "Cherries: " + countOrange;
        }

        
         if (collision.gameObject.CompareTag("HearthItem"))
          {
                if (playerHeath.health < playerHeath.healthMax)
                {
                    gameObject.GetComponent<PlayerLife>().AddHealth();
                    collectorHearthSound.Play();
                    Destroy(collision.gameObject);
                }
         }
    }
}


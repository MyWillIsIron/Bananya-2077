using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int countOrange = 0;

    [SerializeField] private Text orangeText;
    [SerializeField] private AudioSource collectorSoundEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Orange"))
        {
            collectorSoundEffect.Play();
            Destroy(collision.gameObject);
            countOrange++;

            orangeText.text = "Cherries: " + countOrange;
        }
    }
}


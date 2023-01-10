using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    [SerializeField] private AudioSource audioPlatform;
    public void Start()
    {
        audioPlatform.Play();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player") // если платформа столнется с какимто коладером и если он = плэеру то сработает 
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player") 
        {
            collision.gameObject.transform.SetParent(null);
        }
    }


}

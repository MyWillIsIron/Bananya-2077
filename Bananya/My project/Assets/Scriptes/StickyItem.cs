using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("item")) // если платформа столнется с какимто коладером и если он = плэеру то сработает 
        {
            collision.gameObject.transform.SetParent(transform);

        }

    }
}

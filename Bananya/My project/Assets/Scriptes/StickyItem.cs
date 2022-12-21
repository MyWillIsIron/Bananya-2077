using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("item")) 
            collision.gameObject.transform.SetParent(transform);

        }

    }


 




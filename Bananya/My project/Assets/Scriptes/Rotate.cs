using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private AudioSource AudioSaw;

   private void Start()
    {
        AudioSaw.Play();
    }
    private  void Update()
    {
        transform.Rotate(0, 0, 360 * speed * Time.deltaTime);
    } 
    
}

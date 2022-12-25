using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Healtbar : MonoBehaviour
{

    [SerializeField] private Health health;
    [SerializeField] private Image totalhealthbar;
    [SerializeField] private Image currenthealthbar;
    //private void Start()
    //{
    //    totalhealthbar.fillAmount = health.currentHealth / 10;
    //}
    private void Update()
    {
        currenthealthbar.fillAmount = health.currentHealth / 10;  
    }
}

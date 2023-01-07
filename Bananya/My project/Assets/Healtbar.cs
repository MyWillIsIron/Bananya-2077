using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Healtbar : MonoBehaviour
{

    [SerializeField] private PlayerLife playerHeath;
    [SerializeField] private Image totalhealthbar;
    [SerializeField] private Image currenthealthbar;
    private void Start()
    {
        float healthStart = playerHeath.health;
        totalhealthbar.fillAmount = healthStart / 10;
    }
    private void Update()
    {
        float health = playerHeath.health;

        currenthealthbar.fillAmount = health / 10;  
    }
}

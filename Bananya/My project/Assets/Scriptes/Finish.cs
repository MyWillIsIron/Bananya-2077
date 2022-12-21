using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{


    private Animator anim;
    private AudioSource finishSound;
    private bool levelCompleted = false;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        finishSound = GetComponent<AudioSource>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelCompleted)
        {
            anim.SetTrigger("Finish");
            finishSound.Play();
            levelCompleted = true;
            Invoke("CompleteLevel", 2f);
           
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
 }

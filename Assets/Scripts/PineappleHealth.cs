using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineappleHealth : MonoBehaviour
{

    Animator animator;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //if (Player.currentHealth <= 4)
            //{
            //    Player.currentHealth += 2;

            //}
            //else if (Player.currentHealth == 5)
            //{
            //    Player.currentHealth++;
            //}

            Player.currentHealth = 6;
            animator.SetBool("isCollected",true);
            Destroy(gameObject,0.5f);
        }
    }
}

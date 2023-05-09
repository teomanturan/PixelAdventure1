using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Apple : MonoBehaviour
{
    Text currentScoreText;
    Animator animator;
    AudioSource audioSource;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        currentScoreText = GameObject.Find("Current Score Text").GetComponent<Text>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentScoreText.text = "Score: " + Player.score.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DestroyingObject(animator, currentScoreText);
        }
    }

    #region Puan Toplama

    public void DestroyingObject(Animator animator, Text currentScoreText)
    {
        audioSource.Play();
        animator.SetBool("Collected", true);
        Destroy(gameObject, 0.5f);
        HowManyPoints();
        currentScoreText.text = "Score: " + Player.score.ToString();
    }
    #endregion

    #region Kaç Puan Olduðunu Hesaplama
    
    public void HowManyPoints()
    {
        switch (gameObject.name)
        {
            case "Apple":
                Player.score += 5;
                break;

            case "Banana":
                Player.score += 10;
                break;

            case "Cherry":
                Player.score += 15;
                break;

            case "Strawberry":
                Player.score += 20;
                break;
        }
    }

    #endregion
}//Class
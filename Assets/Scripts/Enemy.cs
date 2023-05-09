using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    Text CurrentScoreText;
    Rigidbody2D rb;
    private void Awake()
    {
        CurrentScoreText = GameObject.Find("Current Score Text").GetComponent<Text>();
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Kill(collision);
    }

    #region Düþman Öldürme
    public void Kill(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.score += 10;
            CurrentScoreText.text = "Score: " + Player.score.ToString();
            rb.velocity = new Vector2(rb.velocity.x, Jump.jumpSpeed);
            transform.parent.GetComponent<Animator>().SetBool("IsDead", true);
            Destroy(transform.parent.gameObject,0.5f);
            
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            transform.parent.GetComponent<Animator>().SetBool("IsDead", true);
            Destroy(transform.parent.gameObject, 0.5f);
        }
    }
    #endregion
}

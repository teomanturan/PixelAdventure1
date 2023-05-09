using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    bool isFalling;
    public float fallSpeed = 10f;
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (isFalling)
        {
            rb.velocity = Vector2.down * Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("goingDown");
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }
    }
}

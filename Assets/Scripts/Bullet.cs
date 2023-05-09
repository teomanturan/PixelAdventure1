using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    Animator shurikenAnimator;

    public float bulletSpeed = 500f;
    bool isHitting = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isHitting)
        {
            rb.velocity = transform.right * bulletSpeed * Time.deltaTime;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        if(transform.position.x > 6 || transform.position.x < -6)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Vurdun knk");
            Destroy(gameObject,0.2f);
            isHitting = true;
            shurikenAnimator.SetBool("isHitting", isHitting);
            collision.transform.parent.GetComponent<Animator>().SetBool("IsDead", true);
            Destroy(collision.transform.parent.gameObject,0.5f);
        }
        if (collision.gameObject.tag == "Terrain" || collision.gameObject.tag == "Trap" || collision.gameObject.tag == "NonKillableEnemy")
        {
            isHitting = true;
            Destroy(gameObject,0.5f); 
            shurikenAnimator.SetBool("isHitting", isHitting);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField]
    LayerMask terrainLayer,trambolineLayer;

    [SerializeField]
    Rigidbody2D rb;
    AudioSource audioSource;

    public static bool isGround = true;
    public static float jumpSpeed = 5f;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Player.isStart)
        {
            return;
        }

        RaycastHit2D isHitting = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, terrainLayer);
        RaycastHit2D isHittingTramb = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, trambolineLayer);
        
        if (isHitting)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }

        if(isGround && Input.GetKeyDown(KeyCode.Space) && !Player.isDead)
        {
            JumpAction();
        }
        if (isHittingTramb)
        {
            JumpAction();
        }
    }

    #region Zýplama Ýþlemi

    void JumpAction()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        audioSource.Play();
    }

    #endregion
}

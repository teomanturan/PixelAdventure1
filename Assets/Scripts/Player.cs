using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    
    [SerializeField] Text lastScoreText,highScoreText,startHighScoreText,currentScoreText;
    [SerializeField] GameObject RestartPanel, StartPanel, DeathPanel;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameManager gameManager;
    GameObject fan;
    Rigidbody2D rb;
    Animator animator;
    Collider2D playerCollider;
    AudioSource audioSource;
    Transform bulletSpawner;
    public bool isRunning;
    public float movementSpeed = 7;
    public static bool isDead, isStart;
    public static int score;
    public static bool isFacingRight = true;
    public static int maxHealth = 6;
    public static int currentHealth;
    public static int ammoCount = 3;
    public float fanSpeed = 5f;
    public int highScore = 0;

    private void Awake()
    {
        playerCollider = GetComponent<Collider2D>();
        bulletSpawner = transform.GetChild(0);
        audioSource = gameObject.GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        fan = GameObject.FindGameObjectWithTag("Fan");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.isContinuing)
        {
            score = PlayerPrefs.GetInt("CurrentLevelScore");
            currentScoreText.text = "Score: " + score.ToString();
        }
        if (GameManager.isRestarted)
        {
            StartPanel.SetActive(false);
        }
        currentScoreText.text = "Score: " + score.ToString();
        startHighScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("LastLevel"))
        {
            PlayerPrefs.SetInt("LastLevel", SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void Update()
    {
        if (!isStart)
        {
            return;
        }
        if (!isDead)
        {
            Fire();
        }
        Debug.Log("Score: " + score);
        Debug.Log("levelscore: " + PlayerPrefs.GetInt("CurrentLevelScore"));
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!isStart)
        {
            return;
        }
        if (!isDead)
        {
            float h = Input.GetAxis("Horizontal");
            Mover(h);
            Direction(h);
            RunAnimation(h);
            JumpAnimation(Jump.isGround);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EndPoint"))
        {
            PlayerPrefs.SetInt("CurrentLevelScore", score);
            PlayerPrefs.SetInt("LevelFinishHealth", currentHealth);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            isFacingRight = true;
        }
        if (collision.gameObject.CompareTag("AmmoFiller"))
        {
            ammoCount = 3;
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("NonKillableEnemy"))
        {
            audioSource.Play();
            TakeDamage();
            Collider2D colliderEnemy = collision.gameObject.GetComponent<Collider2D>();
            colliderEnemy.enabled = false;
        }
    }

    #region Fan Mekaniði
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Fan")){
            rb.AddForce(fan.transform.up * fanSpeed);
        }
    }
    #endregion

    #region Hareket Etme

    void Mover(float h)
    {
        rb.velocity = new Vector2(h * movementSpeed, rb.velocity.y);
    }

    #endregion

    #region Saða ve Sola Bakma

    void Direction(float h)
    {
        if (h > 0 && !isFacingRight)
        {
            transform.Rotate(0f, 180f, 0f);
            isFacingRight = true;

        }
        else if (h < 0 && isFacingRight)
        {
            transform.Rotate(0f, 180f, 0f);
            isFacingRight = false;
        }
    }

    #endregion

    #region Koþma Animasyonu

    void RunAnimation(float h)
    {
        if (h != 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        animator.SetBool("isRunning", isRunning);
    }

    #endregion

    #region Delayli Ölüm ve Restart

    IEnumerator DelayedDeath()
    {
        {
            yield return new WaitForSecondsRealtime(1.5f);
            Debug.Log("Ha burdayim");
            Destroy(gameObject);
            if(currentHealth > 0)
            {
                RestartPanel.SetActive(true);
            }
            else
            {
                RestartPanel.SetActive(true);
                PlayerPrefs.SetInt("LastLevel", 0);
            }
        }
    }

    #endregion

    #region Can Kaybý
    
    void Death()
    {
        isDead = true;
        animator.SetBool("isDead", isDead);
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;

        if(score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
            Debug.Log("HighScore: " + PlayerPrefs.GetInt("HighScore"));
        }
        lastScoreText.text = "Last Score: " + score.ToString();
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
        score = PlayerPrefs.GetInt("CurrentLevelScore");
    }

    #endregion

    #region Hasar Yeme
    void TakeDamage()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            Death();
            Debug.Log("Tamamen öldün.");
            StartCoroutine(DelayedDeath());
            PlayerPrefs.SetInt("CurrentLevelScore", 0);
            score = 0;
        }
        else
        {
            Death();
            Debug.Log("Tam ölmedin raad ol.");
            StartCoroutine(DelayedDeath());
        }
        Debug.Log(currentHealth);
    }
    #endregion

    #region Zýplama Animasyonu

    void JumpAnimation(bool isGround)
    {
        animator.SetBool("isJumping", !isGround);
    }

    #endregion

    #region Ateþ Etme

    void Fire()
    {
        if (ammoCount > 0)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Instantiate(bulletPrefab, bulletSpawner.position, bulletSpawner.rotation);
                ammoCount--;
            }
        }
    }

    #endregion

    #region Oyuna Baþlama
     
    public void PlayGame()
    {
        isStart = true;
        score = 0;
        StartPanel.SetActive(false);
        currentHealth = maxHealth;
        ammoCount = 3;
    }

    #endregion
}//CLASS

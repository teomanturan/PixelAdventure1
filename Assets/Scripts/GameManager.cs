using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isRestarted = false;
    public static bool isContinuing = false;

    #region Kaldýðýn Leveldan Devam Etme

    public void Continue()
    {
        Debug.Log("Bu leveldasýn" + PlayerPrefs.GetInt("LastLevel"));
        SceneManager.LoadScene(PlayerPrefs.GetInt("LastLevel"));
        Player.isStart = true;
        isContinuing = true;
        Player.score = PlayerPrefs.GetInt("CurrentScore");
        Player.currentHealth = PlayerPrefs.GetInt("LevelFinishHealth");
    }

    #endregion

    #region Yeniden Baþlama
    public void RestartGame()
    {
        isRestarted = true;
        isContinuing = false;
        Player.isDead = false;
        Player.isFacingRight = true;
        if (Player.currentHealth <= 0)
        {
            Debug.Log("ÝLK LEVELA GÝT LAN");
            Player.currentHealth = Player.maxHealth;
            SceneManager.LoadScene("Level1");
        }
        else
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    #endregion

    #region Oyundan Çýkýþ
    public void QuitGame()
    {

        Application.Quit();
    }
    #endregion

}

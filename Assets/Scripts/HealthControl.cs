using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthControl : MonoBehaviour
{
    public Image[] healthBars;
    private void Awake()
    {
        healthBars = GetComponentsInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthController();
    }



    #region Healthbar Kontrolü

    void HealthController()
    {
        switch (Player.currentHealth)
        {
            case 6:
                Control(1f, 1f, 1f);
                break;
            case 5:
                Control(1f, 1f, 0.5f);
                break;
            case 4:
                Control(1f, 1f, 0f);
                break;
            case 3:
                Control(1f, 0.5f, 0f);
                break;
            case 2:
                Control(1f, 0f, 0f);
                break;
            case 1:
                Control(0.5f, 0f, 0f);
                break;
            case 0:
                Control(0f, 0f, 0f);
                break;
        }
    }

    #endregion

    #region Healthbar Doluluk Ayarlama
    void Control(float h0,float h1, float h2)
    {
        healthBars[0].fillAmount = h0;
        healthBars[1].fillAmount = h1;
        healthBars[2].fillAmount = h2;
    }

    #endregion
}

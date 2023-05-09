using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoController : MonoBehaviour
{
    Image[] shurikenImages;

    private void Awake()
    {
        shurikenImages = GetComponentsInChildren<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AmmoControl();
    }
    #region Mermi Sayýsý Kontrolü

    void AmmoControl()
    {
        switch (Player.ammoCount)
        {
            case 3:
                AmmoFillControl(true, true, true);
                break;
            case 2:
                AmmoFillControl(true, true, false);
                break;
            case 1:
                AmmoFillControl(true, false, false);
                break;
            case 0:
                AmmoFillControl(false, false, false);
                break;
        }
    }

    #endregion

    #region Mermi Doluluk Ayarlama Kontrolü
    
    void AmmoFillControl(bool h0,bool h1,bool h2)
    {
        shurikenImages[0].enabled = h0;
        shurikenImages[1].enabled = h1;
        shurikenImages[2].enabled = h2;
    }
    
    #endregion
}

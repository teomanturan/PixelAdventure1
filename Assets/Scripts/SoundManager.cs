using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Image soundOnImage,soundOffImage;
    private bool muted;

    private void Start()
    {
        muted = PlayerPrefs.GetInt("Muted") == 1;
        ImageChange();
        AudioListener.pause = muted;
    }

    public void MuteButton()
    {
        muted = !muted;
        AudioListener.pause = muted;
        if (muted)
        {
            PlayerPrefs.SetInt("Muted", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Muted", 0);
        }
        ImageChange();
    }

    void ImageChange()
    {
        if (muted)
        {
            soundOnImage.enabled = false;
            soundOffImage.enabled = true;
        }
        else
        {
            soundOnImage.enabled = true;
            soundOffImage.enabled = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayUI : MonoBehaviour
{
    // an image that represents the settings menu
    [Tooltip("the image that represents the settings menu")]
    [SerializeField]
    private Image SettingsOverlay = null;


    // an image that represents the current head selected
    [Tooltip("image that represents the head selection")]
    [SerializeField]
    private Image Head = null;

    [Tooltip("A reference to the text coutner.")]
    [SerializeField]
    private Text TextCounter = null;

    [Tooltip("A reference to the timer counter.")]
    [SerializeField]
    private Text TimerText = null;


    public void SettingButton()
    {
        PauseScript.TogglePause();
        SettingsOverlay.gameObject.SetActive(!SettingsOverlay.gameObject.activeSelf);
    }


    public void SetHeadImage(Sprite NewImage)
    {
        Head.sprite = NewImage;
    }


    public void SetCounter(int Counter)
    {
        TextCounter.text = Counter.ToString();
    }


    public void SetTimer(float Timer)
    {
        TimerText.text = Timer.ToString("F4");
    }
}

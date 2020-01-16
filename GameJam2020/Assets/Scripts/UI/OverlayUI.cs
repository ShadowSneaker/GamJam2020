using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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


    [Tooltip("The text for the mute sounds button")]
    [SerializeField]
    private Text MuteText = null;

    [Tooltip("The Sound that you want the button to make")]
    [SerializeField]
    private SoundScript UISounds;

    // a refernce to the attached transition script
    private TransitionScript Transition;

    public void Start()
    {
        Transition = GetComponent<TransitionScript>();
    }

    public void SettingButton()
    {
        UISounds.Play();
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

    public void LoadScene(string SceneName)
    {
       
        Transition.SceneName = SceneName;
        Transition.LoadScene();
    }

    public void Replay()
    {
        
        Transition.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MuteGame()
    {
        UISounds.Play();
        AudioListener.pause = !AudioListener.pause;
        AudioListener.volume = (AudioListener.pause) ? 0.0f : 1.0f;

        if(AudioListener.pause)
        {
            MuteText.text = "UnMute Sounds";
        }
        else
        {
            MuteText.text = "Mute Sounds";
        }
    }

}

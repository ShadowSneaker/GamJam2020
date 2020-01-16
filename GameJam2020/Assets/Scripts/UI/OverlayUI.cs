using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OverlayUI : MonoBehaviour
{
    [Tooltip("image that represents the head selection")]
    [SerializeField]
    private Image Head = null;

    [Tooltip("A reference to the text coutner.")]
    [SerializeField]
    private Text TextCounter = null;

    [Tooltip("A reference to the timer counter.")]
    [SerializeField]
    private Text TimerText = null;

    [Tooltip("The Sound that you want the button to make")]
    [SerializeField]
    private SoundScript UISounds;

    [Tooltip("A reference to the settings UI prefab.")]
    [SerializeField]
    private Canvas SettingsUI = null;


    private Canvas SettingsInst = null;


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
        if (PauseScript.GamePaused)
        {
            SettingsInst = Instantiate(SettingsUI);
        }
        else
        {
            Destroy(SettingsInst.gameObject);
        }
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
    }

}

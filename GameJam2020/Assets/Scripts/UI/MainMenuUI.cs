using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Tooltip("The Sprite that you want for when the game is muted")]
    [SerializeField]
    private Sprite Muted = null;

    [Tooltip("the sprite that you want for when the game is unmuted")]
    [SerializeField]
    private Sprite UnMuted = null;

    [Tooltip("The Image for the Sounds Button")]
    [SerializeField]
    private Image SoundButton = null;

    [Tooltip("The Sound You want the butttons to make when pressed")]
    [SerializeField]
    private SoundScript UISounds = null;



    public void Awake()
    {
        SoundButton.sprite = (AudioListener.pause) ? Muted : UnMuted;
        UISounds.SetAudio(GetComponent<AudioSource>());
    }


    //a function to exit the game when pressed
    public void ExitGame()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    //a function to open up the settings menu
    public void OpenSettings()
    {
        UISounds.Play();
    }

    public void SoundAudio()
    {
        UISounds.Play();

        AudioListener.pause = !AudioListener.pause;
        AudioListener.volume = (AudioListener.pause) ? 0.0f : 1.0f;

        // have it change image
        if (AudioListener.pause)
        {
            SoundButton.sprite = Muted;
        }
        else
        {
            SoundButton.sprite = UnMuted;
        }

    }

}

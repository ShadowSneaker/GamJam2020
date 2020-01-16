using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    // an image which is the settings menu
    [Tooltip("The Image for the settings menu")]
    [SerializeField]
    private Image SettingsMenu = null;

    [Tooltip("The Sprite that you want for when the game is muted")]
    [SerializeField]
    private Sprite Muted = null;

    [Tooltip("the sprite that you want for when the game is unmuted")]
    [SerializeField]
    private Sprite UnMuted = null;

    [Tooltip("The Image for the Sounds Button")]
    [SerializeField]
    private Image SoundButton = null;


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
        SettingsMenu.gameObject.SetActive(!SettingsMenu.gameObject.activeSelf);
    }

    public void SoundAudio()
    {
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

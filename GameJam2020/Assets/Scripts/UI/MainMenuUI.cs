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

}

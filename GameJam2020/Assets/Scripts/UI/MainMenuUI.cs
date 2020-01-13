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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //function to load a scene depending on the parameter put in
    public void LoadScene(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
    }

    //a function to exit the game when pressed
    public void ExitGame()
    {
        Application.Quit();
    }

    //a function to open up the settings menu
    public void OpenSettings()
    {
        SettingsMenu.gameObject.SetActive(!SettingsMenu.gameObject.activeSelf);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUI : MonoBehaviour
{

    // the level that is to be loaded
    public string TheLevelName;


    public Text TitleText;

    public void Start()
    {
        TitleText.text = TheLevelName;
    }


    public void PlayLevel()
    {
        SceneManager.LoadScene(TheLevelName);
    }

    public void ExitUI()
    {
        
       
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUI : MonoBehaviour
{

    // the level that is to be loaded
    public string TheLevelName;

    [Tooltip("A reference to the Level name text.")]
    public Text TitleText;

    // A reference to the attached transition script.
    private TransitionScript Transition = null;



    public void Start()
    {
        TitleText.text = TheLevelName;
        Transition = GetComponent<TransitionScript>();
        Transition.SceneName = TheLevelName;
    }


    public void PlayLevel()
    {
        Transition.LoadScene();
    }

    public void ExitUI()
    {
        
       
    }

}

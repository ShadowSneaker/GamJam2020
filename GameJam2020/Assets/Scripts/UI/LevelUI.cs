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


    [Tooltip("The sound for clicking the buttons on the UI")]
    [SerializeField]
    private SoundScript UISound = null;


    public void Start()
    {
        TitleText.text = TheLevelName;
        Transition = GetComponent<TransitionScript>();
        Transition.SceneName = TheLevelName;
    }


    public void PlayLevel()
    {
        UISound.Play();
        GameObject.FindGameObjectWithTag("Player").SetActive(false);
        Transition.LoadScene();
    }

    public void ExitUI()
    {
        
       
    }

}

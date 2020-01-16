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


    [Header("Level Info")]

    [Tooltip("A reference to the best time completed.")]
    [SerializeField]
    private Text BestTime = null;

    [Tooltip("A reference to the best score.")]
    [SerializeField]
    private Text BestScore = null;

    [Tooltip("A reference to the three stars for the level.")]
    [SerializeField]
    private Image[] StarImages = new Image[3];


    public void Awake()
    {
        Transition = GetComponent<TransitionScript>();
        UISound.SetAudio(GetComponent<AudioSource>());

        for (int i = 0; i < StarImages.Length; ++i)
        {
            StarImages[i].enabled = false;
        }
    }


    public void UpdateUI(Saving.LevelInfo Info)
    {
        if (Info != null)
        {
            for (int i = 0; i < StarImages.Length; ++i)
            {
                StarImages[i].enabled = (Info.StarCount > i);
            }
            BestScore.text = Info.Score.ToString();
            BestTime.text = Info.Time.ToString("F4");
        }
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


    public void SetLevelName(string LevelName)
    {
        TitleText.text = LevelName;
        Transition.SceneName = LevelName;
    }

}

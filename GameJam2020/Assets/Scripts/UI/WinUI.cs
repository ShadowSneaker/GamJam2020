using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinUI : MonoBehaviour
{

    [Tooltip("A reference to the score text.")]
    [SerializeField]
    private Text ScoreText = null;

    [Tooltip("A reference to the duration text.")]
    [SerializeField]
    private Text DurationText = null;

    [Tooltip("A reference to the Start images.")]
    [SerializeField]
    private Image[] StarImages = new Image[3];

    // A reference to the attached transition script.
    TransitionScript Transition = null;

    [Tooltip("The Sound for when the level is completed")]
    [SerializeField]
    private SoundScript WinSound = null;

    private void Start()
    {
        WinSound.Play();
        Transition = GetComponent<TransitionScript>();
    }


    public void UpdateInfo(float Duration, float Score)
    {
        DurationText.text = " " + Duration.ToString("F4");
        ScoreText.text = " " + Score.ToString();
    }


    public void SetStars(int Count)
    {
        for (int i = 0; i < StarImages.Length; ++i)
        {
            StarImages[i].enabled = (Count > i);
        }
    }


    public void LoadMainMenu()
    {
        Transition.LoadScene("MainMenu");
    }


    public void NextLevel()
    {
        Transition.LoadScene();
    }


    public void Replay()
    {
        Transition.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void SetNextScene(string Name)
    {
        Transition.SceneName = Name;
    }
}

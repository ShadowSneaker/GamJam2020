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

    // A reference to the attached transition script.
    TransitionScript Transition = null;


    private void Start()
    {
        Transition = GetComponent<TransitionScript>();
    }


    public void UpdateInfo(float Duration, float Score)
    {
        DurationText.text = " " + Duration.ToString();
        ScoreText.text = " " + Score.ToString();
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

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

    [Tooltip("A reference to the best duration text.")]
    [SerializeField]
    private Text BestDurationText = null;

    [Tooltip("A reference to the Start images.")]
    [SerializeField]
    private Image[] StarImages = new Image[3];

    // A reference to the attached transition script.
    TransitionScript Transition = null;

    [Tooltip("The Sound for when the level is completed")]
    [SerializeField]
    private SoundScript WinSound = null;

    [Tooltip("The sound that plays when a button is clicked.")]
    [SerializeField]
    private SoundScript UISound = null;

    private GameObject BackgroundMusic = null;
    private AudioSource Audio = null;



    private void Start()
    {
        Audio = GetComponent<AudioSource>();
        WinSound.SetAudio(Audio);
        WinSound.Play();
        Transition = GetComponent<TransitionScript>();

        BackgroundMusic = GameObject.FindGameObjectWithTag("MusicManager");
        BackgroundMusic.SetActive(false);
    }


    public void UpdateInfo(float Duration, float BestDuration, float Score)
    {
        BestDurationText.text = " " + BestDuration.ToString("F4");
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
        Audio.Pause();
        BackgroundMusic.SetActive(true);
        UISound.PlayNew(transform.position);
        Transition.LoadScene("MainMenu");
    }


    public void NextLevel()
    {
        Audio.Pause();
        BackgroundMusic.SetActive(true);
        UISound.PlayNew(transform.position);
        Transition.LoadScene();
    }


    public void Replay()
    {
        Audio.Pause();
        BackgroundMusic.SetActive(true);
        UISound.PlayNew(transform.position);
        Transition.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void SetNextScene(string Name)
    {
        Transition.SceneName = Name;
    }
}

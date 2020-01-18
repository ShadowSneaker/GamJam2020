using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;


public class UIScript : MonoBehaviour
{
    [Tooltip("The icon for muting the game.")]
    [SerializeField]
    private Image SoundIcon = null;

    [Tooltip("The sprite used for muting the game.")]
    [SerializeField]
    private Sprite MutedSprite = null;

    [Tooltip("The sprite used for enabling sound.")]
    [SerializeField]
    private Sprite SoundSprite = null;

    [Tooltip("The music mixer reference")]
    [SerializeField]
    private AudioMixer MusicMixer = null;

    [Tooltip("The game sound mixer reference")]
    [SerializeField]
    private AudioMixer SoundMixer = null;

    [Tooltip("The sound(s) that should be played whenever a button is pressed.")]
    [SerializeField]
    private SoundScript ClickSounds = null;



    private TransitionScript Transition = null;



    public void Start()
    {
        Transition = GetComponent<TransitionScript>();
        SoundIcon.sprite = (AudioListener.pause) ? MutedSprite : SoundSprite;


        ClickSounds.SetAudio(GetComponent<AudioSource>());
    }


    public void Resume()
    {
        ClickSounds.Play();
        PauseScript.Unpause();
        Destroy(gameObject);
    }


    public void MainMenu()
    {
        ClickSounds.Play();
        Transition.LoadScene("MainMenu");
    }


    public void OpenLevel(string Level)
    {
        ClickSounds.Play();
        Transition.LoadScene(Level);
    }


    public void Replay()
    {
        ClickSounds.Play();
        Transition.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void ToggleMute()
    {
        AudioListener.pause = !AudioListener.pause;
        AudioListener.volume = (AudioListener.pause) ? 0.0f : 1.0f;
        SoundIcon.sprite = (AudioListener.pause) ? MutedSprite : SoundSprite;
        if (!AudioListener.pause) ClickSounds.Play();
    }


    public void Mute()
    {
        AudioListener.pause = true;
        AudioListener.volume = 0.0f;
        SoundIcon.sprite = MutedSprite;
    }


    public void Unmute()
    {
        ClickSounds.Play();
        AudioListener.pause = false;
        AudioListener.volume = 1.0f;
        SoundIcon.sprite = SoundSprite;
    }


    public void SetMusicVolume(float Value)
    {
        if (MusicMixer.SetFloat("Music", Value)) Debug.Log("Ran");
    }


    public void SetSoundVolume(float Value)
    {
        SoundMixer.SetFloat("Sound", Value);
    }
}

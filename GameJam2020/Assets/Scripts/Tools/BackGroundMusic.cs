using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    [Tooltip("The music that you want to be played whilst playing the game")]
    [SerializeField]
    private SoundScript BackgroundMusicScript = null;


    [Tooltip("The Background Music prefab you dont wont destroyed")]
    [SerializeField]
    private GameObject SoundManager = null;

    // Start is called before the first frame update
    void Start()
    {

        if (!FindObjectOfType<BackGroundMusic>().CompareTag("MusicManager"))
        {

            LoadSounds();
            DontDestroyOnLoad(SoundManager);
            DontDestroyOnLoad(this);
        }
    }

  
    private void LoadSounds()
    {
        BackgroundMusicScript.SetAudio(GetComponent<AudioSource>());
        BackgroundMusicScript.Play();

        SoundManager = Instantiate(SoundManager);
    }




}

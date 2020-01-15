using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundScript
{
    [Tooltip("Selects a random sound clip to be played everytime this object is played.")]
    public bool RandomClip = true;

    [Tooltip("The clip index this sound should play (Ignored if RandomClip = true).")]
    public int ClipIndex = 0;

    [Tooltip("A list of sounds that can be played.")]
    public List<AudioClip> Clips = new List<AudioClip>();

    // A reference to the audio source attached to the object.
    private AudioSource Audio = null;



    // Plays a sound based on the information of this class.
    public void Play()
    {
        if (Clips.Count > 0)
        {
            if (Audio)
            {
                AudioClip PlayClip = Clips[ClipIndex];
                if (RandomClip)
                {
                    PlayClip = GetRandomClip();
                }
                Audio.clip = PlayClip;
                Audio.Play();
            }
        }
    }


    // Plays the sound stored at the clip index.
    public void PlayClipIndex()
    {
        if (Clips.Count > 0)
        {
            if (Audio)
            {
                Audio.clip = Clips[ClipIndex];
                Audio.Play();
            }
        }
    }


    // Plays a random clip stored in this object.
    public void PlayRandomClip()
    {
        if (Clips.Count > 0)
        {
            if (Audio)
            {
                Audio.clip = GetRandomClip();
                Audio.Play();
            }
        }
    }


    // Plays an inputted clip that has no relavance to this object.
    // @param Clip = The sound clip that should be played.
    public void PlayClip(AudioClip Clip)
    {
        if (Audio)
        {
            Audio.clip = Clip;
            Audio.Play();
        }
    }


    public void PlayNewRandom(Vector3 Location)
    {
        AudioSource.PlayClipAtPoint(GetRandomClip(), Location);
    }


    // Returns a randomly selected clip stored in this object.
    public AudioClip GetRandomClip()
    {
        return Clips[Random.Range(0, Clips.Count)];
    }


    public void SetAudio(AudioSource InAudio)
    {
        Audio = InAudio;
    }
}

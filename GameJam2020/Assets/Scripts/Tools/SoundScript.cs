using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundScript
{
    [Tooltip("The prefab instance of the sound object")]
    [SerializeField]
    private AudioSource AudioPrefab = null;

    [Tooltip("Selects a random sound clip to be played everytime this object is played.")]
    public bool RandomClip = true;

    [Tooltip("The clip index this sound should play (Ignored if RandomClip = true).")]
    public int ClipIndex = 0;

    [Tooltip("A list of sounds that can be played.")]
    public List<AudioClip> Clips = new List<AudioClip>();

    // A reference to the audio source attached to the object.
    private AudioSource Audio = null;
    private Transform Trans = null;



    // Plays a sound based on the information of this class.
    public void Play()
    {
        if (Clips.Count > 0)
        {
            if (AudioPrefab)
            {
                CreateSound();
                return;
            }

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
            if (AudioPrefab)
            {
                CreateSoundIndex();
                return;
            }

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
            if (AudioPrefab)
            {
                CreateSoundRandom();
                return;
            }

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
            if (AudioPrefab)
            {
                CreateSound(Clip);
                return;
            }

            Audio.clip = Clip;
            Audio.Play();
        }
    }


    public void PlayNew(Vector3 Location)
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
                AudioSource.PlayClipAtPoint(PlayClip, Location);
            }
        }
    }


    public void PlayNewRandom(Vector3 Location)
    {
        AudioSource.PlayClipAtPoint(GetRandomClip(), Location);
    }


    public void PlayNewIndex(Vector3 Location)
    {
        AudioSource.PlayClipAtPoint(Clips[ClipIndex], Location);
    }


    // Returns a randomly selected clip stored in this object.
    public AudioClip GetRandomClip()
    {
        return Clips[Random.Range(0, Clips.Count)];
    }


    public void SetAudio(AudioSource InAudio)
    {
        if (InAudio)
        {
            Audio = InAudio;
            Trans = InAudio.transform;
        }
    }


    public void SetTransform(Transform InTrans)
    {
        Trans = InTrans;
    }


    public AudioClip GetClip()
    {
        AudioClip PlayClip = Clips[ClipIndex];
        if (RandomClip)
        {
            PlayClip = GetRandomClip();
        }
        return PlayClip;
    }


    private void CreateSound()
    {
        AudioSource Inst = GameObject.Instantiate(AudioPrefab, Trans.transform.position, Trans.transform.rotation);
        Inst.clip = GetClip();
        Inst.Play();
        GameObject.Destroy(Inst, Inst.clip.length);
        return;
    }


    private void CreateSound(AudioClip Clip)
    {
        AudioSource Inst = GameObject.Instantiate(AudioPrefab, Trans.transform.position, Trans.transform.rotation);
        Inst.clip = Clip;
        Inst.Play();
        GameObject.Destroy(Inst, Inst.clip.length);
        return;
    }


    private void CreateSoundIndex()
    {
        AudioSource Inst = GameObject.Instantiate(AudioPrefab, Trans.transform.position, Trans.transform.rotation);
        Inst.clip = Clips[ClipIndex];
        Inst.Play();
        GameObject.Destroy(Inst, Inst.clip.length);
        return;
    }


    private void CreateSoundRandom()
    {
        AudioSource Inst = GameObject.Instantiate(AudioPrefab, Trans.transform.position, Trans.transform.rotation);
        Inst.clip = GetRandomClip();
        Inst.Play();
        GameObject.Destroy(Inst, Inst.clip.length);
        return;
    }
}

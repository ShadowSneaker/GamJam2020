using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{

    private Animation Anim = null;


    private void Start()
    {
        Anim = GetComponent<Animation>();
    }


    public void Play()
    {
        if (Anim)
        {
            Anim.Play();
        }
    }


    public void Stop()
    {
        if (Anim)
        {
            Anim.Stop();
        }
    }
}

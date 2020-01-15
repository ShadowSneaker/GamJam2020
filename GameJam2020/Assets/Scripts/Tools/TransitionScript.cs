using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TransitionScript : MonoBehaviour
{
    
    [Tooltip("the animation clip for fade in")]
    [SerializeField]
    private AnimationClip FadeInClip = null;

    [Tooltip("the animation clip for fade out")]
    [SerializeField]
    private AnimationClip FadeOutClip = null;

    [Tooltip("the Ui that you want to be created (canvas)")]
    [SerializeField]
    private Canvas TransitionUI = null;

    [Tooltip("the scene that you want to be loaded")]
    public string SceneName = "";



    public void LoadScene()
    {
         StartCoroutine(SceneDelay(SceneName));
    }


    public void LoadScene(string Name)
    {
        StartCoroutine(SceneDelay(Name));
    }
    

    IEnumerator SceneDelay(string Name)
    {
        //Debug.Log(Name);
        PauseScript.Unpause();

        TransitionUI = Instantiate<Canvas>(TransitionUI);
        Animation Anim = TransitionUI.GetComponentInChildren<Animation>();
        Anim.clip = FadeInClip;
        Anim.AddClip(FadeInClip, "FadeIn");
        Anim.AddClip(FadeOutClip, "FadeOut");


        DontDestroyOnLoad(TransitionUI);
        DontDestroyOnLoad(this);
        
        Anim.Play("FadeIn");
        
        yield return new WaitForSeconds(Anim.clip.length);
        SceneManager.LoadScene(Name);
        yield return new WaitForSeconds(0.05f);

        Anim.clip = FadeOutClip;
        Anim.Play("FadeOut");

        Destroy(TransitionUI.gameObject, Anim.clip.length);
        Destroy(gameObject);
    }



}

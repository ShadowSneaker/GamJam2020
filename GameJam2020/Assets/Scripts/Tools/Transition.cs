using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Transition : MonoBehaviour
{
    
    [Tooltip("the animation clip for fade in")]
    [SerializeField]
    private AnimationClip FadeInClip = null;

    [Tooltip("the animation clip for fade out")]
    [SerializeField]
    private AnimationClip FadeOutClip = null;

    [Tooltip("the Ui that you want to be created (canvas)")]
    [SerializeField]
    private Canvas transitionUI = null;

    [Tooltip("the scene that you want to be loaded")]
    [SerializeField]
    private string SceneName = "";



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void LoadNextScene( )
   {
        StartCoroutine(SceneDelay(SceneName));
   }

    IEnumerator SceneDelay(string Name)
    {
        transitionUI = Instantiate<Canvas>(transitionUI);
        Animation Anim = transitionUI.GetComponentInChildren<Animation>();
        Anim.clip = FadeInClip;
        Anim.AddClip(FadeInClip, "FadeIn");
        Anim.AddClip(FadeOutClip, "FadeOut");


        DontDestroyOnLoad(transitionUI);
        DontDestroyOnLoad(this);

        Debug.Log(Anim.GetClipCount());

        if(Anim.GetClip("FadeIn"))
        {
            Debug.Log("start clip found");
            Anim.Play("FadeIn");
        }

        yield return new WaitForSeconds(Anim.clip.length);
        SceneManager.LoadScene(Name);
        yield return new WaitForSeconds(0.01f);

        Anim.clip = FadeOutClip;

        if(Anim.GetClip("FadeOut"))
        {
            Anim.Play("FadeOut");
        }
        

        Destroy(transitionUI, Anim.clip.length);
        Destroy(gameObject);
    }



}

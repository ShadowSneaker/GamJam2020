using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeadScript : MonoBehaviour
{
    [Tooltip("Consumes the head when everytime the head is yeeted.")]
    public bool ConsumeHead = true;

    [Tooltip("The amount of times the user can use this head type.")]
    [SerializeField]
    private int YeetCount = 3;

    // Keeps track of how many throws this head has left.
    internal int ThrowsLeft = 3;

    [Tooltip("Forces this object to keep it's angular and velocity drag.")]
    public bool KeepDrag = false;


    [Header("Sounds")]


    [Tooltip("The sound this head makes when being launched.")]
    [SerializeField]
    SoundScript YeetSounds = null;

    [Tooltip("The sound this head makes when hitting an object (while in movement).")]
    [SerializeField]
    SoundScript HitSounds = null;

    [Tooltip("A list of sounds that this object randomly plays while at rest.")]
    [SerializeField]
    SoundScript RestSounds = null;

    [Tooltip("The random range which this object will wait before making a rest sound.")]
    [SerializeField]
    SRange RestDelay = new SRange(1.0f, 10.0f);

    // The current value of the delay.
    private float Timer = 0.0f;

    // Determines if this is the frame the rigid body has fallen asleep.
    private bool JustFellAsleep = false;

    [Tooltip("The sprite that is displayed on the UI when the user switches to this head.")]
    public Sprite Image = null;

    // A reference to the attached audio component.
    private AudioSource Audio = null;

    // A refernce to the attached rigid body component.
    protected Rigidbody2D Rigid = null;



    public virtual void Reset()
    {
        
        Rigid.angularDrag = 0.01f;
        Rigid.drag = 0.01f;
        
    }


    protected virtual void Start()
    {
        Audio = GetComponent<AudioSource>();
        YeetSounds = new SoundScript(Audio);
        HitSounds = new SoundScript(Audio);
        RestSounds = new SoundScript(Audio);

        Rigid = GetComponent<Rigidbody2D>();
        ThrowsLeft = YeetCount;
    }


    private void FixedUpdate()
    {
        if (Rigid)
        {
            if (Rigid.IsSleeping())
            {
                if (!JustFellAsleep)
                {
                    JustFellAsleep = true;
                    Timer = Random.Range(RestDelay.Min, RestDelay.Max);
                }

                if (Timer > 0.0f)
                {
                    Timer -= Time.fixedDeltaTime;
                }
                else
                {
                    RestSounds.Play();
                    Timer = Random.Range(RestDelay.Min, RestDelay.Max);
                }
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D Other)
    {
        if (Rigid)
        {
            if (Rigid.IsAwake())
            {
                HitSounds.Play();
            }
        }
    }



    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public SoundScript GetYeetSounds()
    {
        return YeetSounds;
    }
}

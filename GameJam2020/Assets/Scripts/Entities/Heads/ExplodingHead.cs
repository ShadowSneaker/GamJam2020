using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingHead : MonoBehaviour
{
    // the power of the explosion
    [Tooltip("The power you want the explosion to be")]
    [SerializeField]
    private float Power = 10.0f;

    // the radius of the explosion
    [Tooltip("The radius of the explosion")]
    [SerializeField]
    private float Radius = 5.0f;

    [Tooltip("The sound that is played when the head explodes.")]
    [SerializeField]
    private SoundScript ExplodeSound = null;

    private ParticleSystem Particle = null;
    private Rigidbody2D RB = null;
    private AudioSource Audio = null;

    // Start is called before the first frame update
    void Start()
    {
        Particle = GetComponent<ParticleSystem>();
        RB = GetComponent<Rigidbody2D>();
        Audio = GetComponent<AudioSource>();
        ExplodeSound = new SoundScript(Audio);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public  void AddExplosionForce()
    {
        

        if (RB)
        {
            Vector2 ExplosionPosition = transform.position;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(ExplosionPosition, Radius);
            foreach(Collider2D hit in colliders)
            {
                Rigidbody2D rb = hit.attachedRigidbody;//hit.GetComponent<Rigidbody2D>();
                if (rb)
                {
                    Vector2 Direction = (rb.transform.position - transform.position).normalized;
                    float Percentage = Vector2.Distance(transform.position, rb.transform.position) / Radius;
                    rb.velocity = Direction * Power * (1.0f - Percentage);
                }
            }


        }

        Particle.Play();
        ExplodeSound.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        AddExplosionForce();
    }

}

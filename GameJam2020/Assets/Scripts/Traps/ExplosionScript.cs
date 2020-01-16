using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript: MonoBehaviour
{
    [Tooltip("Forces the explosions to be used on trigger instead of collision")]
    [SerializeField]
    private bool UseTrigger = false;

    // the power of the explosion
    [Tooltip("The power you want the explosion to be")]
    [SerializeField]
    private float Power = 10.0f;

    // the radius of the explosion
    [Tooltip("The radius of the explosion")]
    [SerializeField]
    private float Radius = 5.0f;

    [Tooltip("the amount of damage that the explosion does")]
    [SerializeField]
    private float Damage = 0.0f;

    [Tooltip("Will this object be destroyed after exploading")]
    [SerializeField]
    private bool Destroys = false;

    [SerializeField]
    private bool ExplodeOnce = false;

    [SerializeField]
    private bool IsProximity = false;

    [SerializeField]
    private float ExplosionDelay = 0.5f;

    [SerializeField]
    private ParticleSystem ExplosionPrefab = null;



    [Header("Sounds")]


    [Tooltip("The sound that is played when the head explodes.")]
    [SerializeField]
    private SoundScript ExplodeSound = null;

    [SerializeField]
    private SoundScript ProximitySound = null;


    internal bool Exploded = false;


    private ParticleSystem Particle = null;
   //private Rigidbody2D RB = null;
    private AudioSource Audio = null;

    // Start is called before the first frame update
    void Start()
    {
        //RB = GetComponent<Rigidbody2D>();
        Audio = GetComponent<AudioSource>();
        ExplodeSound.SetAudio(Audio);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public  void AddExplosionForce()
    {

        if (!Exploded)
        {
            Vector2 ExplosionPosition = transform.position;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(ExplosionPosition, Radius);
            foreach (Collider2D Hit in colliders)
            {
                if (!Hit.CompareTag("Water") && !Hit.CompareTag("Iceicle"))
                {
                    Rigidbody2D Rigid = Hit.attachedRigidbody;//hit.GetComponent<Rigidbody2D>();
                    if (Rigid && Rigid.gameObject != gameObject)
                    {
                        Vector2 Direction = (Rigid.transform.position - transform.position).normalized;
                        float Percentage = Vector2.Distance(transform.position, Rigid.transform.position) / Radius;
                        Rigid.velocity = Direction * Power * (1.0f - Percentage);
                        if (Rigid.GetComponent<HealthScript>())
                        {
                            Rigid.GetComponent<HealthScript>().ApplyDamage(Damage);
                        }
                    }
                }
            }

            
            if (ExplodeOnce) Exploded = true;


            Particle = Instantiate(ExplosionPrefab, transform.position, transform.rotation);
            Destroy(Particle.gameObject, Particle.main.duration + Particle.main.startLifetime.constant);
            ExplodeSound.Play();


            if (Destroys)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!UseTrigger)
        {
            if (IsProximity)
            {
                StartCoroutine(ExplodeDelay());
            }
            else
            {
                AddExplosionForce();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (UseTrigger)
        {
            if (IsProximity)
            {
                StartCoroutine(ExplodeDelay());
            }
            else
            {
                AddExplosionForce();
            }
        }
    }

    public void Reset()
    {
        Exploded = false;
    }


    private IEnumerator ExplodeDelay()
    {
        ProximitySound.Play();
        yield return new WaitForSeconds(ExplosionDelay);
        AddExplosionForce();
    }
}

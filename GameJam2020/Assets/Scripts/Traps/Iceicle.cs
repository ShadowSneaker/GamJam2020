using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceicle : MonoBehaviour
{

    // this is the iceicles rigid body
    private Rigidbody2D RB;


    [SerializeField]
    private float Damage = 20.0f;

    [Tooltip("the sounds for the iceicle falling")]
    [SerializeField]
    private SoundScript IceFall = null;

    [Tooltip("the sounds for the iceicle hitting player")]
    [SerializeField]
    private SoundScript IceHurt = null;

    // Determines if this object should be falling.
    private bool Falling = false;


    // Start is called before the first frame update
    void Start()
    {
        AudioSource Audio = GetComponent<AudioSource>();
        IceFall.SetAudio(Audio);
        IceHurt.SetAudio(Audio);
        RB = GetComponent<Rigidbody2D>();
        RB.bodyType = RigidbodyType2D.Static;

    }

    // Update is called once per frame
    void Update()
    {
        TestBelow();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Falling)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<HealthScript>().ApplyDamage(Damage);

            }

            IceHurt.PlayNew(transform.position);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Falling)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<HealthScript>().ApplyDamage(Damage);

            }

            IceHurt.PlayNew(transform.position);
            Destroy(gameObject);
        }
    }


    private void TestBelow()
    {
        RaycastHit2D hit;

        hit = Physics2D.Raycast(gameObject.transform.position, Vector2.down, 100000.0f);

        if(hit && hit.collider.CompareTag("Player"))
        {
            Falling = true;
            RB.bodyType = RigidbodyType2D.Dynamic;
            IceFall.PlayNew(transform.position);
        }

    }


}

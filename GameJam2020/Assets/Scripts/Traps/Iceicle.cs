using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceicle : MonoBehaviour
{

    // this is the iceicles rigid body
    private Rigidbody2D RB;

    [Tooltip("the sounds for the iceicle falling")]
    [SerializeField]
    private SoundScript IceFall = null;

    [Tooltip("the sounds for the iceicle hitting player")]
    [SerializeField]
    private SoundScript IceHurt = null;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        

    }

    // Update is called once per frame
    void Update()
    {
        TestBelow();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<HealthScript>().ApplyDamage(35.0f);

        }

        Destroy(this);
        IceHurt.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this);
        IceHurt.Play();
    }


    private void TestBelow()
    {
        RaycastHit2D hit;

        hit = Physics2D.Raycast(gameObject.transform.position, Vector2.down, 100000.0f);

        if(hit && hit.collider.CompareTag("Player"))
        {
           
            RB.WakeUp();
            IceFall.Play();
        }

    }


}

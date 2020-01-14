using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friction : MonoBehaviour
{
    [Tooltip("Determiens how much angular drag should be applied to the head.")]
    [SerializeField]
    private float AngularDrag = 0.5f;

    [Tooltip("Determines how much linear drag should be applied to the head.")]
    [SerializeField]
    private float LinearDrag = 0.3f;

    Coroutine Delay;



    private void OnCollisionEnter2D(Collision2D Other)
    {
        if (Other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D Rigid = Other.gameObject.GetComponent<Rigidbody2D>();
            if (Rigid)
            {
                SetDrag(Rigid);
            }
        }
    }


    private void OnCollisionExit2D(Collision2D Other)
    {
        if (Other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D Rigid = Other.gameObject.GetComponent<Rigidbody2D>();
            if (Rigid)
            {
                if (Delay != null) StopCoroutine(Delay);
                Rigid.angularDrag = 0.01f;
                Rigid.drag = 0.01f;
            }
        }
    }


    private void OnCollisionStay2D(Collision2D Other)
    {
        if (Other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D Rigid = Other.gameObject.GetComponent<Rigidbody2D>();
            if (Rigid)
            {
                if (!TMath.NearlyEqual(Rigid.velocity, Vector2.zero, 0.1f))
                {
                    Delay = StartCoroutine(StayDelay(Rigid));
                }
            }
        }
    }


    private IEnumerator StayDelay(Rigidbody2D Rigid)
    {
        yield return new WaitForSeconds(0.05f);
        SetDrag(Rigid);
    }


    private void SetDrag(Rigidbody2D Rigid)
    {
        HeadScript Head = Rigid.GetComponent<HeadScript>();
        if (Head)
        {
            if (Head.KeepDrag)
            {
                return;
            }
        }
        Rigid.angularDrag = AngularDrag;
        Rigid.drag = LinearDrag;
    }
}

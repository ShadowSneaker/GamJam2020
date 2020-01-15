using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyHead : HeadScript
{
    [Tooltip("Determiens how much angular drag should be applied to the head.")]
    [SerializeField]
    private float AngularDrag = 10.0f;

    [Tooltip("Determines how much linear drag should be applied to the head.")]
    [SerializeField]
    private float LinearDrag = 1000.0f;


    Coroutine Delay = null;



    private void OnCollisionEnter2D(Collision2D Other)
    {
        Rigid.angularDrag = AngularDrag;
        Rigid.drag = LinearDrag;
    }

    private void OnCollisionStay2D(Collision2D Other)
    {
        if (Delay != null) StopCoroutine(Delay);
        Delay = StartCoroutine(StayDelay());
    }


    private void OnCollisionExit2D(Collision2D Other)
    {
        if (Delay != null) StopCoroutine(Delay);
    }


    private IEnumerator StayDelay()
    {
        yield return new WaitForSeconds(0.1f);
        Rigid.angularDrag = AngularDrag;
        Rigid.drag = LinearDrag;
    }
}

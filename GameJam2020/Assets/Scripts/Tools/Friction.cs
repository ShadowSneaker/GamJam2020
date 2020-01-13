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



    private void OnCollisionEnter2D(Collision2D Other)
    {
        if (Other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D Rigid = Other.gameObject.GetComponent<Rigidbody2D>();
            if (Rigid)
            {
                Rigid.angularDrag = AngularDrag;
                Rigid.drag = LinearDrag;
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
                Rigid.angularDrag = 0.01f;
                Rigid.drag = 0.0f;
            }
        }
    }
}

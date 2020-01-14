using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushZone : MonoBehaviour
{
    [Tooltip("The amount of force that is applied to all objects inside this field.")]
    [SerializeField]
    private float BlowStrength = 50.0f;



    private void OnTriggerStay2D(Collider2D Other)
    {
        Rigidbody2D Rigid = Other.attachedRigidbody;
        if (Rigid)
        {
            Rigid.AddForce(transform.up * BlowStrength);
        }
    }
}

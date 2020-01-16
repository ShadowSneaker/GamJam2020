using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Activator : MonoBehaviour
{
    [Tooltip("A list of sounds that can trigger the delegate.")]
    [SerializeField]
    private string[] WhitelistTags = null;

    [Tooltip("The material used when the object is activated.")]
    [SerializeField]
    private Material ActiveMaterial = null;

    [Tooltip("A reference to the zone mesh.")]
    [SerializeField]
    private MeshRenderer Mesh = null;

    [Tooltip("The function that is ran when an object collides with this zone.")]
    [SerializeField]
    private UnityEvent OnCollision = null;



    private void OnCollisionEnter2D(Collision2D Other)
    {
        if (HasTag(Other.gameObject))
        {
            if (ActiveMaterial) Mesh.material = ActiveMaterial;

            if (OnCollision != null)
            {
                OnCollision.Invoke();
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (HasTag(Other.gameObject))
        {
            if (ActiveMaterial) Mesh.material = ActiveMaterial;

            if (OnCollision != null)
            {
                OnCollision.Invoke();
            }
        }
    }


    private bool HasTag(GameObject Obj)
    {
        for (int i = 0; i < WhitelistTags.Length; ++i)
        {
            if (Obj.CompareTag(WhitelistTags[i])) return true;
        }
        return false;
    }
}

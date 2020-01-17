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

    [Tooltip("The sound that is played when this object is activated.")]
    [SerializeField]
    private SoundScript ActivationSound = null;

    [Tooltip("A reference to the zone mesh.")]
    [SerializeField]
    private MeshRenderer Mesh = null;

    [Tooltip("The function that is ran when an object collides with this zone.")]
    [SerializeField]
    private UnityEvent OnCollision = null;

    // Represents if this object has been activated.
    private bool Activated = false;



    private void Awake()
    {
        ActivationSound.SetAudio(GetComponent<AudioSource>());
    }


    private void OnCollisionEnter2D(Collision2D Other)
    {
        if (!Activated)
        {
            if (HasTag(Other.gameObject))
            {
                if (ActiveMaterial) Mesh.material = ActiveMaterial;
                Activated = true;

                if (OnCollision != null)
                {
                    OnCollision.Invoke();
                }

                ActivationSound.Play();
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (!Activated)
        {
            if (HasTag(Other.gameObject))
            {
                if (ActiveMaterial) Mesh.material = ActiveMaterial;
                Activated = true;

                if (OnCollision != null)
                {
                    OnCollision.Invoke();
                }

                ActivationSound.Play();
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

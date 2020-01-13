using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    [Tooltip("Should this script lerp towards the target.")]
    [SerializeField]
    private bool UseLerp = true;

    [Tooltip("The strength of the lerp")]
    [SerializeField]
    private float LerpStrength = 5.0f;

    [Tooltip("The object this object should follow.")]
    [SerializeField]
    private Transform FollowObject = null;

    [Tooltip("Should this object use the origional Z location instead of an offset.")]
    [SerializeField]
    private bool UseZ = false;

    [Tooltip("The offset of the following object")]
    [SerializeField]
    private Vector3 Offset = Vector3.zero;



    // Update is called once per frame
    void FixedUpdate()
    {
        if (FollowObject)
        {
            if (UseLerp)
            {
                Vector3 Pos = transform.position;
                Vector2 Other = FollowObject.position;
                Vector3 NewLocation = Vector2.Lerp(Pos, Other, Vector2.Distance(Pos, Other) * LerpStrength * Time.fixedDeltaTime);
                //NewLocation.z = transform.position.z;

                if (UseZ)
                {
                    transform.position = NewLocation + Offset;
                }
                else
                {
                    Vector3 Position = NewLocation;
                    Position.z = transform.position.z;
                    transform.position = Position;
                }
            }
            else
            {
                transform.position = FollowObject.position + Offset;
            }
        }
    }
}

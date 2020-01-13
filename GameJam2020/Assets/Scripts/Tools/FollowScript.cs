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



    // Update is called once per frame
    void FixedUpdate()
    {
        if (FollowObject)
        {
            if (UseLerp)
            {
                Vector2 Pos = transform.position;
                Vector2 Other = FollowObject.position;
                Vector3 NewLocation = Vector2.Lerp(Pos, Other, Vector2.Distance(Pos, Other) * LerpStrength * Time.fixedDeltaTime);
                NewLocation.z = transform.position.z;
                transform.position = NewLocation;
            }
            else
            {
                transform.position = FollowObject.position;
            }
        }
    }
}

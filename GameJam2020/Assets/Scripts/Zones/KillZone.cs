using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{




    public void OnTriggerEnter2D(Collider2D Other)
    {
        HealthScript Health = Other.GetComponent<HealthScript>();
        if (Health)
        {
            Health.ApplyDamage(Mathf.Infinity);
        }
    }
}

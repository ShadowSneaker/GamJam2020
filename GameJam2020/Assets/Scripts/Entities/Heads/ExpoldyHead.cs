using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpoldyHead : HeadScript
{
    ExplosionScript Explosion = null;


    public void Start()
    {
        Explosion = GetComponent<ExplosionScript>();
    }

    public override void Reset()
    {
        Explosion.Reset();
    }
}

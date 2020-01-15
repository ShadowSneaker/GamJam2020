using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpoldyHead : HeadScript
{
    ExplosionScript Explosion = null;


    protected override void Awake()
    {
        base.Awake();
        Explosion = GetComponent<ExplosionScript>();
        Explosion.Exploded = true;
    }

    public override void Reset()
    {
        base.Reset();
        Explosion.Reset();
    }
}

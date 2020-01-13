using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class TMath
{
    public static bool NearlyEqual(float A, float B, float Range)
    {
        return ((A >= B - Range) && (A <= B + Range));
    }

    public static bool NearlyEqual(Vector2 A, Vector2 B, float Range)
    {
        return (((A.x >= B.x - Range) && (A.x <= B.x + Range)) && ((A.y >= B.y - Range) && (A.y <= B.y + Range)));
    }
}


[System.Serializable]
public struct SRange
{
    [Tooltip("The minimum value in the range")]
    public float Min;

    [Tooltip("The maximum value in the range.")]
    public float Max;



    public SRange(float InMin, float InMax)
    {
        Min = InMin;
        Max = InMax;
    }
}

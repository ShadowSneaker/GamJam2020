using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Saving 
{

    [System.Serializable]
    public struct LevelInfo
    {
        public bool Completed;
        public int Index;
        public float Time;
        public int Score;
        public int StarCount;

    }


    public List<LevelInfo> Levels = new List<LevelInfo>();


}

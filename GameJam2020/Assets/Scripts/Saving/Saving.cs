using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


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



public static class TSave
{
    static public Saving LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/GameSave.save"))
        {
            BinaryFormatter BF = new BinaryFormatter();
            FileStream FS = File.Open(Application.persistentDataPath + "/GameSave.save", FileMode.Open);
            Saving Save = (Saving)BF.Deserialize(FS);
            FS.Close();
            return Save;
        }
        return null;
    }
}

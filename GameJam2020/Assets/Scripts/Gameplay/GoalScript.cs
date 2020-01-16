using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GoalScript : MonoBehaviour
{
    //[Tooltip("The next level in the game.")]
    //[SerializeField]
    //private string NextLevel = "";

    [Tooltip("A reference to the game completed UI.")]
    [SerializeField]
    private WinUI UI = null;

    [Tooltip("The thresholds for reaching the stars")]
    [SerializeField]
    private int[] StarThresholds = new int[3];

    [Tooltip("The current level in the sequence.")]
    [SerializeField]
    private int LevelCount = 0;


    private PlayerController Player = null;


    public void Start()
    {
        Player = FindObjectOfType<PlayerController>();
    }


    public void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.CompareTag("Player"))
        {
            Player.LockPlayer();

            float BestTime = Player.GetTimer();
            Saving Save = TSave.LoadGame();
            if (Save != null && Save.Levels.Count >= LevelCount)
            {
                float SavedTime = Save.Levels[LevelCount - 1].Time;
                BestTime = (BestTime < SavedTime) ? BestTime : SavedTime;
            }

            UI = Instantiate(UI);
            UI.UpdateInfo(Player.GetTimer(), BestTime, Player.GetScore());
            UI.SetStars(GetStarCount(Player.GetScore()));
            SaveGame();
        }
    }


    private int GetStarCount(int Score)
    {
        for (int i = 0; i < StarThresholds.Length; ++i)
        {
            if (Score < StarThresholds[i]) return i;
        }
        return 3;
    }

    private void SaveGame()
    {
        Saving Save = TSave.LoadGame();
        if (Save == null)
        {
            Save = new Saving();
        }

        if (LevelCount > Save.Levels.Count)
        {
            // Add new level state.
            Saving.LevelInfo Info = new Saving.LevelInfo();
            Info.Score = Player.GetScore();
            Info.StarCount = GetStarCount(Info.Score);
            Info.Time = Player.GetTimer();
            Info.Completed = true;
            Info.Index = LevelCount;

            Save.Levels.Add(Info);
        }
        else
        {
            // Update level state.
            Saving.LevelInfo Info = Save.Levels[LevelCount - 1];
            Info.Score = (Player.GetScore() > Info.Score) ? Player.GetScore() : Info.Score;
            int StarCount = GetStarCount(Info.Score);
            Info.StarCount = (StarCount > Info.StarCount) ? StarCount : Info.StarCount;
            Info.Time = (Player.GetTimer() < Info.Time) ? Player.GetTimer() : Info.Time;
        }

        // Save to file.
        BinaryFormatter BF = new BinaryFormatter();
        FileStream FS = File.Create(Application.persistentDataPath + "/GameSave.save");
        BF.Serialize(FS, Save);
        FS.Close();
    }


    

}

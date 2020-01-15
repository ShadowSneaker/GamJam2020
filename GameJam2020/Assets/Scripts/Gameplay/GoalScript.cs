using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GoalScript : MonoBehaviour
{
    [Tooltip("The next level in the game.")]
    [SerializeField]
    private string NextLevel = "";

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

            UI = Instantiate(UI);
            UI.UpdateInfo(Player.GetTimer(), Player.GetScore());
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
        Saving Save = LoadGame();
        if (Save == null)
        {
            Save = new Saving();
        }

        if (LevelCount <= Save.Levels.Count)
        {
            // Update our game state
            Saving.LevelInfo Info = new Saving.LevelInfo();
            Info.Score = Player.GetScore();
            Info.StarCount = GetStarCount(Info.Score);
            Info.Time = Player.GetTimer();
            Info.Completed = true;
            Info.Index = LevelCount;

            Save.Levels.Add(Info);
        }

        // Save to file.
        BinaryFormatter BF = new BinaryFormatter();
        FileStream FS = File.Create(Application.persistentDataPath + "/GameSave.save");
        BF.Serialize(FS, Save);
        FS.Close();
    }


    private Saving LoadGame()
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

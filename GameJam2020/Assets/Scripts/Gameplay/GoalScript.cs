using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}

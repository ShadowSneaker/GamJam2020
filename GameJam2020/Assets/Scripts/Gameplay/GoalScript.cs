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

            // For some reason just doing:
            // UI.SetNextScene(NextLevel);
            // doesn't work. This is because it runs before the UI.Start() dispite being after the instantiate.
            StartCoroutine(LevelDelay());
        }
    }

    private IEnumerator LevelDelay()
    {
        yield return new WaitForSeconds(0.01f);
        UI.SetNextScene(NextLevel);
    }
}

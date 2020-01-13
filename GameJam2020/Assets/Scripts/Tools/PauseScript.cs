using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PauseScript
{
    public static bool GamePaused = false;


    public static void Pause()
    {
        Time.timeScale = 0.0f;
        GamePaused = true;
    }


    public static void Unpause()
    {
        Time.timeScale = 1.0f;
        GamePaused = false;
    }


    public static void TogglePause()
    {
        if (GamePaused)
        {
            Unpause();
        }
        else
        {
            Pause();
        }
    }
}

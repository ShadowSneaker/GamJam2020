using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelObject : MonoBehaviour
{
    [Tooltip("The Level you want this game to load")]
    [SerializeField]
    private string LevelName = "";

    [Tooltip("The instance of the ui you want to load")]
    [SerializeField]
    private LevelUI LevelUIInstance = null;

    [Tooltip("The level sequence value.")]
    public int LevelIndex = 0;

    // Allows the UI to appear on this level.
    private bool Unlocked = false;
    private bool IsCompleted = false;



    [Header("Colours")]


    [Tooltip("The material used for incomplete levels.")]
    [SerializeField]
    private Material Incomplete = null;

    [Tooltip("The material used for completed levels.")]
    [SerializeField]
    private Material Completed = null;

    [Tooltip("The material used for locked levels.")]
    [SerializeField]
    private Material Locked = null;

    
    // the thing to store the ui in
    private LevelUI UI;

    private MeshRenderer Mat = null;


    private void Start()
    {
        Mat = GetComponent<MeshRenderer>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (Unlocked)
        {
            if (other.CompareTag("Player"))
            {
                UI = Instantiate(LevelUIInstance);
                UI.GetComponent<LevelUI>().TheLevelName = LevelName;
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (UI)
        {
            if (other.CompareTag("Player"))
            {
                Destroy(UI.gameObject);
            }
        }
    }


    public void SetLockedState(bool Unlock, bool NewCompleted)
    {
        Unlocked = Unlock;
        IsCompleted = NewCompleted;

        if (Unlocked)
        {
            if (IsCompleted)
            {
                Mat.material = Completed;
            }
            else
            {
                Mat.material = Incomplete;
            }
        }
        else
        {
            Mat.material = Locked;
        }
    }
}

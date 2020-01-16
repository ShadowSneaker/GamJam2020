using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelObject : MonoBehaviour
{
    [Tooltip("Forces the level to be available.")]
    [SerializeField]
    private bool ForceOpen = false;

    [Tooltip("The Level you want this game to load")]
    [SerializeField]
    private string LevelName = "";

    [Tooltip("The instance of the ui you want to load")]
    [SerializeField]
    private LevelUI LevelUI = null;

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
    private LevelUI UIInstance;

    private MeshRenderer Mat = null;


    private void Start()
    {
        Mat = GetComponent<MeshRenderer>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (Unlocked || ForceOpen)
        {
            if (other.CompareTag("Player"))
            {
                UIInstance = Instantiate(LevelUI);
                //UIInstance.GetComponent<LevelUI>().TheLevelName = LevelName;
                UIInstance.SetLevelName(LevelName);

                MapPlayer Player = other.GetComponent<MapPlayer>();
                Saving.LevelInfo Info = (Player.GetSave().Levels.Count >= LevelIndex) ? Player.GetSave().Levels[LevelIndex - 1] : null;
                
                UIInstance.UpdateUI(Info);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (UIInstance)
        {
            if (other.CompareTag("Player"))
            {
                Destroy(UIInstance.gameObject);
            }
        }
    }


    public void SetLockedState(bool Unlock, bool NewCompleted)
    {
        Unlocked = Unlock;
        IsCompleted = NewCompleted;

        if (Unlocked || ForceOpen)
        {
            if (IsCompleted)
            {
                if (Completed)
                {
                    if (!Mat)
                    {
                        Mat = GetComponent<MeshRenderer>();
                    }
                    if (Mat) Mat.material = Completed;
                }
            }
            else
            {
                if (Incomplete)
                {
                    if (!Mat)
                    {
                        Mat = GetComponent<MeshRenderer>();
                    }
                    if (Mat) Mat.material = Incomplete;
                }
            }
        }
        else
        {
            Mat.material = Locked;
        }
    }
}

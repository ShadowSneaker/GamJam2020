using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapPlayer : MonoBehaviour
{

    [Tooltip("The gameobject that you want to add as the target shown where the player clicks")]
    [SerializeField]
    private GameObject Target = null;

    // A list of all levels on this map.
    private LevelObject[] Levels = null;

    // A reference to the nav mesh agent.
    private NavMeshAgent Agent = null;

    [Tooltip("The prefab of the settings UI.")]
    [SerializeField]
    private Canvas SettingsUI = null;

    private Canvas SettingsInst = null;

    private Saving Save = null;


    // Start is called before the first frame update
    void Start()
    {
        Agent = gameObject.GetComponent<NavMeshAgent>();
        Levels = FindObjectsOfType<LevelObject>();
        

        if (Levels.Length > 0)
        {
            Save = TSave.LoadGame();
            if (Save == null)
            {
                Save = new Saving();
            }

            // Set the base locked state for all levels.
            for (int i = 0; i < Levels.Length; ++i)
            {
                for (int j = 0; j < Save.Levels.Count; ++j)
                {
                    if (Levels[i].LevelIndex == j + 1)
                    {
                        Levels[i].SetLockedState(Levels[i].LevelIndex <= Save.Levels.Count, Save.Levels[j].Completed);
                    }
                }
            }

            // Unlock the next level.
            for (int i = 0; i < Levels.Length; ++i)
            {
                if (Levels[i].LevelIndex == Save.Levels.Count + 1)
                {
                    Levels[i].SetLockedState(true, false);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        MapMovement();
    }


    private void MapMovement()
    {
        if (!PauseScript.GamePaused)
        {
            if (Input.GetButtonDown("Yeet"))
            {
                Ray Hit = Camera.main.ScreenPointToRay(Input.mousePosition);
                Plane HitPlane = new Plane(-Vector3.up, Vector3.zero);
                float Distance;

                HitPlane.Raycast(Hit, out Distance);
                Vector3 MousePos = Hit.GetPoint(Distance);


                Target.transform.position = new Vector3(MousePos.x, 1.0f, MousePos.z);

                if (transform.position != MousePos)
                {
                    Agent.SetDestination(MousePos);
                }
            }
        }


        if (Input.GetButtonDown("Pause"))
        {
            PauseScript.TogglePause();
            if (PauseScript.GamePaused)
            {
                SettingsInst = Instantiate(SettingsUI);
            }
            else
            {
                Destroy(SettingsInst.gameObject);
            }
        }
        
    }


    public Saving GetSave()
    {
        return Save;
    }


}

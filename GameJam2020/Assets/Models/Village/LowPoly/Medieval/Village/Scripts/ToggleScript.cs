using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleScript : MonoBehaviour
{
    public GameObject ObjectToToggle;
    public bool TurnedOn = false;

    private void Start()
    {
        if (ObjectToToggle) ObjectToToggle.SetActive(TurnedOn);
    }

    public void Toggle()
    {
        if (TurnedOn)
        {
            TurnedOn = false;
        }
        else
        {
            TurnedOn = true;
        }

        if (ObjectToToggle) ObjectToToggle.SetActive(TurnedOn);
    }

    public void ForceOn()
    {
        if (ObjectToToggle) ObjectToToggle.SetActive(true);
        TurnedOn = true;
    }

    public void ForceOff()
    {
        if (ObjectToToggle) ObjectToToggle.SetActive(false);
        TurnedOn = false;
    }
}

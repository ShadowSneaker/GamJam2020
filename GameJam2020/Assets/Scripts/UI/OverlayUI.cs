using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayUI : MonoBehaviour
{
    // an image that represents the settings menu
    [Tooltip("the image that represents the settings menu")]
    [SerializeField]
    private Image SettingsOverlay = null;


    // an image that represents the current head selected
    [Tooltip("image that represents the head selection")]
    [SerializeField]
    private Image Head = null;

    // a number to determine what head should be shown
    private int HeadNumber = 1;

    // a number for how many heads can be used
    //[Tooltip("the maximum number of how many heads thier are to use")]
    //[SerializeField]
    //private int MaxHeadNumber = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SettingButton()
    {
        PauseScript.TogglePause();
        SettingsOverlay.gameObject.SetActive(!SettingsOverlay.gameObject.activeSelf);
    }

    public void SwitchHead()
    {
        //for now it changes the colour
        switch(HeadNumber)
        {
            case (1):
                {

                    Head.color = Color.red;
                    HeadNumber++;
                    break;
                }
            case (2):
                {
                    Head.color = Color.blue;
                    HeadNumber++;
                    break;
                }
            case (3):
                {
                    Head.color = Color.green;
                    HeadNumber = 1;
                    break;
                }

        }

        //change the head image
        // call a function to change the head weapon
    }

}

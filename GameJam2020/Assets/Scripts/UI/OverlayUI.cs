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

    // this number is to determine how many weapons the head will be able to use
    [Tooltip("The current weapon we have selected.")]
    [SerializeField]
    private int WeaponIndex = 1;

    // a number for how many heads can be used
    //[Tooltip("the maximum number of how many heads thier are to use")]
    //[SerializeField]
    //private int MaxHeadNumber = 3;

    // an array for the weapon colours (will be changed to images later)
    [Tooltip("An array for all the coulors you want the differnt weapons to be")]
    [SerializeField]
    private List<Color> WeaponNumberArray = new List<Color>();


    // Start is called before the first frame update
    void Start()
    {
        Head.color = WeaponNumberArray[WeaponIndex];
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
        

        WeaponIndex++;
        if(WeaponIndex >=  WeaponNumberArray.Count)
        {
            WeaponIndex = 0;
        }

        Head.color = WeaponNumberArray[WeaponIndex];

        //change the head image
        // call a function to change the head weapon
    }

}

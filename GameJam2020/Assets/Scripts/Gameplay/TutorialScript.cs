using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{

    [SerializeField]
    private Text FirstText = null;

    [SerializeField]
    private Text HeadText = null;



    private void Update()
    {
        if (Input.GetButtonDown("Yeet"))
        {
            RemoveFirst();
        }


        if (Input.GetButtonDown("SwitchWeapon"))
        {
            RemoveHead();
        }
    }


    public void RemoveFirst()
    {
        FirstText.gameObject.SetActive(false);
    }


    public void RemoveHead()
    {
        if (HeadText.color.a > 0.0f)
            HeadText.gameObject.SetActive(false);
    }
}

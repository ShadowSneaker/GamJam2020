using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [Tooltip("all the objects that you want the pressure plate to rotate")]
    [SerializeField]
    private List<GameObject> RotateObjects = new List<GameObject>();

    [Tooltip("How much you want all of the objects to be rotated by")]
    [SerializeField]
    private float RotationAmount = 0.0f;

    [Tooltip("all of the objects that you want opened when the plate is triggered")]
    [SerializeField]
    private List<GameObject> OpenObects = new List<GameObject>();

    [Tooltip("do you want this function to only run once if so turn to true")]
    [SerializeField]
    private bool ActiveOnce = false;

    private enum Function {Rotate, Open};

    [Tooltip("you can select what function you want Rotate rotates selected objects and open will open doors or objects")]
    [SerializeField]
    private Function SelectedFunction = Function.Rotate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //a function to rotate the game object that is set in this script
    public void RotateGameObject()
    {

        for(int i = 0; i < RotateObjects.Count; i++)
        {
            RotateObjects[i].transform.eulerAngles += new Vector3(RotateObjects[i].transform.rotation.x, RotateObjects[i].transform.rotation.y, (RotateObjects[i].transform.rotation.z + RotationAmount));
        }

    }

    public void OpenGameObjects()
    {
        foreach(GameObject G in OpenObects)
        {
            G.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            // change this to run the function selected

            switch(SelectedFunction)
            {
                case (Function.Open):
                    {
                        OpenGameObjects();
                        break;
                    }
                case (Function.Rotate):
                    {
                        RotateGameObject();
                        break;
                    }
            }

        }

        if(ActiveOnce)
        {
            gameObject.SetActive(false);
        }
            
    }

}

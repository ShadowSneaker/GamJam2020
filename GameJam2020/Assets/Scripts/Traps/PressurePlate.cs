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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            RotateGameObject();
    }

}

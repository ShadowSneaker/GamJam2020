using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelObject : MonoBehaviour
{
    [Tooltip("The Level you want this game to load")]
    [SerializeField]
    private string LevelName;

    [Tooltip("The instance of the ui you want to load")]
    [SerializeField]
    private LevelUI LevelUIInstance;

    
    // the thing to store the ui in
    private LevelUI UI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {

            UI = Instantiate(LevelUIInstance);
            UI.GetComponent<LevelUI>().TheLevelName = LevelName;
            
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(UI.gameObject);
        }
    }

}

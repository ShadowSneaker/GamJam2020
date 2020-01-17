using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        var Objects = GameObject.FindGameObjectsWithTag("MusicManager");
        for (int i = 0; i < Objects.Length; ++i)
        {
            if (Objects[i] != gameObject)
            {
                Destroy(Objects[i]);
            }
        }
        DontDestroyOnLoad(gameObject);
    }




}

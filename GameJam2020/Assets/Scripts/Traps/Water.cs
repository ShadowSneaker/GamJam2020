﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    [Tooltip("The Material you want the object to change when it is turned to ice")]
    [SerializeField]
    private Material IceMaterial = null;

    [Tooltip("The sounds that you want to use for the water")]
    [SerializeField]
    private SoundScript WaterSounds = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wind"))
        {
            // have it change colour or model to look like frozen water
            gameObject.GetComponent<MeshRenderer>().material = IceMaterial;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;

        }
        else if(collision.CompareTag("Player"))
        {
            WaterSounds.Play();
            collision.GetComponent<HealthScript>().ApplyDamage(100.0f);
        }
    }


}

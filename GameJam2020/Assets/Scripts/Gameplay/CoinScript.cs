using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [Tooltip("The amount of points this coin is worth.")]
    [SerializeField]
    private int Points = 50;

    [Tooltip("The sound this coin makes when picked up.")]
    [SerializeField]
    private SoundScript PickupSound = null;


    private PlayerController Player = null;


    private void Start()
    {
        Player = FindObjectOfType<PlayerController>();
    }



    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.CompareTag("Player"))
        {
            PickupSound.PlayNewRandom(transform.position);
            Player.AddPoints(Points);
            Destroy(gameObject);
        }
    }
}

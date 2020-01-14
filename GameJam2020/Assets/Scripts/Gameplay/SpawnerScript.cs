using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [Tooltip("Allows the item to respawn after a specified delay.")]
    [SerializeField]
    private bool AllowRespawn = true;

    [Tooltip("The duration it takes for the item to be replaced.")]
    [SerializeField]
    private float SpawnDelay = 5.0f;

    [Tooltip("The object that this spwner should spawn.")]
    [SerializeField]
    private HeadScript Head = null;

    private GameObject HeadInst = null;
    private PlayerController Player = null;



    private void Start()
    {
        Player = GameObject.FindObjectOfType<PlayerController>();

        HeadInst = Instantiate(Head.transform.GetChild(0).gameObject, transform);
    }


    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.CompareTag("Player"))
        {
            Player.AddHead(Head);
            Destroy(HeadInst);

            if (AllowRespawn)
            {
                StartCoroutine(RespawnDelay());
            }
        }
    }


    private IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(SpawnDelay);
        if (AllowRespawn) Respawn();
    }


    public void Respawn()
    {
        if (HeadInst) Destroy(HeadInst);
        HeadInst = Instantiate(Head.transform.GetChild(0).gameObject, transform);
    }
}

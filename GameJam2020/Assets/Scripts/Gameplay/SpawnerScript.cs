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

    [Tooltip("The speed the head spins.")]
    [SerializeField]
    private float SpinStrength = 5.0f;

    private GameObject HeadInst = null;
    private PlayerController Player = null;
    private ParticleSystem Particle = null;
    private Light Highlight = null;



    private void Start()
    {
        Player = FindObjectOfType<PlayerController>();

        HeadInst = Instantiate(Head.transform.GetChild(0).gameObject, transform);
        Particle = GetComponentInChildren<ParticleSystem>();
        Highlight = GetComponentInChildren<Light>();
    }


    private void FixedUpdate()
    {
        if (HeadInst)
        {
            HeadInst.transform.Rotate(new Vector3(0.0f, SpinStrength * Time.fixedDeltaTime, 0.0f));
        }
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
                Particle.Stop();
                Highlight.enabled = false;
            }
        }
    }


    private IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(SpawnDelay);
        if (AllowRespawn) Respawn();
        Particle.Play();
        Highlight.enabled = true;
    }


    public void Respawn()
    {
        if (HeadInst) Destroy(HeadInst);
        HeadInst = Instantiate(Head.transform.GetChild(0).gameObject, transform);
    }
}

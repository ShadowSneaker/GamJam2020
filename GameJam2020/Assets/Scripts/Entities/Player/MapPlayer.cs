using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapPlayer : MonoBehaviour
{

    [Tooltip("The gameobject that you want to add as the target shown where the player clicks")]
    [SerializeField]
    private GameObject Target;

    [Tooltip("How fast do you want the player to move")]
    [SerializeField]
    private float PlayerSpeed;



    private NavMeshAgent Agent = null;

    // Start is called before the first frame update
    void Start()
    {
      
        Agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        MapMovement();
    }


    private void MapMovement()
    {

        if(Input.GetMouseButtonDown(0))
        {
            Ray Hit = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane HitPlane = new Plane(-Vector3.up, Vector3.zero);
            float Distance;
            
            HitPlane.Raycast(Hit, out Distance);
            Vector3 MousePos = Hit.GetPoint(Distance);

            
            Target.transform.position = new Vector3(MousePos.x, 1.0f, MousePos.z);

            if (transform.position != MousePos)
            {
                Agent.SetDestination(MousePos);
            }
        }
        
    }





}

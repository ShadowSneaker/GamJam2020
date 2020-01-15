using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Refernce to the Nodes for the enemy to move between 
    [Tooltip("these are the nodes that the enemy can move to")]
    [SerializeField]
    private List<GameObject> Nodes = new List<GameObject>();

    // an integer to store what node we are currently moving towards
    [Tooltip(" this is the node the enemy will be moving towards")]
    [SerializeField]
    public int MoveToNode;

    // an enum to determine what kind of movement the AI will exert
    public enum Movement {Idle, Node, Recoil };

    // a refernce to the enum for the AI's type of movement
    public Movement CurrentMovement;

    // the amount that you want the enemy to move by
    public float MovementDistance;

    // the amount of damage the enemy can do to the player
    [Tooltip("the amount of damage the enemy can deal to the player")]
    [SerializeField]
    private float Damage = 0.0f;

    //the rigid body of the enemy
    private Rigidbody2D RB;

    // the time in which the enemy will recoil for once they have attacked
    private float RecoilTime = 3.0f;


    // Start is called before the first frame update
    void Start()
    {
        // the first node within the list
        MoveToNode = 0;

        // gets the rigid bidy for later use
        RB = gameObject.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveTo(MoveToNode);
    }


    //a function to move to the node selected
    // refernces an integer that is the node you want the enemy to move to 
    public void MoveTo(int node)
    {
        switch(CurrentMovement)
        {
            case (Movement.Idle):
                {
                    break;
                }
            case (Movement.Node):
                {
                    gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, Nodes[node].transform.position, MovementDistance * Time.deltaTime);
                    break;
                }
            case (Movement.Recoil):
                {
                    RB.AddForce(-transform.forward * 1000);
                    RecoilTime -= Time.deltaTime;
                    if(RecoilTime <= 0.0f)
                    {
                        CurrentMovement = Movement.Node;
                        RecoilTime = 3.0f;
                    }
                    
                    break;
                }
        }


    }


    // a function to change the node it needs to move to (this will be collision function)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // this should check for a node and change the move to number to this node plus one
        if(collision.CompareTag("Node"))
        {
            
            MoveToNode = collision.GetComponent<Node>().NodeNumber;
            

            if(MoveToNode >= Nodes.Count)
            {
                MoveToNode = 0;
            }

        }

        
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthScript Health = collision.gameObject.GetComponent<HealthScript>();
            if (Health)
            {
                Health.ApplyDamage(Damage);
                CurrentMovement = Movement.Recoil;
               
            }

           
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        
       
        
    }

}

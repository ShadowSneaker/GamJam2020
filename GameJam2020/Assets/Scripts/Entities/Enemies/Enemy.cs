using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Refernce to the Nodes for the enemy to move between 
    [Tooltip("these are the nodes that the enemy can move to")]
    [SerializeField]
    private List<GameObject> Nodes;

    // an integer to store what node we are currently moving towards
    [Tooltip(" this is the node the enemy will be moving towards")]
    [SerializeField]
    public int MoveToNode;

    // an enum to determine what kind of movement the AI will exert
    public enum Movement {Idle, Node };

    // a refernce to the enum for the AI's type of movement
    public Movement CurrentMovement;

    // the amount that you want the enemy to move by
    public float MovementDistance;

    // the amount of damage the enemy can do to the player
    [Tooltip("the amount of damage the enemy can deal to the player")]
    [SerializeField]
    private int Damage;

    // Start is called before the first frame update
    void Start()
    {
        // the first node within the list
        MoveToNode = 0;
        Debug.Log(Nodes.Count);
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
        if(CurrentMovement == Movement.Node)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, Nodes[node].transform.position, MovementDistance * Time.deltaTime);
        }


    }


    // a function to change the node it needs to move to (this will be collision function)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // this should check for a node and change the move to number to this node plus one
        if(collision.CompareTag("Node"))
        {
            
            MoveToNode = collision.GetComponent<Node>().NodeNumber;
            Debug.Log(MoveToNode);

            if(MoveToNode >= Nodes.Count)
            {
                MoveToNode = 0;
            }

        }

        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<HealthScript>().ApplyDamage(Damage);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
       
        
    }

}

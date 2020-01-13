using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Displays the cursor on the screen")]
    [SerializeField]
    private bool ShowCursor = true;

    [Tooltip("The strength the user has to yeet heads.")]
    [SerializeField]
    private float YeetStrength = 5.0f;


    public Rigidbody2D Head;

    // Determines if the head can be yeeted.
    private bool CanYeet;


    public void Start()
    {
        Cursor.visible = ShowCursor;
        Cursor.lockState = (ShowCursor) ? CursorLockMode.Confined : CursorLockMode.Locked;
    }


    public void Update()
    {
        if (Input.GetButtonDown("Yeet"))
        {
            if (CanYeet)
            {
                Yeet(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }

        Vector3 Pos = Head.transform.position;
        Pos.z = -5.0f;
        transform.position = Pos;
    }


    public void FixedUpdate()
    {
        if (!CanYeet && Head.IsSleeping())
        {
            CanYeet = true;
        }
    }



    public void Yeet(Vector2 YeetDirection)
    {
        Vector2 Direction = YeetDirection - new Vector2(Head.transform.position.x, Head.transform.position.y);
        Head.AddForce(Direction * YeetStrength);
        CanYeet = false;
    }
}

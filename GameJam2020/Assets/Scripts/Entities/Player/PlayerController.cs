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

    [Tooltip("The maximum distance to determine the maximu strength.")]
    [SerializeField]
    private float MaxDistance = 100.0f;

    [Tooltip("The maximum thickness of the line.")]
    [SerializeField]
    private float THICC_ness = 2.0f;

    [Tooltip("The starting colour for the line")]
    [SerializeField]
    private Color StartColour = Color.white;

    [Tooltip("The ending colour for the line.")]
    [SerializeField]
    private Color EndColour = Color.red;

    [Tooltip("a refernce to the ui displayed")]
    [SerializeField]
    private OverlayUI UI = null;

    public Rigidbody2D Head;

    // Determines if the head can be yeeted.
    private bool CanYeet;

    //determines if the player can switch weapons
    private bool CanSwitch;

    // A reference to the attached line renderer.
    private LineRenderer Line = null;


    public void Start()
    {
        Cursor.visible = ShowCursor;
        Cursor.lockState = (ShowCursor) ? CursorLockMode.Confined : CursorLockMode.Locked;

        Line = GetComponent<LineRenderer>();
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

        if(Input.GetButtonDown("SwitchWeapon"))
        {
            if (CanSwitch)
            {
                Debug.Log("HMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM");
                UI.SwitchHead();
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
            CanSwitch = true;

        }

        Line.enabled = CanYeet;

        if (Line)
        {
            Line.SetPosition(0, Head.position);
            Line.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));

            float Percent = (Vector2.Distance(Head.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) / MaxDistance);
            Color LerpColor = Color.Lerp(StartColour, EndColour, Percent);
            Line.startColor = LerpColor;
            Line.endColor = LerpColor;

            Line.endWidth = Mathf.Lerp(0.01f, THICC_ness, Percent);
        }
    }



    public void Yeet(Vector2 YeetDirection)
    {
        Vector2 Direction = YeetDirection - new Vector2(Head.transform.position.x, Head.transform.position.y);
        Head.AddForce(Direction * YeetStrength);
        CanYeet = false;
        CanSwitch = false;
    }
}

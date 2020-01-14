using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Displays the cursor on the screen")]
    [SerializeField]
    private bool ShowCursor = true;

    [Tooltip("The maximum distance the user can zoom in.")]
    [SerializeField]
    private float MaxZoom = 1.0f;

    [Tooltip("The maximum distance the user can zoom out.")]
    [SerializeField]
    private float MinZoom = 20.0f;

    [Tooltip("Should the head keep the linear and angular drag after colliding with this object (if false after a short time the drag will be reset).")]
    [SerializeField]
    private bool KeepDrag = false;

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

    private OverlayUI UIInstance = null;

    // A reference to the head object.
    public HeadScript Head = null;

    // A reference to the head's rigid body.
    private Rigidbody2D HeadRigid = null;

    // Determines if the head can be yeeted.
    private bool CanYeet;

    //determines if the player can switch weapons
    private bool CanSwitch;

    // A reference to the attached line renderer.
    private LineRenderer Line = null;

    // The current direction the head is aimed in.
    private Vector2 Direction = Vector2.zero;


    public void Start()
    {
        HeadRigid = Head.GetComponent<Rigidbody2D>();

        Cursor.visible = ShowCursor;
        Cursor.lockState = (ShowCursor) ? CursorLockMode.Confined : CursorLockMode.Locked;

        Line = GetComponent<LineRenderer>();
        Line.startColor = StartColour;

        UIInstance = Instantiate(UI);
    }


    public void Update()
    {
        if (Input.GetButtonDown("Yeet"))
        {
            if (CanYeet)
            {
                Yeet(Direction);
            }
        }

        if (Input.GetButtonDown("SwitchWeapon"))
        {
            if (CanSwitch)
            {
                UIInstance.SwitchHead();
            }
        }

        if (Input.mouseScrollDelta.y != 0.0f)
        {
            Vector3 NewPos = transform.position;
            NewPos.z += Input.mouseScrollDelta.y;
            NewPos.z = Mathf.Clamp(NewPos.z, -MinZoom, -MaxZoom);
            transform.position = NewPos;
        }
    }


    public void FixedUpdate()
    {
        if (!CanYeet && HeadRigid.IsSleeping())
        {
            CanYeet = true;
            CanSwitch = true;

            if (!KeepDrag)
            {
                HeadRigid.angularDrag = 0.01f;
                HeadRigid.drag = 0.01f;
            }
        }

        Line.enabled = CanYeet;

        if (Line)
        {
            Ray Hit = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane HitPlane = new Plane(Vector3.forward, new Vector3(0.0f, 0.0f, 0.0f));
            float Distance;
            HitPlane.Raycast(Hit, out Distance);
            
            Vector2 MousePos = Hit.GetPoint(Distance);
            Direction = MousePos - HeadRigid.position;
            Direction = Vector2.ClampMagnitude(Direction, MaxDistance);

            Line.SetPosition(0, HeadRigid.position);
            Line.SetPosition(1, HeadRigid.position + Direction);

            float Percent = (Vector2.Distance(HeadRigid.position, MousePos) / MaxDistance);
            Color LerpColor = Color.Lerp(StartColour, EndColour, Percent);
            Line.endColor = LerpColor;

            Line.endWidth = Mathf.Lerp(0.01f, THICC_ness, Percent) + 0.1f;
        }
    }



    public void Yeet(Vector2 YeetDirection)
    {
        HeadRigid.AddForce(Direction * YeetStrength);
        Head.GetYeetSounds().Play();
        CanYeet = false;
        CanSwitch = false;
    }


    public void SetHead(HeadScript NewHead)
    {
        Head = NewHead;
        HeadRigid = Head.GetComponent<Rigidbody2D>();
    }
}

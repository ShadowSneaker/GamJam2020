using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [System.Serializable]
    public class SHeadPair
    {
        [Tooltip("A reference to the had prefab.")]
        public HeadScript Head;

        [Tooltip("The amount of uses this head has left.")]
        public int Count;


        public SHeadPair(HeadScript InHead)
        {
            Head = InHead;
            Count = InHead.ThrowsLeft;
        }
    }


    [Tooltip("Displays the cursor on the screen")]
    [SerializeField]
    private bool ShowCursor = true;

    [Tooltip("The maximum distance the user can zoom in.")]
    [SerializeField]
    private float MaxZoom = 1.0f;

    [Tooltip("The maximum distance the user can zoom out.")]
    [SerializeField]
    private float MinZoom = 20.0f;

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


    [Header("Heads")]


    [Tooltip("A list of head prefabs the user has.")]
    [SerializeField]
    private List<SHeadPair> Heads = new List<SHeadPair>();

    [Tooltip("The idnex that is currently being used.")]
    [SerializeField]
    private int HeadIndex = 0;

    public SHeadPair CurrentHead = null;

    [Tooltip("The inital spawn location of head")]
    [SerializeField]
    private Transform InitialLocation = null;



    private OverlayUI UIInstance = null;

    // A reference to the head object.
    //public HeadScript Head = null;

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

    // A reference to the follow script.
    private FollowScript Follow = null;


    // Settings


    internal bool EnableControls = true;
    internal bool DrawPowerBar = true;





    public void Start()
    {
        Cursor.visible = ShowCursor;
        Cursor.lockState = (ShowCursor) ? CursorLockMode.Confined : CursorLockMode.Locked;

        Line = GetComponent<LineRenderer>();
        Line.startColor = StartColour;

        UIInstance = Instantiate(UI);
        Follow = GetComponent<FollowScript>();

        SetHead(Heads[HeadIndex].Head);
    }


    public void Update()
    {
        if (EnableControls)
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
                    //UIInstance.SwitchHead();
                    SetHead(Heads[CycleHead()].Head);
                    
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
    }


    public void FixedUpdate()
    {
        if (!CanYeet && HeadRigid.IsSleeping())
        {
            CanYeet = true;
            CanSwitch = true;

            if (Heads[HeadIndex].Count <= 0 && Heads[HeadIndex].Head.ConsumeHead)
            {
                Heads.Remove(Heads[HeadIndex]);
                SetHead(Heads[CycleHead()].Head);
            }

            UIInstance.SetCounter((CurrentHead.Head.ConsumeHead) ? Heads[HeadIndex].Count : 1);
        }

        Line.enabled = CanYeet;

        if (Line && DrawPowerBar)
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
        CurrentHead.Head.Reset();
        HeadRigid.AddForce(Direction * YeetStrength);
        CurrentHead.Head.GetYeetSounds().Play();

        if (Heads[HeadIndex].Head.ConsumeHead)
            --Heads[HeadIndex].Count;

        CanYeet = false;
        CanSwitch = false;
    }


    public void SetHead(HeadScript NewHead)
    {
        if (ContainsHead(NewHead))
        {
            //Heads[GetHeadIndex(CurrentHead.Head)].SetCount(CurrentHead.Count);
            CurrentHead.Count = Heads[GetHeadIndex(NewHead)].Count;

            Vector3 Location = (InitialLocation) ? InitialLocation.position : Vector3.zero;
            if (CurrentHead.Head)
            {
                Location = CurrentHead.Head.transform.position;
                Destroy(CurrentHead.Head.gameObject);
            }


            CurrentHead.Head = Instantiate(NewHead, Location, Quaternion.identity);
            HeadRigid = CurrentHead.Head.GetComponent<Rigidbody2D>();
            Follow.FollowObject = CurrentHead.Head.transform;

            UIInstance.SetCounter((CurrentHead.Head.ConsumeHead) ? CurrentHead.Count : 1);
            UIInstance.SetHeadImage(CurrentHead.Head.Image);
        }
    }


    public void AddHead(HeadScript NewHead)
    {
        if (NewHead)
        {
            if (!ContainsHead(NewHead))
            {
                Heads.Add(new SHeadPair(NewHead));
            }
        }
    }


    public int CycleHead()
    {
        ++HeadIndex;
        if (HeadIndex >= Heads.Count)
        {
            HeadIndex = 0;
        }
        return HeadIndex;
    }


    private bool ContainsHead(HeadScript Head)
    {
        for (int i = 0; i < Heads.Count; ++i)
        {
            if (Heads[i].Head == Head) return true;
        }
        return false;
    }

    
    private int GetHeadIndex(HeadScript Head)
    {
        for (int i = 0; i < Heads.Count; ++i)
        {
            if (Heads[i].Head == Head) return i;
        }
        return -1;
    }


    private SHeadPair GetHeadPair(HeadScript Head)
    {
        for (int i = 0; i < Heads.Count; ++i)
        {
            if (Heads[i].Head == Head) return Heads[i];
        }
        return Heads[0];
    }


    public void LockPlayer()
    {
        HeadRigid.Sleep();
        EnableControls = false;
        DrawPowerBar = false;
    }


    public void UnlockPlayer()
    {
        HeadRigid.WakeUp();
        EnableControls = true;
        DrawPowerBar = true;
    }
}

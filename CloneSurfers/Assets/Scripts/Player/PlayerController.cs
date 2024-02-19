using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SwipeController),
                  typeof(Rigidbody),
                  typeof(PlayerStateManager))]
public class PlayerController : MonoBehaviour
{
    //--------States Enum--------
    public enum States
    {
        Ground,
        Jump,
        Slide,
        Hit,
        Dead
    }
    States _currentEnumState;
    public States CurrentEnumState { get { return _currentEnumState; } set { _currentEnumState = value; } }
    //----------------------------

    public Rigidbody RB { get; private set; }
    public Animator Anim { get; private set; }

    PlayerStateManager _stateManager;

    [SerializeField] LayerMask _groundLayerMask;
    public LayerMask GroundLayerMask { get { return _groundLayerMask; } }

    SwipeController _swipeController;
    HitManager _hitManager;
    //--------States--------
    public PlayerGroundRoot _groundRoot { get; private set; }
    public PlayerJumpRoot _jumpRoot { get; private set; }
    public PlayerSlideRoot _slideRoot { get; private set; }
    public PlayerHitRoot _hitRoot { get; private set; }
    //----------------------


    //--------Touch Controls--------
    Vector2 _startPosition;
    Vector2 _endPosition;
    [SerializeField] float _yGap, _xGap;
    //------------------------------

    //--------Ground Check--------
    [SerializeField] Transform _feet;
    [SerializeField] float _groundCheckRadius;
    //----------------------------
    //--------Collider Switch--------
    [SerializeField] Collider _standCollider, _slideCollider;
    public Collider StandCollider { get { return _standCollider; } }
    public Collider SlideCollider { get { return _slideCollider; } }

    Collider[] colliders;
    //-------------------------------

    int _minSpeed = 5;
    [SerializeField] float _currentMovementSpeed;
    public float Speed { get { return _currentMovementSpeed; } }
    RaycastHit slopeHit;



    //----------Hit-----------
    [SerializeField] Material _defaultBodyMaterial, _defaultHairMaterial;
    [SerializeField] Material _currentBodyMaterial, _currentHairMaterial;
    public Material BodyMaterial { get { return _currentBodyMaterial; } }
    public Material HairMaterial { get { return _currentHairMaterial; } }
    //------------------------
    private void Awake()
    {
        _currentBodyMaterial.color = _defaultBodyMaterial.color;
        _currentHairMaterial.color = _defaultHairMaterial.color;

        RB = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();
        colliders = new Collider[] { _standCollider, _slideCollider };
        _swipeController = GetComponent<SwipeController>();
        _hitManager = GetComponent<HitManager>();
        InitializeStates();

        _stateManager = GetComponent<PlayerStateManager>();
        _stateManager.SwitchState(_groundRoot);

        Input.simulateMouseWithTouches = true;

    }

    public bool IsOnGround()
    {
        return Physics.CheckSphere(_feet.position, _groundCheckRadius, GroundLayerMask);
    }

    void InitializeStates()
    {
        _groundRoot = new PlayerGroundRoot(this);
        _jumpRoot = new PlayerJumpRoot(this);
        _slideRoot = new PlayerSlideRoot(this);
        _hitRoot = new PlayerHitRoot(this);
    }
    public void TryToSwitchState(PlayerBaseState newState, States enumState)
    {
        if (enumState == _currentEnumState)
            if (_stateManager.CurrentState != newState)
                _stateManager.SwitchState(newState);
    }
    public void SwitchColliders(Collider selectedCollider)
    {
        selectedCollider.gameObject.SetActive(true);

        for (int i = 0; i < colliders.Length; i++)
            if (selectedCollider != colliders[i])
                colliders[i].gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGameStarted)
            RB.velocity = new Vector3(RB.velocity.x, RB.velocity.y, Speed);
        else 
            transform.position = _swipeController.StartPosition;
        _stateManager.CurrentState.UpdateState();

        _currentMovementSpeed = _minSpeed + GameManager.Instance._scoreMultiplier; 
    }

    public bool IsOnSlope()
    {
        if (Physics.Raycast(_feet.position, Vector3.down, out slopeHit, 0.05f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < 80;
        }
        return false;
    }
    public string NextCommand()
    {
        if (!GameManager.Instance.IsGameStarted)
        {
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                GameManager.Instance.IsGameStarted = true;
                Anim.SetBool("GameStarted", true);
            }
        }
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            _startPosition = Input.mousePosition;
            //    _startPosition = Input.GetTouch(0).position;
        }
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
        {
            _endPosition = Input.mousePosition;
            //_endPosition = Input.GetTouch(0).position;

            float y = _startPosition.y - _endPosition.y;
            float x = _startPosition.x - _endPosition.x;
            if (Mathf.Abs(y) > Mathf.Abs(x))
            {
                if (y < -_yGap)
                    return "Jump";
                else if (y > _yGap)
                    return "Slide";
            }
            else
            {
                if (x < -_xGap)
                    _swipeController.MoveTo(right: true);
                else if (x > _xGap)
                    _swipeController.MoveTo(right: false);
            }

        }
        return "No-Command";
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "AddMap")
            if (collision.transform.parent.GetComponent<MapSetup>().CanAddNewMap)
            {
                GameManager.Instance.MapManager.AddMap();
                collision.transform.parent.GetComponent<MapSetup>().MapCreated();
            }
        if (collision.tag == "Interactable")
            collision.GetComponent<IInteractable>().OnAction();

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            _hitManager.StartHit();
        }        
        if(collision.gameObject.tag == "Ground")
        {
            if(RB.velocity.z == 0)
            {
                Debug.Log("Bom");
            }
        }
    
    }

    private void OnApplicationQuit()
    {
        _hitRoot.ReturnToNormal();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_feet.position, _groundCheckRadius);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] Transform orientation;
    [SerializeField] float baseMoveSpeed;
    [SerializeField] float groundDrag;
    [SerializeField] float baseJumpForce;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airMultiplier;

    public float moveSpeed;
    float tinyMoveSpeed;
    float jumpForce;
    
    bool readyToJump = true;

    [Header("Ground Check")]
    [SerializeField] float originalPlayerHeight;
    [SerializeField] LayerMask whatIsGround;
    public bool grounded;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    // [Header("Cameras")]
    // [SerializeField] CinemachineVirtualCamera normalCam;
    // [SerializeField] CinemachineVirtualCamera burrowingCam;
 
    float horizontalInput;
    float verticalInput;

    public float playerHeight;

    public float OriginalPlayerHeight {get{return originalPlayerHeight;}}
    public float BaseJumpForce {get{return baseJumpForce;}}
    public float JumpForce {get{return jumpForce;} set{jumpForce = value;}}

    //jump support
    float fallMulitiplier = 2.5f;
    float lowJumpMulitiplier = 2f;
    float fallGravity;
    float lowJumpGravity;
    bool isJumpPressed;
    [Header("Ground Check")]
    [SerializeField] Vector3 boxSize;
    [SerializeField] float maxDistance;
    
    Vector3 moveDirection;

    Rigidbody rb;
    SurfaceAngle surfaceAngleScript;
    float surfaceAngle;

    //Animation support
    bool isMovementPressed = false;
    Animator animator;
    private int isWalkingHash;
    private int isRunningHash;
    private int isJumpingHash;
    public bool isBurrowing = false;
    public bool echoLocation = false;
    public bool isStrong = false;
    public bool isTiny = false;
    public bool highJump = false;

    float groundedResetTime = 2.5f;

    Vector3 offsetVector;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        surfaceAngleScript = GetComponent<SurfaceAngle>();
        animator = GetComponentInChildren<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
    }

    void Start()
    {
        fallGravity = Physics.gravity.y * (fallMulitiplier - 1);
        lowJumpGravity = Physics.gravity.y * (lowJumpMulitiplier - 1);
        playerHeight = originalPlayerHeight;
        jumpForce = baseJumpForce;

        tinyMoveSpeed = baseMoveSpeed * 0.5f;

        offsetVector = new Vector3(0, playerHeight, 0);
    }

    // void OnEnable() 
    // {
    //     CameraSwitcher.Register(normalCam);
    //     CameraSwitcher.Register(burrowingCam);
    // }
    
    // void OnDisable() 
    // {
    //     CameraSwitcher.Unregister(normalCam);
    //     CameraSwitcher.Unregister(burrowingCam);
    // }

    // Update is called once per frame
    void Update()
    {
        //ground check
        GroundCheck();
        if(!grounded)
        {
            groundedResetTime -= Time.deltaTime;
            if(groundedResetTime <= 0 || grounded)
            {
                grounded = true;
                groundedResetTime = 2.5f;
            }
        }

        MyInput();
        SpeedControl();
    }

    void FixedUpdate() 
    {
        MovePlayer();

        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * fallGravity * Time.deltaTime;
        }else if(rb.velocity.y < 0 && !Input.GetKey(jumpKey))
        {
            rb.velocity += Vector3.up * lowJumpGravity * Time.deltaTime;
        }
    }

    void GroundCheck()
    {
        // grounded = Physics.Raycast(transform.position, Vector3.down, 0.1f, whatIsGround);
        grounded = Physics.Raycast(transform.position + offsetVector, Vector3.down, playerHeight + 0.2f);
    }

    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(horizontalInput == 0 && verticalInput == 0)
        {
            isMovementPressed = false;
            animator.SetBool(isWalkingHash, false);
        }
        else
        {
            isMovementPressed = true;
            animator.SetBool(isWalkingHash, true);
        }
        
        isJumpPressed = Input.GetKey(jumpKey);
        // if(Input.GetKey(jumpKey) && readyToJump && grounded)
        if(Input.GetKey(jumpKey) && readyToJump)
        {
            readyToJump = false;
            Jump();
            animator.SetBool(isJumpingHash, true);
            Invoke(nameof(ResetJump), jumpCooldown);
        }
        else if(!isJumpPressed && grounded)
        {
            animator.SetBool(isJumpingHash, false);
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = baseMoveSpeed * 1.5f;
            if(isMovementPressed)
            {
                animator.SetBool(isRunningHash, true);
            }
        }
        else
        {
            moveSpeed = baseMoveSpeed;
            animator.SetBool(isRunningHash, false);
        }
    }

    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(isTiny)
        {
            moveSpeed = tinyMoveSpeed;
        }
        else
        {
            moveSpeed = baseMoveSpeed;
        }

        //on ground
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10, ForceMode.Force);
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10 * airMultiplier, ForceMode.Force);
    }

    void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    public void Jump()
    {
        if(highJump)
        {
            jumpForce = baseJumpForce * 3f;
        }
        else
        {
            jumpForce = baseJumpForce;
        }

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
        readyToJump = true;
    }
}

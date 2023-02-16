using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] Transform orientation;
    [SerializeField] float baseMoveSpeed;
    [SerializeField] float groundDrag;
    [SerializeField] float baseJumpForce;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airMultiplier;

    float moveSpeed;
    float jumpForce;
    
    bool readyToJump = true;

    [Header("Ground Check")]
    [SerializeField] float originalPlayerHeight;
    [SerializeField] LayerMask whatIsGround;
    public bool grounded;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

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
    
    Vector3 moveDirection;

    Rigidbody rb;
    AbilityHolder abilityHolder;

    public bool isBurrowing = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        abilityHolder = GetComponent<AbilityHolder>();
    }

    void Start()
    {
        fallGravity = Physics.gravity.y * (fallMulitiplier - 1);
        lowJumpGravity = Physics.gravity.y * (lowJumpMulitiplier - 1);
        playerHeight = originalPlayerHeight;
        jumpForce = baseJumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();

        //handle drag
        if(grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
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

    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = baseMoveSpeed * 1.5f;
        }
        else
        {
            moveSpeed = baseMoveSpeed;
        }
    }

    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

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
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
        readyToJump = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Copy : MonoBehaviour
{
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    //variables to store optimized setter/getter parameter ids
    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;
    int JumpCountHash;

    //Movement Support
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    Vector3 appliedMovement;
    float movementSpeed = 2f;
    bool isMovementPressed;
    bool isRunPressed;
    float runMulitplier = 2.0f;

    //Gravity Support
    float groundedGravity = -0.05f;
    float gravity = -9.8f;
    float previousYVelocity;
    float newYVelocity;
    float nextYVelocity;
    
    //falling gravity change support
    bool isFalling;
    float fallMultiplier = 2.0f;

    // Animation Support
    bool isWalking;
    bool isRunning;

    //Rotation Support
    Vector3 positionToLookAt;
    Quaternion currentRotation;
    Quaternion targetRotation;
    float rotationFactorPerFrame = 6f;

    //jump support
    bool isJumpPressed = false;
    float initialJumpVelocity;
    float maxJumpHeight = 1f;
    float maxJumpTime = 0.75f;
    float timeToApex;
    bool isJumping = false;
    bool isJumpAnimating = false;
    int JumpCount = 0;
    Dictionary<int, float> initialJumpVelocities = new Dictionary<int, float>();
    Dictionary<int, float> jumpGravities = new Dictionary<int, float>();
    float secondJumpGravity;
    float secondJumpInitialVelocity;
    float thirdJumpGravity;
    float thirdJumpInitialVelocity;
    Coroutine currentJumpResetRoutine = null;

    void Awake() 
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
        JumpCountHash = Animator.StringToHash("jumpCount");

        playerInput.CharacterControls.Move.started += OnMovementInput;
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
        playerInput.CharacterControls.Move.performed += OnMovementInput;
        playerInput.CharacterControls.Run.started += OnRun;
        playerInput.CharacterControls.Run.canceled += OnRun;
        playerInput.CharacterControls.Jump.started += OnJump;
        playerInput.CharacterControls.Jump.canceled += OnJump;

        SetupJumpVariables();
    }

    void Update()
    {
        HandleRotation();
        HandleAnimation();
        if(isRunPressed)
        {
            appliedMovement.x = currentRunMovement.x;
            appliedMovement.z = currentRunMovement.z;
        }
        else
        {
            appliedMovement.x = currentMovement.x;
            appliedMovement.z = currentMovement.z;
        }

        characterController.Move(appliedMovement * movementSpeed * Time.deltaTime);

        HandleGravity();
        HandleJump();
    }

    void SetupJumpVariables()
    {
        timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
        secondJumpGravity = (-2 * (maxJumpHeight + 0.5f)) / Mathf.Pow(timeToApex, 2);
        secondJumpInitialVelocity = (2 * (maxJumpHeight + 0.5f)) / timeToApex;
        thirdJumpGravity = (-2 * (maxJumpHeight + 1)) / Mathf.Pow(timeToApex, 2);
        thirdJumpInitialVelocity = (2 * (maxJumpHeight + 1)) / timeToApex;

        initialJumpVelocities.Add(1, initialJumpVelocity);
        initialJumpVelocities.Add(2, secondJumpInitialVelocity);
        initialJumpVelocities.Add(3, thirdJumpInitialVelocity);

        jumpGravities.Add(0, gravity);
        jumpGravities.Add(1, gravity);
        jumpGravities.Add(2, secondJumpGravity);
        jumpGravities.Add(3, thirdJumpGravity);
    }

    void HandleJump()
    {
        if(!isJumping && characterController.isGrounded && isJumpPressed)
        {
            if(JumpCount < 3 && currentJumpResetRoutine != null)
            {
                StopCoroutine(currentJumpResetRoutine);
            }
            animator.SetBool(isJumpingHash, true);
            isJumpAnimating = true;
            isJumping = true;
            JumpCount += 1;
            animator.SetInteger(JumpCountHash, JumpCount);
            currentMovement.y = initialJumpVelocities[JumpCount];
            appliedMovement.y = initialJumpVelocities[JumpCount];
        }
        else if(!isJumpPressed && isJumping && characterController.isGrounded)
        {
            isJumping = false;
        }
    }

    IEnumerator JumpResetRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        JumpCount = 0;
    }

    void OnRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    void OnJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        currentRunMovement.x = currentMovementInput.x * runMulitplier;
        currentRunMovement.z = currentMovementInput.y * runMulitplier;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void HandleAnimation()
    {
        isWalking = animator.GetBool(isWalkingHash);
        isRunning = animator.GetBool(isRunningHash);

        if(isMovementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if((isMovementPressed && isRunPressed) && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }
        else if(!isRunPressed && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }
    }

    void HandleGravity()
    {
        isFalling = currentMovement.y <= 0.0f || !isJumpPressed;
        if(characterController.isGrounded)
        {
            if(isJumpAnimating)
            {
                animator.SetBool(isJumpingHash, false);
                isJumpAnimating = false;
                currentJumpResetRoutine = StartCoroutine(JumpResetRoutine());
                if(JumpCount == 3)
                {
                    JumpCount = 0;
                    animator.SetInteger(JumpCountHash, JumpCount);
                }
            }
            currentMovement.y = groundedGravity;
            appliedMovement.y = groundedGravity;
        }
        else if(isFalling)
        {
            previousYVelocity = currentMovement.y;
            currentMovement.y = currentMovement.y + (jumpGravities[JumpCount] * fallMultiplier * Time.deltaTime);
            appliedMovement.y = Mathf.Max((previousYVelocity + currentMovement.y) * 0.5f, -20.0f);
        }
        else{
            previousYVelocity = currentMovement.y;
            currentMovement.y = currentMovement.y + (jumpGravities[JumpCount] * Time.deltaTime);
            appliedMovement.y = (previousYVelocity + currentMovement.y) * 0.5f;
        }
    }

    void HandleRotation()
    {
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        currentRotation = transform.rotation;

        if(isMovementPressed)
        {
            targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    void OnEnable() 
    {
        playerInput.CharacterControls.Enable();
    }

    void OnDisable()  
    {
        playerInput.CharacterControls.Disable();
    }
}

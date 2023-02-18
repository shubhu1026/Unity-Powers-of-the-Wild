using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    //variables to store optimized setter/getter parameter ids
    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;
    int isFallingHash;
    int jumpCountHash;

    //Movement Support
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    Vector3 appliedMovement;
    float movementSpeed = 2f;
    bool isMovementPressed;
    bool isRunPressed;
    float runMultiplier = 2.0f;

    //Gravity Support
    float gravity = -9.8f;
    float previousYVelocity;
    float newYVelocity;
    float nextYVelocity;
    
    //falling gravity change support
    bool isFalling;
    //float fallMultiplier = 2.0f;

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
    int jumpCount = 0;
    Dictionary<int, float> initialJumpVelocities = new Dictionary<int, float>();
    Dictionary<int, float> jumpGravities = new Dictionary<int, float>();
    float secondJumpGravity;
    float secondJumpInitialVelocity;
    float thirdJumpGravity;
    float thirdJumpInitialVelocity;
    bool requireNewJumpPress = false;
    Coroutine currentJumpResetRoutine = null;

    //state variables
    PlayerBaseState currentState;
    PlayerStateFactory states;

    //getters and setters
    public PlayerBaseState CurrentState {get { return currentState;} set {currentState = value;}}
    public Animator Animator { get{return animator;}}
    public Coroutine CurrentJumpResetRoutine { get{return currentJumpResetRoutine;} set { currentJumpResetRoutine = value;}}
    public Dictionary<int, float> InitialJumpVelocities {get { return initialJumpVelocities;} set {initialJumpVelocities = value;}}
    public int JumpCount {get { return jumpCount;} set {jumpCount = value;}}
    public int JumpCountHash {get { return jumpCountHash;}}
    public int IsJumpingHash {get { return isJumpingHash;} set {isJumpingHash = value;}}
    public int IsWalkingHash {get { return isWalkingHash;} set {isWalkingHash = value;}}
    public int IsRunningHash {get { return isRunningHash;} set {isRunningHash = value;}}
    public int IsFallingHash {get { return isFallingHash;} set {isFallingHash = value;}}
    public bool IsJumping {get { return isJumping;} set {isJumping = value;}}
    public bool IsJumpPressed {get { return isJumpPressed;} set {isJumpPressed = value;}}
    public float CurrentMovementY {get { return currentMovement.y;} set {currentMovement.y = value;}}
    public float AppliedMovementY {get { return appliedMovement.y;} set {appliedMovement.y = value;}}
    public float AppliedMovementX {get { return appliedMovement.x;} set {appliedMovement.x = value;}}
    public float AppliedMovementZ {get { return appliedMovement.z;} set {appliedMovement.z = value;}}
    public float RunMultiplier {get{return runMultiplier;}}
    public Vector2 CurrentMovementInput {get{return currentMovementInput;}}
    public float Gravity {get{return gravity;} set{gravity = value;}}
    public CharacterController CharacterController{get { return characterController;}}
    public Dictionary<int, float> JumpGravities{get{return jumpGravities;} set{jumpGravities = value;}}
    public bool RequireNewJumpPress { get{return requireNewJumpPress;} set{requireNewJumpPress = value;}}
    public bool IsMovementPressed {get{return isMovementPressed;}}
    public bool IsRunPressed{ get {return isRunPressed;}}

    void Awake() 
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        //setup state
        states = new PlayerStateFactory(this);
        currentState = states.Grounded();
        currentState.EnterState();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
        isFallingHash = Animator.StringToHash("isFalling");
        jumpCountHash = Animator.StringToHash("jumpCount");

        playerInput.CharacterControls.Move.started += OnMovementInput;
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
        playerInput.CharacterControls.Move.performed += OnMovementInput;
        playerInput.CharacterControls.Run.started += OnRun;
        playerInput.CharacterControls.Run.canceled += OnRun;
        playerInput.CharacterControls.Jump.started += OnJump;
        playerInput.CharacterControls.Jump.canceled += OnJump;

        SetupJumpVariables();
    }

    void Start() 
    {
        characterController.Move(appliedMovement * movementSpeed * Time.deltaTime);
    }

    void Update()
    {
        HandleRotation();
        currentState.UpdateStates();
        characterController.Move(appliedMovement * movementSpeed * Time.deltaTime);
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

    void SetupJumpVariables()
    {
        timeToApex = maxJumpTime / 2;
        float initialGravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
        secondJumpGravity = (-2 * (maxJumpHeight + 0.5f)) / Mathf.Pow(timeToApex, 2);
        secondJumpInitialVelocity = (2 * (maxJumpHeight + 0.5f)) / timeToApex;
        thirdJumpGravity = (-2 * (maxJumpHeight + 1)) / Mathf.Pow(timeToApex, 2);
        thirdJumpInitialVelocity = (2 * (maxJumpHeight + 1)) / timeToApex;

        initialJumpVelocities.Add(1, initialJumpVelocity);
        initialJumpVelocities.Add(2, secondJumpInitialVelocity);
        initialJumpVelocities.Add(3, thirdJumpInitialVelocity);

        jumpGravities.Add(0, initialGravity);
        jumpGravities.Add(1, initialGravity);
        jumpGravities.Add(2, secondJumpGravity);
        jumpGravities.Add(3, thirdJumpGravity);
    }

    void OnRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    void OnJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
        requireNewJumpPress = false;
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        currentRunMovement.x = currentMovementInput.x * runMultiplier;
        currentRunMovement.z = currentMovementInput.y * runMultiplier;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
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

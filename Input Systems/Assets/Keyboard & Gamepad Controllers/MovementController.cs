using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    public PlayerInput playerInput;
    private Animator animator;

    int isRunningHash;
    int isWalkingHash;
    int isJumpingHash;

    private Vector2 currentMovementInput;
    private bool movementPressed;
    private bool runPressed;
    private bool jumpPressed;
    private void Awake()
    {
        playerInput = new PlayerInput();

        playerInput.CharacterControls.Movement.performed += ctx =>
        {
            currentMovementInput = ctx.ReadValue<Vector2>();
            movementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
            
            if (!movementPressed)
            {
                animator.SetBool(isWalkingHash, false);
            }
        };
        playerInput.CharacterControls.Run.performed += ctx => runPressed = ctx.ReadValueAsButton();
        playerInput.CharacterControls.Jump.started += ctx => jumpPressed = true;
        playerInput.CharacterControls.Jump.canceled += ctx => jumpPressed = false;
    }

    void Start()
    {
        animator = GetComponent<Animator>();

        isRunningHash = Animator.StringToHash("isRunning");
        isWalkingHash = Animator.StringToHash("isWalking");
        isJumpingHash = Animator.StringToHash("isJumping");
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    public void HandleMovement()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isJumping = animator.GetBool(isJumpingHash);

        if (!jumpPressed)
        {
            animator.SetBool(isJumpingHash, false);
        }

        if (movementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        else if (!movementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if (movementPressed && runPressed && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }
        else if ((!movementPressed || !runPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }
        
        if (jumpPressed && !isJumping)
        {
            animator.SetBool(isJumpingHash, true);
        }
    }

    void HandleRotation()
    {
        Vector3 currentPositon = transform.position;

        Vector3 newPosition = new Vector3(currentMovementInput.x, 0, currentMovementInput.y);

        Vector3 lookAtPosition = currentPositon + newPosition;

        transform.LookAt(lookAtPosition);
    }
    
    void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
    
    

}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    CharacterController character_obj;
    Vector3 cam_rotation;
    public float moveSpeed = 1f;
    public float walkScale = 0.3f;
    public float runScale = 1f;
    public float rotationSpeed = 10f; // Adjust the rotation speed as needed
    public Camera cam_obj;
    private PlayerInput playerInput;
    private Vector2 moveInput;
    private bool isJumping;
    private Animator animator;
    
    // Start is called before the first frame update
    void Awake()
    {
        //Application.targetFrameRate = 30;
    }
    void Start()
    {
        character_obj = GetComponent<CharacterController>();

        playerInput = GetComponent<PlayerInput>();

        animator = GetComponent<Animator>();

        playerInput.actions["Horizontal"].performed += OnHorizontalPerformed;
        playerInput.actions["Horizontal"].canceled += OnHorizontalCanceled;
        playerInput.actions["Vertical"].performed += OnVerticalPerformed;
        playerInput.actions["Vertical"].canceled += OnVerticalCanceled;
        playerInput.actions["ButtonA"].performed += OnButtonAPerformed;
    }

    void OnHorizontalPerformed(InputAction.CallbackContext context)
    {
        moveInput.x = context.ReadValue<float>();
    }
    void OnHorizontalCanceled(InputAction.CallbackContext context)
    {
        moveInput.x = 0f;
    }
    void OnVerticalPerformed(InputAction.CallbackContext context)
    {
        moveInput.y = context.ReadValue<float>();
    }
    void OnVerticalCanceled(InputAction.CallbackContext context)
    {
        moveInput.y = 0f;
    }
    void OnButtonAPerformed(InputAction.CallbackContext context)
    {
        isJumping = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isJumping)
        {
            isJumping = false;
        }

        float horizontalInput = moveInput.x;
        float verticalInput = moveInput.y;
        Vector3 camForward = cam_obj.transform.forward;
        Vector3 camRight = cam_obj.transform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        Vector3 moveDirection = (camForward.normalized * verticalInput + camRight.normalized * horizontalInput).normalized;
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        float magnitudeTotal = moveInput.magnitude > 0f ? (moveInput.magnitude > 0.5f ? runScale : walkScale ) : 0f;
        if (magnitudeTotal == runScale){
            animator.SetBool("Idle", false); animator.SetBool("Walk", false); animator.SetBool("Run", true);
        }
        else if (magnitudeTotal == walkScale){
            animator.SetBool("Idle", false); animator.SetBool("Walk", true); animator.SetBool("Run", false);
        }
        else{
            animator.SetBool("Idle", true); animator.SetBool("Walk", false); animator.SetBool("Run", false);
        }
        Vector3 moveResultant = moveDirection * moveSpeed * magnitudeTotal * Time.deltaTime;
        
        character_obj.Move(moveResultant);
    }
    private void OnDestroy()
    {
        playerInput.actions["Horizontal"].performed -= OnHorizontalPerformed;
        playerInput.actions["Vertical"].performed -= OnVerticalPerformed;
        playerInput.actions["ButtonA"].performed -= OnButtonAPerformed;
    }
}

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
    public float rotationSpeed = 10f; // Adjust the rotation speed as needed
    public Camera cam_obj;
    private PlayerInput playerInput;
    private Vector2 moveInput;
    private bool isJumping;
    
    // Start is called before the first frame update
    void Awake()
    {
        //Application.targetFrameRate = 30;
    }
    void Start()
    {
        character_obj = GetComponent<CharacterController>();

        playerInput = GetComponent<PlayerInput>();

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
        character_obj.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Get input axes relative to camera
        /*moveInput = new Vector3(moveInput.x * moveSpeed, 0f, moveInput.y * moveSpeed);
        Vector3 moveDirection = Vector3.Normalize(Vector3.ProjectOnPlane(cam_obj.transform.forward, Vector3.up)) * moveInput.z + Vector3.Normalize(Vector3.ProjectOnPlane(cam_obj.transform.right, Vector3.up)) * moveInput.x;

        // Rotate towards movement direction (if there's any input)
        if (moveDirection.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        Debug.Log(moveDirection.normalized.x);*/
        // Smoothly move the character
        //Vector3 movement = new Vector3(moveDirection.normalized.x * moveSpeed * Time.deltaTime, 0f, moveDirection.normalized.z * moveSpeed * Time.deltaTime);
        //character_obj.Move(movement);

        /*moveSpeed = new Vector3(movespeed_multiplier * moveInput.x, 0, movespeed_multiplier * moveInput.y);
        cam_rotation = cam_obj.transform.TransformDirection(moveSpeed); cam_rotation.Set(cam_rotation.x, 0, cam_rotation.z);

        if (character_obj.isGrounded) { 
            moveapply = cam_rotation.normalized * moveSpeed.magnitude; moveapply.Set(moveapply.x, 0, moveapply.z); 
        }
        else {
            //moveapply = cam_rotation.normalized * moveSpeed.magnitude; moveapply.Set(moveapply.x, -9.81f * Time.deltaTime, moveapply.z);
            moveapply = cam_rotation.normalized * moveSpeed.magnitude; moveapply.Set(moveapply.x, 0, moveapply.z);
        }
        if (cam_rotation.normalized != Vector3.zero){
            Quaternion targetRotation = Quaternion.LookRotation(cam_rotation.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        character_obj.Move(moveapply);*/
        //character_obj.Move(moveSpeed);
    }
    private void OnDestroy()
    {
        playerInput.actions["Horizontal"].performed -= OnHorizontalPerformed;
        playerInput.actions["Vertical"].performed -= OnVerticalPerformed;
        playerInput.actions["ButtonA"].performed -= OnButtonAPerformed;
    }
}

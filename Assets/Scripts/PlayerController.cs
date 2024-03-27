using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    CharacterController character_obj;
    Vector3 move_speed; Vector3 cam_rotation; Vector3 moveapply; public float movespeed_multiplier = 1f;
    public Camera cam_obj;
    private PlayerInput playerInput;
    private bool gamepad_mode = false;
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
        move_speed = new Vector3(movespeed_multiplier * moveInput.x, 0, movespeed_multiplier * moveInput.y);
        cam_rotation = cam_obj.transform.TransformDirection(move_speed); cam_rotation.Set(cam_rotation.x, 0, cam_rotation.z);
        if (character_obj.isGrounded) { moveapply = cam_rotation.normalized * move_speed.magnitude; moveapply.Set(moveapply.x, 0, moveapply.z); }
        else { moveapply = cam_rotation.normalized * move_speed.magnitude; moveapply.Set(moveapply.x, -9.81f * Time.deltaTime, moveapply.z); }
        character_obj.Move(moveapply);
        //character_obj.Move(move_speed);
    }
    private void OnDestroy()
    {
        playerInput.actions["Horizontal"].performed -= OnHorizontalPerformed;
        playerInput.actions["Vertical"].performed -= OnVerticalPerformed;
        playerInput.actions["ButtonA"].performed -= OnButtonAPerformed;
    }
}

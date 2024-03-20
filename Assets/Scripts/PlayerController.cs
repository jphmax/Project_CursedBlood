using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController character_obj;
    Vector3 move_speed; Vector3 cam_rotation; Vector3 moveapply; public float movespeed_multiplier = 1f;
    public Camera cam_obj;
    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 30;
    }
    void Start()
    {
        character_obj = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        move_speed = new Vector3(movespeed_multiplier * Input.GetAxis("Horizontal"), 0, movespeed_multiplier * Input.GetAxis("Vertical"));
        cam_rotation = cam_obj.transform.TransformDirection(move_speed); cam_rotation.Set(cam_rotation.x, 0, cam_rotation.z);
        if (character_obj.isGrounded) { moveapply = cam_rotation.normalized * move_speed.magnitude; moveapply.Set(moveapply.x, 0, moveapply.z); }
        else { moveapply = cam_rotation.normalized * move_speed.magnitude; moveapply.Set(moveapply.x, -9.81f * Time.deltaTime, moveapply.z); }
        character_obj.Move(moveapply);
        //character_obj.Move(move_speed);
    }
}

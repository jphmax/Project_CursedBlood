using UnityEngine;
using UnityEngine.InputSystem;

public class InputScript : MonoBehaviour
{
    private PlayerInput playerInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        playerInput.actions["KeyboardMouseSenseBtn"].performed += OnKeyboardMouseSenseBtnPerformed;
        playerInput.actions["ControllerSenseBtn"].performed += OnControllerSenseBtnPerformed;
        playerInput.actions["MouseMoveSense"].performed += OnMouseMoveSensePerformed;
        playerInput.actions["ControllerAnalogSense"].performed += OnControllerAnalogSensePerformed;
    }

    void OnKeyboardMouseSenseBtnPerformed(InputAction.CallbackContext context)
    {
        //Debug.Log("KeyboardMouseSenseBtn");
    }

    void OnControllerSenseBtnPerformed(InputAction.CallbackContext context)
    {
        //Debug.Log("ControllerSenseBtn");
    }

    void OnMouseMoveSensePerformed(InputAction.CallbackContext context)
    {
        //Debug.Log("MouseMoveSense");
    }

    void OnControllerAnalogSensePerformed(InputAction.CallbackContext context)
    {
        //Debug.Log("ControllerAnalogSense");
    }

    private void Update()
    {
    }

    private void OnDestroy()
    {
        playerInput.actions["KeyboardMouseSenseBtn"].performed -= OnKeyboardMouseSenseBtnPerformed;
        playerInput.actions["ControllerSenseBtn"].performed -= OnControllerSenseBtnPerformed;
        playerInput.actions["MouseMoveSense"].performed -= OnMouseMoveSensePerformed;
        playerInput.actions["ControllerAnalogSense"].performed -= OnControllerAnalogSensePerformed;
    }
}

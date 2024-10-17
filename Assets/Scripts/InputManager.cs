using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public UnityEvent<Vector3> onUpdateCameraRotation;

    private Vector3 cameraRotation;

    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;

    private PlayerMotor motor;
    private PlayerCore core;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.OnFoot.Enable();
        onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        core = GetComponent<PlayerCore>();

        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.Sprint.performed += ctx => motor.Sprint();

        onFoot.Shoot.performed += core.Shoot;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    public void UpdateCameraRotation(Vector3 rotation)
    {
        cameraRotation = rotation;
        motor.ProcessLook(cameraRotation);
        onUpdateCameraRotation?.Invoke(rotation);
    }

    public Vector2 GetLook()
    {
        return playerInput.OnFoot.Look.ReadValue<Vector2>();
    }

    //private void LateUpdate()
    //{
    //    look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    //}

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
}

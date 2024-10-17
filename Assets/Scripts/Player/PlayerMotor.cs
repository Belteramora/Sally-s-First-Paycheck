using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private Transform cameraTransform;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool crouching;
    private bool lerpCrouch;
    private bool sprinting;
    private float crouchTimer = 0f;

    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;


    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        controller = GetComponent<CharacterController>();    
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;

            float p = crouchTimer / 1;
            p *= p;

            if (crouching)
                controller.height = Mathf.Lerp(controller.height, 1, p);
            else
                controller.height = Mathf.Lerp(controller.height, 2, p);

            if(p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }

    public void ProcessLook(Vector3 cameraRotation)
    {
        transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, cameraRotation.y, transform.eulerAngles.z));
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;

        moveDirection = transform.forward * input.y + transform.right * input.x;

        controller.Move(moveDirection * Time.deltaTime * speed);


        playerVelocity.y += gravity * Time.deltaTime;

        //TODO: немного непонятно насчет такого мува с гравитацией. Разобраться попозже, возможно заменить на другое
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;

        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Sprint()
    {
        sprinting = !sprinting;

        if (sprinting)
            speed = 8;
        else
            speed = 5;
    }
}

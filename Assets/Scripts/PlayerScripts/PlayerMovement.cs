using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float jumpHeight = 3f;
    public float crouchSpeed = 4f;
    public float sprintSpeed = 20f;
    private float initialZRotation;
    private float initialYRotation;
    private bool isPeeking = false;
    private bool isPeekingLeft = false;
    private bool isPeekingRight = false;

    Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        CheckJump();
        CheckSprint();
        CheckCrouch();
        CheckPeek();
        velocity.y += (Physics.gravity.y * 1.5f) * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    private void CheckJump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }
    }

    private void CheckSprint()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }

        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 12f;
        }
    }

    private void CheckCrouch()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            speed = crouchSpeed;
        }

        else if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            speed = 12f;
        }
    }

    private void CheckPeek()
{
    if (Input.GetKeyDown(KeyCode.Q))
    {
        if (!isPeekingLeft)
        {
            initialZRotation = transform.rotation.eulerAngles.z;
            initialYRotation = transform.rotation.eulerAngles.y;
            transform.Rotate(0, 0, 10f);
            isPeekingLeft = true;
            isPeekingRight = false;
        }
    }
    else if (Input.GetKeyUp(KeyCode.Q))
    {
        if (isPeekingLeft)
        {
            transform.rotation = Quaternion.Euler(0, initialYRotation, initialZRotation);
            isPeekingLeft = false;
        }
    }

    if (Input.GetKeyDown(KeyCode.E))
    {
        if (!isPeekingRight)
        {
            initialZRotation = transform.rotation.eulerAngles.z;
            initialYRotation = transform.rotation.eulerAngles.y;
            transform.Rotate(0, 0, -10f);
            isPeekingRight = true;
            isPeekingLeft = false;
        }
    }
    else if (Input.GetKeyUp(KeyCode.E))
    {
        if (isPeekingRight)
        {
            Debug.Log("Peek bırakıldı");
            transform.rotation = Quaternion.Euler(0, initialYRotation, initialZRotation);
            isPeekingRight = false;
        }
    }
}
}

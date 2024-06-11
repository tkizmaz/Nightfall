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
    Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    // Update is called once per frame
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

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }

        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 12f;
        }

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            speed = crouchSpeed;
        }

        else if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            speed = 12f;
        }

        velocity.y += (Physics.gravity.y * 1.5f) * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        Debug.Log(speed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

enum MovementState
{
    Immobile,
    Mobile
}

public enum PlayerState
{
    Idle,
    Walking,
    Crouching,
    Sprinting,
    Jumping
}

[System.Serializable]
public class PlayerWalkingEvent : UnityEvent<bool> {}

[System.Serializable]
public class PlayerSprintEvent : UnityEvent<bool> {}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float jumpHeight = 3f;
    [SerializeField]
    private float crouchSpeed = 4f;
    [SerializeField]
    private float sprintSpeed = 20f;
    private float initialZRotation;
    private float initialYRotation;
    private bool isPeekingLeft = false;
    private bool isPeekingRight = false;
    private Vector3 velocity;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField] 
    private float groundDistance = 0.4f;
    [SerializeField]
    private LayerMask groundMask;
    [SerializeField]
    private LayerMask obstacleMask;
    private bool isGrounded;
    public PlayerWalkingEvent onPlayerWalk;
    public PlayerSprintEvent onPlayerSprint;
    public UnityEvent onPlayerJump;
    private MovementState movementState;
    private PlayerState playerState;
    public PlayerState PlayerState => playerState;
    PlayerAnimationController playerAnimationController;
    
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) || Physics.CheckSphere(groundCheck.position, groundDistance, obstacleMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            if (playerState == PlayerState.Jumping)
            {
                playerState = PlayerState.Idle;
            }
        }

        if(movementState == MovementState.Mobile && this.gameObject.GetComponent<Player>().HealthState == HealthState.Alive)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
            
            if(move != Vector3.zero)
            {
                
                onPlayerWalk.Invoke(true);
                playerState = (speed == sprintSpeed) ? PlayerState.Sprinting : (speed == crouchSpeed) ? PlayerState.Crouching : PlayerState.Walking;
            }
            else
            {
                onPlayerWalk.Invoke(false);
                playerState = PlayerState.Idle;
            }
            velocity.y += (Physics.gravity.y * 1.5f) * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
        
        CheckJump();
        CheckSprint();
        CheckCrouch();
        CheckPeek();
    }

    private void CheckJump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
            playerState = PlayerState.Jumping;
            onPlayerJump.Invoke();
        }
    }

    private void CheckSprint()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
            if (playerState != PlayerState.Idle)
            {
                playerState = PlayerState.Sprinting;
                onPlayerSprint.Invoke(true);
            }
        }

        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 2f;
            if (playerState == PlayerState.Sprinting)
            {
                playerState = PlayerState.Walking;
                onPlayerSprint.Invoke(false);
            }
        }
    }

    private void CheckCrouch()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            speed = crouchSpeed;
            if (playerState != PlayerState.Idle)
                playerState = PlayerState.Crouching;
        }

        else if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            speed = 2f;
            if (playerState == PlayerState.Crouching)
                playerState = PlayerState.Walking;
        }
    }

    private void Start() 
    {
        movementState = MovementState.Mobile;
        playerState = PlayerState.Idle;
        playerAnimationController = this.gameObject.GetComponent<PlayerAnimationController>();
        if(playerAnimationController != null)
        {
            onPlayerWalk.AddListener(playerAnimationController.PlayWalkAnimation);
            onPlayerSprint.AddListener(playerAnimationController.PlaySprintAnimation);
            onPlayerJump.AddListener(playerAnimationController.PlayJumpAnimation);
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
                transform.rotation = Quaternion.Euler(0, initialYRotation, initialZRotation);
                isPeekingRight = false;
            }
        }
    }

    public void ImmobilizePlayer()
    {
        movementState = MovementState.Immobile;
        playerState = PlayerState.Idle;
    }

    public void MobilizePlayer()
    {
        movementState = MovementState.Mobile;
    }
}

//Jack Sherlock, 9-12-2023
//Script for 1st-person character/camera controls

//libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{

    //player variables
    public float speed = 2.0f;
    public float gravity = -10.0f;
    public float jumpForce = 2.0f;

    private CharacterController characterController;
    private Vector2 moveInput;
    private Vector3 playerVelocity;
    private bool grounded;
    private Vector2 mouseMovement;

    //camera vars
    public Camera cameraLive;
    public float sensitivity = 25.0f;

    private float camXRotation;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        //hide mouse cursor at start
        Cursor.lockState = CursorLockMode.Locked;




    }

    // Update is called once per frame
    void Update()
    {
        grounded = characterController.isGrounded;
        MovePlayer();
        Look();
    }

    public void MovePlayer()
    {
        //directional movement
        Vector3 moveVec = transform.right * moveInput.x + transform.forward * moveInput.y;

        //move the characterController
        characterController.Move(moveVec * speed * Time.deltaTime);

        //add gravity
        playerVelocity.y += gravity * Time.deltaTime;

        if (grounded && playerVelocity.y < 0) //if grounded is true and pV less than 0
        {
            playerVelocity.y = -2.5f; //why did he ask me for this value, i don't know a good value (this is supposed to cap acceleration)
            //this doesn't seem right, bc pV is supposed to be -10 times time so this cap might not make sense here
        }

        characterController.Move(playerVelocity * Time.deltaTime);
    }

    public void Look()
    {
        float xAmt = mouseMovement.x * sensitivity * Time.deltaTime;
        float yAmt = mouseMovement.y * sensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseMovement * sensitivity * Time.deltaTime);

        camXRotation -= xAmt;
        camXRotation = Mathf.Clamp(camXRotation, -90f, 90f);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log("Move Input Val: " + moveInput.ToString()); //YES i can use tostring easily again, godbless



    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseMovement = context.ReadValue<Vector2>();
        Debug.Log("Mouse Movement Val: " + mouseMovement.ToString());


    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Jump();
        Debug.Log("Boing");


    }

    public void Jump()
    {
        if (grounded)
        {
            playerVelocity.y = jumpForce;
        }
    }
}

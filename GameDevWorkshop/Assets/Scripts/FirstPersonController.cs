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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {

    }

    public void OnLook(InputAction.CallbackContext context)
    {

    }

    public void OnJump(InputAction.CallbackContext context)
    {

    }
}

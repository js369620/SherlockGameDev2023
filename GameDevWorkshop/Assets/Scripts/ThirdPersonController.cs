/*
 * Jack Sherlock
 * 9/26/2023
 * Third-person controller based on RigidBody
 * */

//libraries we reference:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //this is what we used in 1st-person controller

public class ThirdPersonController : MonoBehaviour
{

    //var declarations

    //input vars
    private Vector2 moveInput;

    //camera vars
    public Transform camera;

    //player vars
    public Rigidbody rigidbody;
    public Transform player;
    public Transform playerModel;
    public Transform orientation;
    public float moveForce; //force applied to player 
    public float rotationSpeed; //speed of model's rotation
    private Vector3 direction; 



    // Start is called before the first frame update
    void Start()
    {
        //hide mouse cursor at start
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    public void GetMovementInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();


    }

    public void GetJumpInput(InputAction.CallbackContext context)
    {
        
    }

    public void RotatePlayer()
    {
        //what direction to face
        var camPosition = new Vector3(camera.position.x, camera.position.y, camera.position.z);
        Vector3 viewDirection = player.position - camPosition;

        orientation.forward = viewDirection;

        //set direction
        direction = orientation.right * moveInput.x + orientation.forward * moveInput.y;
        direction = direction.normalized;

        //pick up here
    }
}

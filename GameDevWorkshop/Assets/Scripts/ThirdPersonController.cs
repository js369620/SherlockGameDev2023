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
    public float jumpForce = 500f; //UHHHH 

    //raycast vars
    private float ray_length = 1.1f;

    // Start is called before the first frame update
    void Start()
    {
        //hide mouse cursor at start
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update() //called every frame, can be a variable amount
    {
        RotatePlayer();

        Debug.DrawRay(transform.position, Vector3.down * ray_length, Color.blue);


    }

    private void FixedUpdate() //specific frames are assigned?
    {
        MovePlayer();
    }

    public void GetMovementInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log(moveInput);

    }

    public void GetJumpInput(InputAction.CallbackContext context)
    {
        //when button/key is pressed, do it
        if (context.phase == InputActionPhase.Started)
        {
            Jump();
            //"Don't be John Bowditch" - John Bowditch
        }
    }

    public void RotatePlayer()
    {
        //what direction to face
        var camPosition = new Vector3(camera.position.x, player.position.y, camera.position.z);
        Vector3 viewDirection = player.position - camPosition;

        orientation.forward = viewDirection;

        //set direction
        direction = orientation.right * moveInput.x + orientation.forward * moveInput.y;
        direction = direction.normalized;

        //a normal is the direction a UV faces, fun fact

        //keyboard input
        if (moveInput != Vector2.zero)
        {
            //this creates a new rotation that we want the playerModel to look at
            Quaternion new_rotation = Quaternion.LookRotation(direction, Vector3.up);
            
            //the typing of many keys sounds like rain

            //calculated rotation, now we want the playerModel to move towards that rotation
            playerModel.rotation = Quaternion.Slerp(playerModel.rotation, new_rotation, rotationSpeed * Time.deltaTime);
                //Slerp is for rotation, Lerp is for 
                //Slurp is for credit card fraud
        }
    }

    public void MovePlayer()
    {
        rigidbody.AddForce(direction * moveForce, ForceMode.Force);


    }

    public void Jump()
    {
        if (IsOnGround())
        {
            rigidbody.AddForce(Vector3.up * jumpForce);
        }
    }

    bool IsOnGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        return Physics.Raycast(ray, ray_length);
    }
}

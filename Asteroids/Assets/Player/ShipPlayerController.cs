/*
 * Jack Sherlock, ShipPlayerController 10-19-2023
 * script for controlling ship
 * */

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipPlayerController : MonoBehaviour
{
    //input we're givin' it
    private Vector2 moveInput; //looking for key being pressed

    //player vars
    public float moveForce;
    public float rotateSpeed;
    private Rigidbody rigidbody;

    //screenwrap vars
    public Camera cam;
    public Plane[] camFrustum;

    //debug vars
    public GameObject test_cube1, test_cube2, test_cube3, test_cube4;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        //get planes from cam frustum
        camFrustum = GeometryUtility.CalculateFrustumPlanes(cam);
    }

    // Update is called once per frame
    void FixedUpdate() //changed from Update to Fixed, restricted to specific frames
    {
        //print(moveInput);

        RotatePlayer(); 
        MovePlayer();
        ScreenWrap();
    }

    public void GetMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void MovePlayer()
    {
        //think of threshold val as 0-100%, a key down is 100% (cannot be partial)
        if (moveInput.y == 1) //so if a key is down, do the block
        {
            rigidbody.AddForce(transform.forward * moveForce, ForceMode.Force);
        }
        
    }

    private void RotatePlayer() //rotating on our X input
    {
        transform.Rotate(Vector3.up, moveInput.x * rotateSpeed * Time.deltaTime, Space.Self);
    }

    private void ScreenWrap() //creating screen wrap, whoa who woulda guessed
    {
        //wrap vars
        //"The first one is JayZ" - john
        float radius = 2;
        Vector3 bottomPoint = transform.position - new Vector3(0, 0, radius);
        Vector3 topPoint = transform.position + new Vector3(0, 0, radius);
        Vector3 rightPoint = transform.position + new Vector3(radius, 0, 0);
        Vector3 leftPoint = transform.position - new Vector3(radius, 0, 0);
        float distanceToCamera;

        //debug
        test_cube1.transform.position = bottomPoint;
        test_cube2.transform.position = topPoint;
        test_cube3.transform.position = rightPoint;
        test_cube4.transform.position = leftPoint;

        if (!camFrustum[3].GetSide(bottomPoint)) //top plane
        {
            //finding distance the player is from the camera
            distanceToCamera = transform.position.z - cam.transform.position.z;
            //positioning player based on distance
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - (distanceToCamera * 2) + 1f); //we want the INVERSE of the z position here
        }

        if (!camFrustum[2].GetSide(topPoint)) //bottom plane
        {
            //finding distance the player is from the camera
            distanceToCamera = cam.transform.position.z - transform.position.z;
            //positioning player based on distance
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (distanceToCamera * 2) - 1f); //add DistToCam and subtract 1f
        }

        if (!camFrustum[1].GetSide(rightPoint)) //right plane
        {
            //finding distance the player is from the camera
            distanceToCamera = transform.position.x - cam.transform.position.x;
            //positioning player based on distance
            transform.position = new Vector3(transform.position.x - (distanceToCamera * 2) + 1f, transform.position.y, transform.position.z); //we want the INVERSE of the x position here
        }

        if (!camFrustum[0].GetSide(leftPoint)) //left plane
        {
            //finding distance the player is from the camera
            distanceToCamera = cam.transform.position.x - transform.position.x;
            //positioning player based on distance
            transform.position = new Vector3(transform.position.x + (distanceToCamera * 2) - 1f, transform.position.y, transform.position.z); 
        }
    }
}

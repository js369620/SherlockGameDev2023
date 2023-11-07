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

    //shootin' vars (bang bang)
    private bool isShooting;
    public GameObject bulletPrefab;
    public Transform[] shootPoints; //we will be looking for a position which is under transform (originally was a single Transform)
    public float rateOfFire = 0.15f;
    private float shootTimer = 0.0f; //we want to track this internally; NOT a counter
    private int shootPointTracker = 0; //array index


    //debug vars
    //public GameObject test_cube1, test_cube2, test_cube3, test_cube4;

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
        Shoot(isShooting, shootPoints); //requires args because we wrote this with params
        ScreenWrap();
    }

    public void GetMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void GetShootInput(InputAction.CallbackContext context)
    {
        isShooting = true;

        if (context.phase == InputActionPhase.Canceled)
        {
            //if we let up on the key, isShooting equals false
            isShooting = false;

        }
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
        /*
        test_cube1.transform.position = bottomPoint;
        test_cube2.transform.position = topPoint;
        test_cube3.transform.position = rightPoint;
        test_cube4.transform.position = leftPoint;
        */ //why the hell doesn't ctrl / work 

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

    private void Shoot(bool shooting, Transform[] bulletSpawnPoints) //params, yay
    {
        //he started explaining what it does but i can't type for shit on this keyboard

        if (shootTimer > 0) shootTimer -= Time.deltaTime;
        if (shooting && shootTimer <= 0) //tracks keystrokes (along with timer)
        {
            shootTimer = rateOfFire;

            //instantiate a bullet, give it position and rotation
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoints[shootPointTracker].position, Quaternion.identity);

            //bullet.transform.forward = transform.forward;
            //we don't need this after editing the Bullet script
            //take the bullet's rigidbody and add force to it
            bullet.GetComponent<Bullet>().OnShot(transform.forward);
            //"Bullet" used to be "RigidBody", OnShot used to be AddForce

            shootPointTracker++; //auto-increment go brr
            if (shootPointTracker > shootPoints.Length - 1) shootPointTracker = 0; //reset index once it goes beyond the size of the array

            //"we want the bullet to have a certain amount of life and then DIE"
            //Destroy(bullet, 2.5f); //in seconds
                //we don't need this after editing the Bullet script
        }
    }
} //"when in doubt, go nuclear" - John

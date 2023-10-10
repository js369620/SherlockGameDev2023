/*
 * Jack Sherlock, 10-10-2023
 * script for flashlight input
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour
{
    public Light light;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        light.enabled = !light.enabled; //this is simpler than using an if statement
    }
}

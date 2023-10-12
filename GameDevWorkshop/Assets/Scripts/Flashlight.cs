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
    public AudioSource audioSource; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        float r = Random.Range(0.0f, 1.0f);
        float g = Random.Range(0.0f, 1.0f);
        float b = Random.Range(0.0f, 1.0f);
        light.color = new Color(r, g, b);
        */
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            light.enabled = !light.enabled; //this is simpler than using an if statement
            audioSource.pitch = Random.Range(0.5f, 1.5f);
            audioSource.Play();
            /*
            float r = Random.Range(0.0f, 1.0f);
            float g = Random.Range(0.0f, 1.0f);
            float b = Random.Range(0.0f, 1.0f);
            light.color = new Color(r, g, b);
            */
        }
        
    }
}

/*
 * Jack Sherlock, 11-2-2023 Bullet.cs
 * script for bullets and collisions for them
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletLifetime;
    private Rigidbody rigidbody;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //controls what the bullet does when spawned
    public void OnShot(Vector3 direction)
    { 
        rigidbody = GetComponent<Rigidbody>();

        transform.forward = direction;

        rigidbody.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);

        Destroy(gameObject, bulletLifetime);

    }
}

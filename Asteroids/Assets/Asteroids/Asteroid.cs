/*
 * Jack Sherlock, 11/7/2023
 * how asteroids will move, spawn, die
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float life; //"we're gonna define life with a float"
    public float moveSpeed;
    private Rigidbody rb;
    [HideInInspector] public GameObject spawner;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 direction = (Vector3.zero - transform.position).normalized;
        rb.AddForce(direction * moveSpeed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player Bullet")
        {
            Destroy(other.transform.parent.gameObject); //i don't know why but this statement looks stupid
            life--;

            if (life <= 0)
            {
                Destroy(gameObject);
                spawner.GetComponent<Spawner>().asteroidsCount--;
                spawner.GetComponent<Spawner>().CheckAsteroidCount();
            }
        }
    }
}

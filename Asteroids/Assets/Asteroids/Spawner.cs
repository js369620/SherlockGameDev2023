/*
 * Jack Sherlock, 11-9-2023
 * spawn asteroids script
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public Camera cam;
    public Plane[] camFrustum;
    private Vector3 rayDirection = Vector3.forward; //not transform, good guess apparently
    private int asteroidsToSpawn = 3; //start small
    [HideInInspector] public int asteroidsCount = 0; //tracks number of asteroids in the scene
    

    // Start is called before the first frame update
    void Start()
    {
        //get planes from cam frustum (straight outta ShipPlayerController.cs)
        camFrustum = GeometryUtility.CalculateFrustumPlanes(cam);

        CheckAsteroidCount();
        /*
        for(int i = 0; i < asteroidsToSpawn; i++)
        {
            SpawnAsteroid();
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(new Vector3(cam.transform.position.x, 0, cam.transform.position.z), rayDirection * 10000, Color.red);
    }

    private void SpawnAsteroid()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized; //OH GOD THE UNIT CIRCLE
        rayDirection = new Vector3(randomDirection.x, 0, randomDirection.y);
        Ray ray = new Ray(new Vector3(cam.transform.position.x, 0, cam.transform.position.z), rayDirection); //oh yeah, let's just call it all a variation of "ray"

        float enter = 0.0f;
        Vector3 hitPoint = Vector3.zero;
        float shortestLength = 1000;

        //oh fuck here we go again
        for (int i = 0; i <= 3; i++) //initialize, compare, update
        {
            if (camFrustum[i].Raycast(ray, out enter)) //what does this mean
            {
                if (ray.GetPoint(enter).magnitude < shortestLength)
                {
                    hitPoint = ray.GetPoint(enter);
                    shortestLength = ray.GetPoint(enter).magnitude;
                    //remember that raycasts go to infinity
                }
            }
        }

        GameObject asteroid = Instantiate(asteroidPrefab, hitPoint, Quaternion.identity);

        asteroid.GetComponent<Asteroid>().spawner = this.gameObject; //spawner will be red at first (it's missing a var)
        asteroidsCount++;
    }

    public IEnumerator PopulateScene() //this will be red until you return a value
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < asteroidsToSpawn; i++)
        {
            SpawnAsteroid();
        }
    }

    public void CheckAsteroidCount()
    {
        if (asteroidsCount == 0)
        {
            StartCoroutine(PopulateScene());
        }
    }
}

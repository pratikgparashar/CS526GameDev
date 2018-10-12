using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawner_up : MonoBehaviour {

    // Use this for initialization
    public GameObject planes;
    float randX;
    float randY;
    Vector2 whereToSpawn;
    public float spawnRate;
    float nextSpawn = 1f;
    int planeCount;
    static int maxPlaneCount = 10;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn && planeCount < maxPlaneCount)
        {
            nextSpawn = Time.time + spawnRate;
            //randY = Random.Range(-6.5f, 3.0f);
            randY = 4f;
            randX = Random.Range(-10.0f, 10.0f);
            //whereToSpawn = new Vector2(randX, transform.position.y);
            whereToSpawn = new Vector2(randX, randY);
            Instantiate(planes, whereToSpawn, Quaternion.identity);
            planeCount += 1;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(planes);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planeSpawner : MonoBehaviour {

    public GameObject planes;
    float randX;
    float randY;
    Vector2 whereToSpawn;
    public float spawnRate = 10f;
    float nextSpawn = 0.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            randX = 5.0f;
            randY = Random.Range(-4.0f, 5.0f);
            //whereToSpawn = new Vector2(randX, transform.position.y);
            whereToSpawn = new Vector2(randX, randY);
            Instantiate(planes, whereToSpawn, Quaternion.identity);
        }
    }
    void OnBecameInvisible()
    {
        Destroy(planes);
    }
}

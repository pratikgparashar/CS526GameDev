using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawnner : MonoBehaviour {

	public GameObject planes;
    float randX;
    float randY;
    Vector2 whereToSpawn;
    public float spawnRate;
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
            randY = 1.0f;
            randX = Random.Range(-2.0f, 3.0f);
            //whereToSpawn = new Vector2(randX, transform.position.y);
            whereToSpawn = new Vector2(randX, randY);
            Instantiate(planes, whereToSpawn, Quaternion.identity);
        }
    }
}

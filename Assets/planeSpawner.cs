using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PlayerMovement1
{
    public class planeSpawner : MonoBehaviour
    {

        public GameObject planes;
        float randX;
        float randY;
        Vector2 whereToSpawn;
        public float spawnRate;
        float nextSpawn = 1f;
        int planeCount=1;
        static int maxPlaneCount = 10;
        public int direction;


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
                if (direction == 1)
                {

                    randY = -10.5f;
                    randX = Random.Range(-10.0f, 10.0f);
                }
                else if (direction == 2)
                {
                    //Debug.Log("Here");
                    randY = 4f;
                    randX = Random.Range(-10.0f, 10.0f);
                }
                else if (direction == 3)
                {
                    randY = Random.Range(-6.5f, 6.0f);
                    randX = -10.0f;
                }
                else
                {
                    randY = Random.Range(-6.5f, 6.0f);
                    randX = 10.0f;
                }
                //randY = Random.Range(-6.5f, 3.0f);

                //whereToSpawn = new Vector2(randX, transform.position.y);
                whereToSpawn = new Vector2(randX, randY);
                GameObject p = Instantiate(planes, whereToSpawn, Quaternion.identity);
                p.GetComponent<planeScript>().spawnernumber = direction;

                planeCount += 1;
            }
        }

        //void OnBecameInvisible()
        //{
        //    Destroy(planes);

        //}

    }
}
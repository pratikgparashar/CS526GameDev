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
        float nextSpawn = 5f;
        int planeCount=1;
        static int maxPlaneCount = 5;
        public int direction;
		public ATCCenter atc;


        // Use this for initialization
        void Start()
        {
			spawnRate=2;
        }

        // Update is called once per frame
        void Update()
        {
			direction = Random.Range(1,5);
            if (Time.time > nextSpawn && atc.allPlaneCount < maxPlaneCount)
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
                
                whereToSpawn = new Vector2(randX, randY);
                GameObject p = Instantiate(planes, whereToSpawn, Quaternion.identity);
				p.name = ""+direction;
                p.GetComponent<planeScript>().spawnernumber = direction;
				atc.incrPlaneCount();
            }
        }

        //void OnBecameInvisible()
        //{
        //    Destroy(planes);

        //}

    }
}
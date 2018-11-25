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
        public int ColorPlane;
        public Sprite orange;
        public Sprite red;
        public Sprite plane;
        public Sprite water;
        // public SpriteRenderer SpriteRenderer;
		public ATCCenter atc;


        // Use this for initialization
        void Start()
        {
            orange = Resources.Load<Sprite>("orange");
            red = Resources.Load<Sprite>("red");
            water = Resources.Load<Sprite>("water");
            // SpriteRenderer = GetComponent<SpriteRenderer>();
            // if (SpriteRenderer.sprite == null)
            //     SpriteRenderer.sprite = orange;
			spawnRate=2;
        }

        // Update is called once per frame
        void Update()
        {
            ColorPlane = 0;
            direction = Random.Range(1, 5);
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
                if (atc.getActiveScene() == "MultipleRunway")
                {
                    Debug.Log("Here Right now ");
                    ColorPlane = Random.Range(0, 2);

                }

                GameObject p = Instantiate(planes, whereToSpawn, Quaternion.identity);
                if (ColorPlane == 0)
                {   if (atc.getActiveScene() == "FireScene" && atc.getWaterPlaneCount())
                    {
                        p.GetComponent<SpriteRenderer>().sprite = water;
                        p.GetComponent<planeScript>().waterPlane = true;
                        atc.incrWaterPlaneCount();
                    }
                    else
                        p.GetComponent<SpriteRenderer>().sprite = orange;
                }
                else
                    p.GetComponent<SpriteRenderer>().sprite = red;
				p.name = ""+direction;
                p.GetComponent<planeScript>().spawnernumber = direction;
                p.GetComponent<planeScript>().ColorPlane = ColorPlane;
				atc.incrPlaneCount();
            }
        }

        //void OnBecameInvisible()
        //{
        //    Destroy(planes);

        //}

    }
}
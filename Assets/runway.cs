using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PlayerMovement1
{
    public class runway : MonoBehaviour
    {
        private Rigidbody2D rb;
        Vector2 temp;
        bool touchedRunway = false;
        GameObject gameObj;
        Vector3 originalScale, destinationScale;
        public int runwayColor;
        // Use this for initialization
        void Start()
        {
               
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if(isAllowedOrNot(col)){
                landOrNot(col);
            }
            else{
                Debug.Log("PLUYYYYYHHFFHFBH");
                gameObj = col.gameObject;
                Physics2D.IgnoreCollision(col.gameObject.GetComponent<PolygonCollider2D>(), this.GetComponent<BoxCollider2D>());
                StartCoroutine(wait());
                touchedRunway = true;
            }

        }
        public bool isAllowedOrNot(Collision2D col){

            return runwayColor == col.gameObject.GetComponent<planeScript>().ColorPlane;
        }
        void landOrNot(Collision2D col){

            Collider2D collide = col.collider;
            Vector3 contactPoint = col.contacts[0].point;
            Vector3 center = collide.bounds.center;
            bool right = contactPoint.x < center.x;
            bool top = contactPoint.y > center.y;
            if (right)
            {
                rb = col.gameObject.GetComponent<Rigidbody2D>();
                Destroy(rb);
                float currentTime = 0.0f;
                do
                {
                    originalScale = col.gameObject.transform.localScale;
                    if (originalScale.x < 0 || originalScale.y < 0)
                        break;
                    destinationScale = new Vector3(originalScale.x - 0.0001f, originalScale.y - 0.0001f, 0.1f);
                    col.gameObject.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / 1);
                    currentTime += Time.deltaTime;
                } while (currentTime <= 3);


                ArrayList PathNode = col.gameObject.GetComponent<PathFollower>().PathNode;
                Node n = col.gameObject.GetComponent<PlayerMovement>().InitialNode;
                Node newn = Instantiate(n, this.GetComponent<Renderer>().bounds.center, n.transform.rotation);
                PathNode.Add(newn);
                gameObj = col.gameObject;
                gameObj.GetComponent<PathFollower>().settouchRunway(true);
            }

        }
        void waitTime()
        {
            StartCoroutine(wait());
        }
        IEnumerator wait(){
            yield return new WaitForSecondsRealtime(2);
            Physics2D.IgnoreCollision(gameObj.GetComponent<PolygonCollider2D>(), this.GetComponent<BoxCollider2D>(), false);
        }
        public bool gettouch()
        {
            return touchedRunway;
        }
        public Vector3 getRunwayMidpoint()
        {
            return this.GetComponent<Renderer>().bounds.center;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PlayerMovement1{
public class planeScript : MonoBehaviour {


    private Rigidbody2D rb;
    public GameObject pl;
    public int spawnernumber;
    public int ColorPlane;
        public bool waterPlane;
    float randX = 0.0f;
    float randY = 0.0f;
    Vector3 directionF = new Vector3(0.02f,0.05f,0);
    public bool moveSingle = true;
    Vector3 location;
	public bool touchedRunway= false;
    Vector3 runwayMidpoint;

        // Use this for initialization
    void Start()
    {
        waterPlane = false;
        rb = GetComponent<Rigidbody2D>();
        randX = Random.Range(-0.04f, 0.0f);
        randY = Random.Range(-0.04f, 0.02f);
        location = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height/2, Camera.main.nearClipPlane));
     }

    // Update is called once per frame
    void Update()
    {

            if (transform.name != "OriginalPlayerShip")
            {
				if(touchedRunway && this.GetComponent<PathFollower>().PathNode.Count==0)
				{
					Destroy(this.gameObject);
				}
                if (moveSingle && !transform.GetComponent<PlayerMovement>().getmoveOrNot())
                {

                    if (pl.GetComponent<planeScript>().spawnernumber == 1)
                    {
                        location.y = 10f;
                    }
                    else if (pl.GetComponent<planeScript>().spawnernumber == 3)
                    {
                        location.x = 12f;
                    }
                    else if (pl.GetComponent<planeScript>().spawnernumber == 2)
                    {
                        location.y = -10f;
                    }
                    else
                    {
                        location.x = -12f;
                    }
                    FaceMoveDirection(location);
                    pl.transform.position = Vector3.MoveTowards(pl.transform.position, location, Time.deltaTime * 1f);
                }
            }
        }

    void OnCollisionEnter2D(Collision2D col)
    {
            if (col.gameObject.name != "runway" && col.gameObject.name != "OriginalPlayerShip" && col.gameObject.name!="runway1"){
			col.gameObject.GetComponent<PathFollower>().destroyNode();
			Destroy(col.gameObject);
		}
		
    }

	public void settouchRunway(bool toogle)
    {
         touchedRunway = toogle;
    }

    public void setMoveSingle(bool toogle){
        moveSingle = toogle;
    }

    public void setDirection(Vector3 direction){
        moveSingle = true;
        directionF = direction;
    }

	public void FaceMoveDirection(Vector3 CurrentPositionHolder)
	{
		Vector3 diff = CurrentPositionHolder - pl.transform.position;
		diff.Normalize();
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		pl.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
	}
    public bool isWaterPlane()
    {
        return waterPlane;
    }
    }
}
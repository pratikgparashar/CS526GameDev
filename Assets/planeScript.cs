using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PlayerMovement1{
public class planeScript : MonoBehaviour {
    private Rigidbody2D rb;
    public GameObject pl;
    float randX = 0.0f;
    float randY = 0.0f;
    Vector3 directionF = new Vector3(0.02f,0.05f,0);
    bool moveSingle = true;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        randX = Random.Range(-0.04f, 0.0f);
        randY = Random.Range(-0.04f, 0.02f);

    }

    // Update is called once per frame
    void Update()
    {
		//Debug.Log("PLANE SCRIPT INITI");
		//pl.GetComponent<PathFollower>().destroyNode();
        if (moveSingle){
                //FaceMoveDirection(new Vector3(directionF.x*0.001f,0,directionF.y*0.002f));
                //pl.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
                Vector3 a = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
                FaceMoveDirection(a);

                pl.transform.position = Vector3.MoveTowards(pl.transform.position, a, Time.deltaTime * 1f);

                //pl.transform.Translate(a);
                //pl.transform.position = pl.transform.TransformDirection(directionF);
                //transform.Translate()
                //transform.Translate()

                //Debug.Log("PLANE SCRIPT" + pl.transform.forward + " - "+  Time.deltaTime * 5);
                //transform.Translate(pl.transform.forward);
                //transform.position += Vector3.one * Time.deltaTime;
                //rb.AddRelativeForce(Vector3.up * 5f );
            }
        }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name != "runway"){
			col.gameObject.GetComponent<PathFollower>().destroyNode();
			Destroy(col.gameObject);
		}
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
}
}

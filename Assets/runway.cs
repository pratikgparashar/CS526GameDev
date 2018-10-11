using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PlayerMovement1{
public class runway : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("land hua hai");
        Debug.Log("collision name = " + col.gameObject.name);
		col.gameObject.GetComponent<PathFollower>().destroyNode();
        Destroy(col.gameObject);
        print("hit left");
       
    }
}
}

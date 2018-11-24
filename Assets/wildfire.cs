using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PlayerMovement1
{
public class wildfire : MonoBehaviour {
	float[,] positions = new float[,] {{2.13f, -2.06f},{-2.42f, -2.06f},{1.01f, -4.09f},{-0.42f, 1.83f}};
	float fireSize = 1.0f;
	public GameObject atcGob;
	ATCCenter atc;
	// Use this for initialization
	void Start () {
		if(transform.name != "OriginalWildfire"){
			int num = Random.Range(0,4);
			transform.position = new Vector3(positions[num,0],positions[num,1],0.0f);
			atc = atcGob.GetComponent<ATCCenter>();
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	private void OnTriggerEnter2D(Collider2D other)
    {
            if(!other.gameObject.GetComponent<planeScript>().isWaterPlane()){
    		Destroy(other.gameObject);
			Application.LoadLevel("GameOverScene");	
    	}

    	if(transform.name != "OriginalWildfire"){
			fireSize = transform.localScale.x/2.0f;
			transform.localScale = new Vector3(fireSize, fireSize, fireSize);
			if(fireSize < 0.075f){
				Destroy(gameObject);
				atc.descrfireCount();
			}
		}
    }
}
}
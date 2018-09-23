using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PlayerMovement1;
namespace PlayerMovement1{

	public class PlayerMovement : MonoBehaviour {
		public GameObject atcGob;
		public GameObject Path;
        public Node InitialNode;
		public PathFollower pF;
		public ATCCenter atc;
		


		//Vector3 worldCoordinates;
		float maxSpeed = 5f;
		bool moveOrNot = false;
		bool startPath = false;
		Vector3 prevCoord;
		
		// Use this for initialization
		void Start () {
            pF = Path.GetComponent<PathFollower>();
			atc = atcGob.GetComponent<ATCCenter>();
            //pF = GetComponentInChildren<PathFollower>();
			Debug.Log(transform.name + "" + pF.name + "" + atc.name);
		}
	
		// Update is called once per frame
		void Update () {
			foreach(Touch touch in Input.touches)
			{
				if (touch.phase == TouchPhase.Began)
				{
					//Debug.Log("Touch: " + touch.position);
					Vector3 worldCoordinates = Camera.main.ScreenToWorldPoint(touch.position);
					worldCoordinates.z = transform.position.z;
					RaycastHit2D hit = Physics2D.Raycast(worldCoordinates, Vector2.zero);
					//Debug.Log("HITTTTIn" + hit.collider.gameObject.name );
					if (hit.collider != null && hit.collider.gameObject.name == transform.name) {
						atc.setActivePlane(transform.name);
						startPath = true;
						prevCoord = worldCoordinates;
                        this.GetComponent<planeScript>().setMoveSingle(false);
						if(pF.shouldReinitialize())
							pF.reInitializePath(worldCoordinates,InitialNode);
					}
					//pF.addNodeSInPathNode(Instantiate(InitialNode, new Vector3(worldCoordinates.x,worldCoordinates.y), InitialNode.transform.rotation));
					
				}
				if (touch.phase == TouchPhase.Moved && startPath && atc.getActivePlane() == transform.name) 
				{
					Vector3 worldCoordinates = Camera.main.ScreenToWorldPoint(touch.position);
					worldCoordinates.z = transform.position.z;
					if (Math.Sqrt(Math.Pow(worldCoordinates.x - prevCoord.x,2)+Math.Pow(worldCoordinates.y - prevCoord.y,2))>0.005)
					{
						Debug.Log("NEW INIR" + InitialNode.GetHashCode());
						pF.addNodeSInPathNode(Instantiate(InitialNode, new Vector3(worldCoordinates.x,worldCoordinates.y), InitialNode.transform.rotation));
						prevCoord = worldCoordinates;
					}
				}
				if (touch.phase == TouchPhase.Ended && atc.getActivePlane() == transform.name) 
				{	
					atc.setActivePlane("");
					moveOrNot = true;
					startPath = false;
				}
			}
		}

		/*public void FaceMoveDirection()
		{
			Vector3 diff = new Vector3(worldCoordinates.x,worldCoordinates.y) - transform.position;
			diff.Normalize();
 
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			//Debug.Log("Angle : " + rot_z);
			transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
		}*/

		public bool getmoveOrNot(){
			return moveOrNot;
		}

		public void setmoveOrNot(){
			//Debug.Log("Angle : " );
			moveOrNot = false;
			pF.startInfinite();
		}

		public void	setStartPath(){
			startPath = false;
		}
	}
}
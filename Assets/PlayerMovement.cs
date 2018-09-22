using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PlayerMovement1{

	public class PlayerMovement : MonoBehaviour {
		public GameObject Path;
		public Node InitialNode;
		Vector3 worldCoordinates;
		float maxSpeed = 5f;
		static bool moveOrNot = false;
		bool startPath = false;
		public PathFollower pF;
		Vector3 prevCoord;
		// Use this for initialization
		void Start () {
			pF = Path.GetComponent<PathFollower>();
		}
	
		// Update is called once per frame
		void Update () {
			foreach(Touch touch in Input.touches)
			{
				if (touch.phase == TouchPhase.Began)
				{
					//Debug.Log("Touch: " + touch.position);
					worldCoordinates = Camera.main.ScreenToWorldPoint(touch.position);
					worldCoordinates.z = transform.position.z;
					RaycastHit2D hit = Physics2D.Raycast(worldCoordinates, Vector2.zero);
					if (hit.collider != null  && hit.collider.gameObject.name == "PlayerShip") {
						Debug.Log("OBJECT " );
						startPath = true;
						prevCoord = worldCoordinates;
						if(pF.shouldReinitialize())
							pF.reInitializePath(worldCoordinates,InitialNode);
                //hit.collider.attachedRigidbody.AddForce(Vector2.up);
					}
					//pF.addNodeSInPathNode(Instantiate(InitialNode, new Vector3(worldCoordinates.x,worldCoordinates.y), InitialNode.transform.rotation));
					
				}
				if (touch.phase == TouchPhase.Moved && startPath) 
				{
					worldCoordinates = Camera.main.ScreenToWorldPoint(touch.position);
					worldCoordinates.z = transform.position.z;
					if (Math.Sqrt(Math.Pow(worldCoordinates.x - prevCoord.x,2)+Math.Pow(worldCoordinates.y - prevCoord.y,2))>0.005)
					{
						//Debug.Log("Touch: " + touch.position);
						//Debug.Log("Transform: " + transform.position);
						//Debug.Log("WorldCoordinates: " + worldCoordinates);
						//transform.Translate(worldCoordinates);
						//Vector3 relative = transform.InverseTransformPoint(new Vector3(worldCoordinates.x,worldCoordinates.y));
						//float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
						//
						//transform.Rotate(0, 0, 5.0f);
						//FaceMoveDirection();
						//transform.position = new Vector3(worldCoordinates.x,worldCoordinates.y);
						//Debug.Log(touch.position);
						//transform.Translate(touch.position.x,touch.position.y);
						//Debug.Log("Transform 2: " + transform.position);




						pF.addNodeSInPathNode(Instantiate(InitialNode, new Vector3(worldCoordinates.x,worldCoordinates.y), InitialNode.transform.rotation));
						prevCoord = worldCoordinates;
					}
				}
				if (touch.phase == TouchPhase.Ended) 
				{
					moveOrNot = true;
				}
			}
		}

		public void FaceMoveDirection()
		{
			Vector3 diff = new Vector3(worldCoordinates.x,worldCoordinates.y) - transform.position;
			diff.Normalize();
 
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			//Debug.Log("Angle : " + rot_z);
			transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
		}

		public bool getmoveOrNot(){
			return moveOrNot;
		}

		public void setmoveOrNot(){
			moveOrNot = false;
		}

		public void	setStartPath(){
			startPath = false;
		}
	}
}
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
		public bool moveOrNot = false;
		bool startPath = false;
		Vector3 prevCoord;
		Vector3 firstCoord;
		
		
		// Use this for initialization
		void Start () {
			highLightStop();
			String a = DateTime.Now.ToString("h:mm:ss tt");
			if(transform.name != "OriginalPlayerShip")
				transform.name  = "Plane"+a+transform.name;
			
			//transform.scale.x = scales[sc];
			//transform.scale.y = scales[sc];
			Path.transform.name = "Path"+a+transform.name;
			pF = gameObject.GetComponent<PathFollower>();
			
			atc = atcGob.GetComponent<ATCCenter>();
            //pF = GetComponentInChildren<PathFollower>();
			Debug.Log(transform.name + "" + pF.name + "" + atc.name);
		}
	
		// Update is called once per frame
		void Update () {

			


			if(transform.name != "OriginalPlayerShip"){
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
							highLightStart();
							atc.setActivePlane(transform.name);
							firstCoord = new Vector3(0,0,0);
							startPath = true;
							prevCoord = worldCoordinates;
							//moveOrNot = true;
							//pF.setMoveSingle(false);
							if(pF.shouldReinitialize())
								pF.reInitializePath(transform.position,InitialNode);
						}
						//pF.addNodeSInPathNode(Instantiate(InitialNode, new Vector3(worldCoordinates.x,worldCoordinates.y), InitialNode.transform.rotation));
					
					}
					else if (touch.phase == TouchPhase.Moved && startPath && atc.getActivePlane() == transform.name) 
					{
						//worldCoordinates = proposed location for dot.
						Vector3 worldCoordinates = Camera.main.ScreenToWorldPoint(touch.position);
						worldCoordinates.z = transform.position.z;

						if (Math.Sqrt(Math.Pow(worldCoordinates.x - prevCoord.x,2)+Math.Pow(worldCoordinates.y - prevCoord.y,2))>0.005)
						{
							Node n = Instantiate(InitialNode, new Vector3(worldCoordinates.x,worldCoordinates.y),InitialNode.transform.rotation);
							n.name = "Node"+transform.name;
							if(firstCoord == new Vector3(0,0,0)){
								firstCoord = worldCoordinates;
								pF.setCurrentPositionHolder(firstCoord);
							}
							pF.addNodeSInPathNode(n);
							prevCoord = worldCoordinates;
						}
					}
					else if (touch.phase == TouchPhase.Ended && atc.getActivePlane() == transform.name) 
					{	
						highLightStop();
						atc.setActivePlane("");
						moveOrNot = true;
						startPath = false;
						//pF.setCurrentPositionHolder(prevCoord);
					}
				}
			}
		}


		public bool getmoveOrNot(){
			return moveOrNot;
		}

		public void setmoveOrNot(){
			moveOrNot = false;
			pF.startInfinite();
		}

		public void	setStartPath(){
			startPath = false;
		}

		public void highLightStart(){
			transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
		}
		
		public void highLightStop(){
			transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
		}

	}
}
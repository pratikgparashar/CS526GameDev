using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerMovement1;
namespace PlayerMovement1{
public class PathFollower : MonoBehaviour {
	public  GameObject Player;
	public Node InitialNode2;
	//Node[] PathNode;
	ArrayList PathNode = new ArrayList();
	public float MoveSpeed;
	float Timer;
	int CurrentNode;
	static Vector3 CurrentPositionHolder;

	PlayerMovement plMove;

	// Use this for initialization
	void Start () {
		//PathNode = GetComponentsInChildren<Node>();
		PathNode.Add(InitialNode2);
		plMove = Player.GetComponent<PlayerMovement>();
		CheckNode();
	}
	
	void CheckNode(){
		if(CurrentNode <= PathNode.Count-1){
			Timer = 0;
			CurrentPositionHolder = ((Node)PathNode[CurrentNode]).transform.position;
		}else{
			plMove.setmoveOrNot();
		
		}
		
	}
	// Update is called once per frame
	void Update () {
		//Debug.Log(CurrentNode);
		Timer += Time.deltaTime * MoveSpeed;
		if(plMove.getmoveOrNot()){
			if(Player.transform.position != CurrentPositionHolder){
				FaceMoveDirection(CurrentPositionHolder);
				Player.transform.position = Vector3.Lerp(Player.transform.position,CurrentPositionHolder,Timer);
				if(CurrentNode - 1 > 0){
					((Node)PathNode[CurrentNode-1]).GetComponent<Node>().DestroyGameObject();
					PathNode.RemoveAt(CurrentNode-1);
					CurrentNode--;
					
				}
				
			}else{
				if(CurrentNode < PathNode.Count-1){
					CurrentNode++;
					CheckNode();
				}
		
			}
		}
	}

	public void addNodeSInPathNode(Node newNode){
		//Debug.Log((Node)newNode);
		PathNode.Add(newNode);
	}

	public void FaceMoveDirection(Vector3 CurrentPositionHolder)
	{
		Vector3 diff = CurrentPositionHolder - Player.transform.position;
		diff.Normalize();
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		//Debug.Log("Angle : " + rot_z);
		Player.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
	}

	public void reInitializePath(Vector3 worldCoordinates, Node InitialNode){
		//PathNode.Clear();
		
		for(int i = PathNode.Count-1;i>0 ;i--){
			((Node)PathNode[i]).GetComponent<Node>().DestroyGameObject();
			PathNode.RemoveAt(i);
		}
		CurrentNode =0;
		addNodeSInPathNode(Instantiate(InitialNode, new Vector3(worldCoordinates.x,worldCoordinates.y), InitialNode.transform.rotation));
	}

	public bool shouldReinitialize(){
		if(PathNode.Count > 1)
			return true;
		else
			return false;
	}
}
}
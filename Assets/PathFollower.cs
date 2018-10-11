using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerMovement1;
namespace PlayerMovement1{
public class PathFollower : MonoBehaviour {
	public  GameObject Player;
	public Node InitialNode2;
	static float[] speeds = {0.5f, 0.8f, 1f, 2f};
	public ArrayList PathNode;
	public float MoveSpeed;
	float Timer;
	int CurrentNode = 2;
	Vector3 CurrentPositionHolder;
	bool moveInfinitely = false;
	Vector3 infiniteDirection ;
	PlayerMovement plMove;

	// Use this for initialization
	void Start () {
		plMove = Player.GetComponent<PlayerMovement>();
		PathNode = new ArrayList();
		CurrentPositionHolder = Player.transform.position;
		MoveSpeed =  speeds[Random.Range(0, 4)];
	}
	
	void CheckNode(){
		if(CurrentNode <= PathNode.Count-1){
			Timer = 0;
			CurrentPositionHolder = ((Node)PathNode[CurrentNode]).transform.position;
		}else{
			plMove.setmoveOrNot();
			Player.GetComponent<planeScript>().setDirection(CurrentPositionHolder);
		}
		
	}
	// Update is called once per frame
	void Update () {

		if(plMove.getmoveOrNot()){
			if(Player.transform.position != CurrentPositionHolder){
				FaceMoveDirection(CurrentPositionHolder);
				Player.transform.position = Vector3.MoveTowards(Player.transform.position,CurrentPositionHolder,Time.deltaTime * MoveSpeed);
				if(CurrentNode - 1 >= 0){
					((Node)PathNode[CurrentNode-1]).GetComponent<Node>().DestroyGameObject();
					PathNode.RemoveAt(CurrentNode-1);
					CurrentNode--;
				}
				infiniteDirection = (Player.transform.position - CurrentPositionHolder).normalized;
			}else{
				if(CurrentNode < PathNode.Count){
					CurrentNode++;
					CheckNode();
				}
			}
		}
	}


    public void addNodeSInPathNode(Node newNode){
    	if(moveInfinitely)
    		moveInfinitely = false;
    	PathNode.Add(newNode);
    }

	public void FaceMoveDirection(Vector3 CurrentPositionHolder)
	{
		Vector3 diff = CurrentPositionHolder - Player.transform.position;
		diff.Normalize();
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		Player.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
	}

	public void reInitializePath(Vector3 worldCoordinates, Node InitialNode){
		destroyNode();
		CurrentNode =0;
		addNodeSInPathNode(Instantiate(InitialNode, new Vector3(worldCoordinates.x,worldCoordinates.y), InitialNode.transform.rotation));
	}

	public bool shouldReinitialize(){
		return (PathNode.Count > 1);
	}

	public void startInfinite(){
		destroyNode();
		moveInfinitely = true;
	}

	public void setCurrentPositionHolder(Vector3 direction){
        CurrentPositionHolder = direction;
    }

	public void destroyNode(){
		for (int i = PathNode.Count-1;i>=0 ;i--){
			((Node)PathNode[i]).GetComponent<Node>().DestroyGameObject();
			PathNode.RemoveAt(i);
		}
	}
}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerMovement1;
namespace PlayerMovement1{
public class PathFollower : MonoBehaviour {
	public  GameObject Player;
	public Node InitialNode2;
	//Node[] PathNode;
	ArrayList PathNode;
	public float MoveSpeed;
	float Timer;
	int CurrentNode = 2;
	Vector3 CurrentPositionHolder;
	bool moveInfinitely = false;
	Vector3 infiniteDirection ;

	PlayerMovement plMove;

	// Use this for initialization
	void Start () {
		//PathNode = GetComponentsInChildren<Node>();
		//PathNode.Add(InitialNode2);
		plMove = Player.GetComponent<PlayerMovement>();
		PathNode = new ArrayList();
		CurrentPositionHolder = Player.transform.position;
		//CheckNode();
		Debug.Log(PathNode.GetHashCode() + Player.name);
	}
	
	void CheckNode(){
		if(CurrentNode <= PathNode.Count-1){
			Timer = 0;
			CurrentPositionHolder = ((Node)PathNode[CurrentNode]).transform.position;
			Debug.Log(CurrentPositionHolder.x + "/" + CurrentPositionHolder.y + "----" + PathNode.GetHashCode());
		}else{
			plMove.setmoveOrNot();
			//Debug.Log("CA:::::: SET");
			Player.GetComponent<planeScript>().setDirection(CurrentPositionHolder);

		}
		
	}
	// Update is called once per frame
	void Update () {
		//Debug.Log(CurrentNode);
		Timer += Time.deltaTime * MoveSpeed;
		if(false && moveInfinitely){
			Debug.Log("MOVEE-INFINTE");
                //transform.Translate(CurrentPositionHolder);
                //Player.transform.position += Player.transform.forward * Time.deltaTime * 1f;
                //Player.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector3(Player.transform.position.x,0,Player.transform.position.y));
            }
		if(plMove.getmoveOrNot()){
			if(Player.transform.position != CurrentPositionHolder){
				Debug.Log(Player.transform.position + " IS MOVING FROM " + PathNode.GetHashCode());
				FaceMoveDirection(CurrentPositionHolder);
				Player.transform.position = Vector3.MoveTowards(Player.transform.position,CurrentPositionHolder,Time.deltaTime * 1f);
				if(CurrentNode - 1 >= 0){
					((Node)PathNode[CurrentNode-1]).GetComponent<Node>().DestroyGameObject();
					PathNode.RemoveAt(CurrentNode-1);
					CurrentNode--;
					
				}
				infiniteDirection = (Player.transform.position - CurrentPositionHolder).normalized;
				
			}else{
				if(CurrentNode < PathNode.Count){
					//Debug.Log("BARABrt"+CurrentNode);
					CurrentNode++;
					CheckNode();
				}
		
			}
		}
	}


    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("land hua hai");
        //Debug.Log("collision name = " + col.gameObject.name);



       
            for (int i = 1; i < PathNode.Count;i++){
                ((Node)PathNode[i - 1]).GetComponent<Node>().DestroyGameObject();
                PathNode.RemoveAt(i - 1);
            }
            if(col.gameObject.name!="runway")
                Destroy(col.gameObject);

        }
    public void addNodeSInPathNode(Node newNode){
    	//Debug.Log((Node)newNode);
    	if(moveInfinitely)
    		moveInfinitely = false;
    	Debug.Log("ADD NODE " + PathNode.GetHashCode() + Player.name);
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
		Debug.Log("INTIAL - NOD" + InitialNode.GetHashCode());
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

	public void startInfinite(){

		moveInfinitely = true;
	}
}
}
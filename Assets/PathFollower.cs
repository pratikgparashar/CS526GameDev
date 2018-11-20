using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerMovement1;
namespace PlayerMovement1{
public class PathFollower : MonoBehaviour {
	public  GameObject Player;
	public Node InitialNode2;
	static float[] speeds = {0.5f, 0.8f, 1f, 2f};
	//path data store
	public ArrayList PathNode;
	public float MoveSpeed;
	float Timer;
	int CurrentNode = 0;
	public Vector3 CurrentPositionHolder;
	bool moveInfinitely = false;
	Vector3 infiniteDirection ;
	PlayerMovement plMove;
	static float[] scales = {0.05f, 0.03f, 0.025f, 0.04f};

	//PlaneSc
	private Rigidbody2D rb;
    public int spawnernumber;
    float randX = 0.0f;
    float randY = 0.0f;
    Vector3 directionF = new Vector3(0.02f,0.05f,0);
    public bool moveSingle = true;
    Vector3 location;
	public bool touchedRunway= false;
    Vector3 runwayMidpoint;

	public Vector3 prevCommand;


	// Use this for initialization
	void Start () {
		plMove = Player.GetComponent<PlayerMovement>();
		PathNode = new ArrayList();
		CurrentPositionHolder = Player.transform.position;
		MoveSpeed =  speeds[Random.Range(0, 4)];
		int sc = Random.Range(0,4);	
		Player.gameObject.transform.localScale = new Vector3(scales[sc],scales[sc],0);
		//PlaneSc
		rb = GetComponent<Rigidbody2D>();
		location = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height/2, Camera.main.nearClipPlane));
	}
	
	void CheckNode(){
		if(CurrentNode <= PathNode.Count-1){
			CurrentPositionHolder = ((Node)PathNode[CurrentNode]).transform.position;
		}else{
			plMove.setmoveOrNot();
			setDirection(CurrentPositionHolder);
		}
		
	}
	// Update is called once per frame
	void Update () {
		if(transform.name != "OriginalPlayerShip"){ 
			
			if(touchedRunway && this.GetComponent<PathFollower>().PathNode.Count==0){
				Destroy(this.gameObject);
				plMove.atc.descrPlaneCount();
				ScoreScript.scoreValue+=1;
			}

			if(PathNode.Count > 0){
				if(Player.transform.position.x != prevCommand.x || Player.transform.position.y != prevCommand.y){
					FaceMoveDirection(CurrentPositionHolder);
					Player.transform.position = Vector3.MoveTowards(Player.transform.position,CurrentPositionHolder,Time.deltaTime * MoveSpeed);
					infiniteDirection = (Player.transform.position - CurrentPositionHolder).normalized;
				}else if(Player.transform.position.x == prevCommand.x && Player.transform.position.y == prevCommand.y){
					((Node)PathNode[CurrentNode]).GetComponent<Node>().DestroyGameObject();
					PathNode.RemoveAt(CurrentNode);
					if(PathNode.Count >= 1){
						CurrentPositionHolder = ((Node)PathNode[CurrentNode]).transform.position;
						prevCommand = CurrentPositionHolder;
					}
				}
			}//PlaneSc
			else{
				if (Player.GetComponent<planeScript>().spawnernumber == 1){
					location.y = 10f;
				}
				else if (Player.GetComponent<planeScript>().spawnernumber == 3){
					location.x = 12f;
				}
				else if (Player.GetComponent<planeScript>().spawnernumber == 2){
					location.y = -10f;
				}
				else{
					location.x = -12f;
				}
				FaceMoveDirection(location);
				Player.transform.position = Vector3.MoveTowards(Player.transform.position, location, Time.deltaTime * MoveSpeed/2);
			}
		}
	}


    public void addNodeSInPathNode(Node newNode){
    	if(moveInfinitely)
    		moveInfinitely = false;
    	PathNode.Add(newNode);
		prevCommand =  ((Node)PathNode[0]).transform.position;
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
		CurrentPositionHolder = worldCoordinates;
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

	//PlaneSc
	void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name != "runway" && col.gameObject.name != "OriginalPlayerShip"){

        	if(ScoreScript.scoreValue > PlayerPrefs.GetInt("HighScore"))
			{
				PlayerPrefs.SetInt("HighScore", ScoreScript.scoreValue);
			}
			col.gameObject.GetComponent<PathFollower>().destroyNode();
			plMove.atc.descrPlaneCount();
			//Game End
			Destroy(col.gameObject);
			Application.LoadLevel("GameOverScene");
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
}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using PlayerMovement1;
namespace PlayerMovement1{
public class ATCCenter : MonoBehaviour {
	
	String activePlane;
	public GameObject fire;
	public int allPlaneCount;
	public int allfireCount;
    public int allWaterPlaneCount;
    public String activeScene;
	// Use this for initialization
	void Start () {
		allPlaneCount = 0;
        allWaterPlaneCount = 0;
        Scene scene = SceneManager.GetActiveScene();
		activeScene = scene.name;
        Debug.Log("Active scene is '" + scene.name + "'.");
	}
	
	// Update is called once per frame
	void Update () {
		if(activeScene == "FireScene"){
			if(allfireCount == 0){
				incrfireCount();
				Instantiate(fire, new Vector2(0.0f,0.0f), Quaternion.identity);
			}
		}
	}

	public void setActivePlane(String s){
			//Debug.Log(activePlane + " ----" + s);
			activePlane = s;
	}

	public String getActivePlane(){
		return activePlane;
	}

	public void incrPlaneCount(){
		allPlaneCount += 1;
	}

	public void descrPlaneCount(){
		allPlaneCount -= 1;
	}

	public void incrfireCount(){
		allfireCount += 1;
	}

	public void descrfireCount(){
		allfireCount -= 1;
	}

	public String getActiveScene(){
		return activeScene;
	}
    public void incrWaterPlaneCount()
    {
            allWaterPlaneCount += 1;
    }

    public void descWaterPlaneCount()
    {
            allWaterPlaneCount -= 1;   
    }
    public bool getWaterPlaneCount()
    {
        return allWaterPlaneCount==0;
    }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PlayerMovement1;
namespace PlayerMovement1{
public class ATCCenter : MonoBehaviour {
	
	String activePlane;
	public int allPlaneCount;
	// Use this for initialization
	void Start () {
		allPlaneCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setActivePlane(String s){
			Debug.Log(activePlane + " ----" + s);
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
}
}
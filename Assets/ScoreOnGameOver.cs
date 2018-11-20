using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreOnGameOver : MonoBehaviour {

	// Use this for initialization
	Text scoreGameOver;
	void Start () {
		scoreGameOver  = GetComponent<Text> ();
		scoreGameOver.text = "Score: "+ ScoreScript.scoreValue;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
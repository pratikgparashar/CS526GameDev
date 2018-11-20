using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreScript : MonoBehaviour {


	public static int highScoreValue = 0;
	Text score;

	// Use this for initialization
	void Start () {
		highScoreValue = PlayerPrefs.GetInt("HighScore");
		score = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		score.text = "High Score : "+ highScoreValue;
	}
}

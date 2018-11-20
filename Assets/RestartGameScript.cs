using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGameScript : MonoBehaviour {


public void restartGameScript()
{
	SceneManager.LoadScene("SampleScene");
	//Debug.Log("LOADED THE SCENE =>>>>>>>> SampleScene");
}
	
}

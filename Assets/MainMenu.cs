using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour {

    public void Level1(){
        SceneManager.LoadScene("SampleScene");
    }

    public void Level2()
    {
        SceneManager.LoadScene("FireScene");
    }

    public void Level3()
    {
        SceneManager.LoadScene("FuelScene");
    }

    public void Level4()
    {
        SceneManager.LoadScene("MultipleRunway");
    }
}

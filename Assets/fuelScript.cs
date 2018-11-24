using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuelScript : MonoBehaviour {

	public float max_fuel = 100f;
	public float cur_fuel = 0f;
	public GameObject fuelBar; 


	// Use this for initialization
	void Start () {
		
		cur_fuel = max_fuel;
		InvokeRepeating("decreaseFuel",1f,1f);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void decreaseFuel()
	{
		// cur_fuel -= Time.deltaTime;
		cur_fuel -= 2f;
		float calc_fuel = cur_fuel/max_fuel;
		setFuelBar(calc_fuel);
	}

	public float getCurrentFuel()
	{
		return cur_fuel;
	}


	public void setFuelBar(float myFuel)
	{
		fuelBar.transform.localScale = new Vector3(fuelBar.transform.localScale.x,myFuel,fuelBar.transform.localScale.z); 
	} 

}

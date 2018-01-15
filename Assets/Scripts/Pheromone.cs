using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pheromone : MonoBehaviour 
{

	public float concentration;
	//species
	public float evaporationRate;
	public Transform pheromone;

	// Use this for initialization
	void Start () 
	{
		Instantiate (pheromone, new Vector3 (transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () 
	{
		Destroy (gameObject, 5f);
	}
}

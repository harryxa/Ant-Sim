using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AntClass : MonoBehaviour 
{
	//basic properties of an ant

	public Vector3 position; 		//coordinates of the ant
	public float size; 				//the size of the ant
	//species;						//species of ant (not sure if useful)
	//direction;					//direction the ant is facing
	public int ID;					//unique identifier
	//colony;						//which colony the ant is a member of
	public int type;				//type of ant e.g. worker
	//nest;							//where the ant was born
	public float health;			//the amount of health the ant has
	public float hungerThreshold;	//the threshold below which the ant will be hungry
	public float healthRate;		//rate at which health decreases
	public bool alive;				//determines whether the ant is alive or not
	//public int goal;				//the current goal the ant is trying to complete
	public Vector3 target;			//the target coord of an item e.g. food
	List<int> itemsInView;			//an array of items within view e.g. ants or food
	List<int> pheremonesInRange;	//an array of pheremones within range
	//prioratisedDirection;			//the direction the ant wants to move in

	public float speed;
	private Rigidbody rb;

	//wander variables for getSteering()
	public float wanderRadius = 1.2f;
	public float wanderDistance = 2f;
	//maximum amount of random displacement a second
	public float wanderJitter = 40f;
	private Vector3 wanderTarget;

	//[SerializeField]
	public Transform _destination;
	NavMeshAgent _navMeshAgent;

	// Use this for initialization
	void Start () 
	{		
		//stuff for the wander behavior for getSteering()
		float theta = Random.value * 2 * Mathf.PI;
		//create a vector to a target position on the wander circle
		wanderTarget = new Vector3(wanderRadius * Mathf.Cos(theta), wanderRadius * Mathf.Sin(theta), 0f);


		_navMeshAgent = this.GetComponent<NavMeshAgent> ();
		if (_navMeshAgent == null) 
		{
			Debug.LogError ("navesh agent component is not attached to " + gameObject.name);
		} 	
	}
	
	// Update is called once per frame
	void Update () 
	{
		getSteering ();
		SetDestination ();
	}

	//add the ant to the map
	void AddToMap()
	{
		
	}

	//remove the ant from the map
	void RemoveFromMap()
	{
		
	}

	//determine whether the ant is hungry
	void IsHungry()
	{

	}

	//take a single peice of food if the ant is standing near it
	void TakeFood()
	{

	}

	//determines if the ant is ontop of the nest (for dropping off food etc.)
	void AtNest()
	{

	}

	//determines whether the ants can see the nest
	void SeeNest()
	{

	}

	//pick a food item to target
	void FindFoodTarget()
	{

	}

	//move towards a piece of food and use the item of food
	void GetFood()
	{

	}

	//decide how to use a piece of food e.g. eat or carry
	void UseFood()
	{

	}

	//scan visable surroundings for items of interest, populates itemsInView
	void Search()
	{

	}

	//detects pheremones within range, populates pheremonesInRange
	void Smell()
	{

	}

	//secretes a pheremone where the ant is currently standing
	void Secrete()
	{

	}

	//decides how the ant moves e.g. follow a pheremone or wander randomly
	void Wander()
	{
		
	}

	//moves the ant in the direction its facing
	private void SetDestination()
	{		
		if (_destination != null)
		{
			Vector3 targetVector = _destination.transform.position;
			_navMeshAgent.SetDestination (getSteering());
			//print (targetVector);
		}
	}

	//kill the ant and remove it from the simulation.
	void Die()
	{

	}

	public Vector3 getSteering ()
	{
		//get the jitter for this time frame
		float jitter = wanderJitter * Time.deltaTime;

		//add a small random vector to the target's position
		wanderTarget += new Vector3 (Random.Range (-1f, 1f) * jitter, 0f, Random.Range (-1f, 1f) * jitter);

		//make the wanderTarget fit on the wander circle again
		wanderTarget.Normalize ();
		wanderTarget *= wanderRadius;

		//move the target in front of the character
		Vector3 targetPosition = transform.position + transform.forward * wanderDistance + wanderTarget;
		targetPosition.y = transform.position.y;

		Debug.DrawLine (transform.position, targetPosition);
		Debug.Log ("target position = " + targetPosition);

		return targetPosition;
	}

}

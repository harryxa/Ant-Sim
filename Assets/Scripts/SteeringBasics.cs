using UnityEngine;
using System.Collections;

/* A helper class for steering a game object in 2D */
using System.Collections.Generic;


//[RequireComponent (typeof (Rigidbody))]
public class SteeringBasics : MonoBehaviour {
	
	public float maxVelocity = 3.5f;
	
	/* The maximum acceleration */
	public float maxAcceleration = 10f;

	/* The radius from the target that means we are close enough and have arrived */
	public float targetRadius = 0.005f;
	
	/* The radius from the target where we start to slow down  */
	public float slowRadius = 1f;
	
	/* The time in which we want to achieve the targetSpeed */
	public float timeToTarget = 0.1f;

	public float turnSpeed = 20f;

	private Rigidbody rb;

	public bool smoothing = true;
	public int numSamplesForSmoothing = 5;
	private Queue<Vector2> velocitySamples = new Queue<Vector2>();

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
	}
	
	/* Updates the velocity of the current game object by the given linear acceleration */
	public void steer(Vector3 linearAcceleration) 
	{
		rb.velocity += linearAcceleration * Time.deltaTime;
		
		if (rb.velocity.magnitude > maxVelocity) 
		{
			rb.velocity = rb.velocity.normalized * maxVelocity;
		}
	}

		
	/* A seek steering behavior. Will return the steering for the current game object to seek a given position */
	public Vector3 seek(Vector3 targetPosition, float maxSeekAccel) 
	{
		//Get the direction
		Vector3 acceleration = targetPosition - transform.position;

		//Remove the z coordinate
		acceleration.y = 0;
		
		acceleration.Normalize ();
		
		//Accelerate to the target
		acceleration *= maxSeekAccel;
		
		return acceleration;
	}

    public Vector3 seek(Vector3 targetPosition)
    {
        return seek(targetPosition, maxAcceleration);
    }

    /* Makes the current game object look where he is going */
    public void lookWhereYoureGoing() 
	{
		Vector3 direction = rb.velocity;

		if (smoothing) 
		{
			if (velocitySamples.Count == numSamplesForSmoothing) 
			{
				velocitySamples.Dequeue ();
			}

			velocitySamples.Enqueue (rb.velocity);

			direction = Vector2.zero;

			foreach (Vector3 v in velocitySamples) 
			{
				direction += v;
			}

			direction /= velocitySamples.Count;
		}

		lookAtDirection (direction);
	}

	public void lookAtDirection(Vector3 direction) 
	{
		direction.Normalize();
		
		// If we have a non-zero direction then look towards that direciton otherwise do nothing
		if (direction.sqrMagnitude > 0.001f) 
		{
			float toRotation = (Mathf.Atan2 (direction.y, direction.z) * Mathf.Rad2Deg);
			float rotation = Mathf.LerpAngle(transform.rotation.eulerAngles.z, toRotation, Time.deltaTime*turnSpeed);
			
			transform.rotation = Quaternion.Euler(0, 0, rotation);
		}
	}



   



 

}
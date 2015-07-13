using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BusinessMenMove : MonoBehaviour
{
	public float moveSpeed;
	public Transform[] destination;
	public int startingDestination;

	void Update ()
	{
		if(transform.position == destination[startingDestination].position){
			startingDestination ++;
		}

		if(startingDestination >= destination.Length){
			startingDestination = 0;
		}

		transform.position = Vector3.MoveTowards(transform.position, destination[startingDestination].position, moveSpeed *Time.deltaTime);

	}
}

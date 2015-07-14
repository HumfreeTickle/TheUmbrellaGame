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
		if (transform.position == destination [startingDestination].position) {
			if (startingDestination >= destination.Length - 1) {
				startingDestination = 0;
			} else {
				startingDestination ++;
			}
		}

		transform.position = Vector3.MoveTowards (transform.position, destination [startingDestination].position, moveSpeed * Time.deltaTime);

	}
}

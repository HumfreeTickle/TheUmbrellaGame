﻿using UnityEngine;
using System.Collections;

public class cameraControl : MonoBehaviour
{
	public Transform umbrella;
	public float speed;
	public float xAway;
	public float yAway;
	public float zAway;
	public float restrictAngle;
	public bool annoyingRotation;

	private float rotationClamp (float angle, float min, float max)
	{
		
		if (angle < 0) {       
			angle += 360;
		}
		if (angle > 360) {
			angle -= 360;
		}
		angle = Mathf.Clamp (angle, min, max);
		return angle;
	}


	// Use this for initialization
	void Start ()
	{
		transform.position = umbrella.position - transform.TransformDirection (new Vector3 (xAway, yAway, zAway));
		transform.LookAt (umbrella);
	}


//	void OnCollisionStay(){
//		print ("Hit");
//			rotationThisWay = false;
//	}

	void LateUpdate ()
	{
		transform.position = umbrella.position - transform.TransformDirection (new Vector3 (xAway, yAway, zAway));//Vector3.Scale(umbrella.position, new Vector3(1, 0, 1)).normalized;
		RotateYaw ();
		RotatePitch ();

	}
	
	void RotateYaw ()
	{
		if (Mathf.Abs (Input.GetAxis ("Horizontal_R")) > 0) {
			transform.RotateAround (umbrella.position, Vector3.up, Input.GetAxis ("Horizontal_R") * speed); 
		}
	}

	//there needs to be a restriction on the vertical
	//I'd say about 60 degrees upwards
	//Also 60 down, unless the camera hits the ground then it stops

	void RotatePitch ()
	{

		if (Mathf.Abs (Input.GetAxis ("Vertical_R")) > 0) {

			if (annoyingRotation) {
				Vector3 dir = transform.position - umbrella.position; // find current direction
				float angle = Vector3.Angle (Vector3.up, dir); // find current angle
			
				if (Vector3.Cross (Vector3.up, dir).x < 0) { // used to create the angle of rotationaround
					angle = -angle;
				}
			
				float rotAng = -1 * speed * (Input.GetAxis ("Vertical_R"));
			
				// calculate the clamped angle after rotation:
				var newAngle = Mathf.Clamp (angle + rotAng, -restrictAngle, restrictAngle);
				// find how much you can rotate without violating limits:
				float rotAngle = newAngle - angle;
				// rotate it:
				transform.RotateAround (umbrella.position, transform.TransformDirection (Vector3.right), rotAngle);
			}
			if (!annoyingRotation) {
				transform.RotateAround (umbrella.position, transform.TransformDirection (Vector3.right), -1 * Input.GetAxis ("Vertical_R") * speed); 
			}
//				transform.localEulerAngles = new Vector3(rotationClamp (transform.rotation.x, 0, 40), transform.eulerAngles.y, transform.eulerAngles.z);

		}
	}




}

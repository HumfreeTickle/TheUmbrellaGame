using UnityEngine;
using System.Collections;

public class cameraControl : MonoBehaviour
{
	public Transform umbrella;
	public float speed;
	public float xAway;
	public float yAway;
	public float zAway;
	private bool rotationThisWay;

	float rotationClamp (float angle, float min, float max)
	{
		
		if (angle < 0) {       
			angle += 360;
		}
		if( angle > 360){
			angle -= 360;
		}
		angle = Mathf.Clamp (angle, min, max);
		print (angle);

		return angle;
	}


	// Use this for initialization
	void Start ()
	{
		transform.position = umbrella.position - transform.TransformDirection (new Vector3 (xAway, yAway, zAway));
		transform.LookAt (umbrella);
	}

//	void OnCollisionStay(Collision terrain){
//		if(terrain.gameObject.tag == "Terrain"){
//			rotationThisWay = false;
//		}else{
//			rotationThisWay = true;
//		}
//	}

	void Update ()
	{
		transform.position = umbrella.position - transform.TransformDirection (new Vector3 (xAway, yAway, zAway));//Vector3.Scale(umbrella.position, new Vector3(1, 0, 1)).normalized;
		RotateYaw ();
		RotatePitch ();
//		transform.rotation = Quaternion.Euler (rotationClamp (transform.rotation.x, 0, 40), transform.eulerAngles.y, transform.eulerAngles.z);

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
			transform.RotateAround (umbrella.position, transform.TransformDirection(Vector3.right), -1* Input.GetAxis ("Vertical_R") * speed); 
		}
	}




}

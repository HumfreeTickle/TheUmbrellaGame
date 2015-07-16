using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {
	// The target we are following
	public Transform target;
	// The distance in the x-z plane to the target
	public float distance = 10.0f;
	// the height we want the camera to be above the target
	public float height = 5.0f;
	// How much we want to damp the movement
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;
		
	void LateUpdate () {
		// check first for a target to ensure the script has been mapped correctly
		if (!target) return;
		// Calculate the current rotation angles (only need quaternion for movement)
		float wantedRotationAngle = target.eulerAngles.y;
		float wantedHeight = target.position.y + height;
		float currentRotationAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;
		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
		// Damp the height
		currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
		// Convert the angle into a euler axis rotation using Quaternions
		Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
		// Set the position of the camera on the x-z plane behind the target
		transform.position = target.position;
		transform.position -= currentRotation * Vector3.forward * distance;
		// Set the height of the camera
		transform.position = new Vector3(transform.position.x,currentHeight,transform.position.z);
		// Set the LookAt property to remain fixed on the target
		transform.LookAt(target);
	}
}

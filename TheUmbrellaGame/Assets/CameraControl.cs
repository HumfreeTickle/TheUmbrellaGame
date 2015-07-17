using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	private Rigidbody rb;
	private Rigidbody playerrb;
	private Transform playerTr;
	private Camera camrea;

	private float newCameraFOV;

	public GameObject player;
	public float speed;

	// The target we are following
	public Transform target;
	// The distance in the x-z plane to the target
	public float distance = 10.0f;
	// the height we want the camera to be above the target
	public float height = 5.0f;
	// How much we want to damp the movement
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;


	
	void Start () {
		rb = GetComponent<Rigidbody>();
		playerrb = player.GetComponent<Rigidbody>();
		playerTr = player.GetComponent<Transform>();
		camrea = GetComponent<Camera>();
	}
	
	void FixedUpdate () {
		if (!player) return;

//		rb.velocity = Vector3.Lerp (rb.velocity, playerrb.velocity, Time.fixedDeltaTime * speed);


		// Calculate the current rotation angles (only need quaternion for movement)
		float wantedRotationAngle = playerTr.eulerAngles.y;
		float wantedHeight = playerrb.velocity.y + height;
		float currentRotationAngle = transform.eulerAngles.y;
		float currentHeight = rb.velocity.y;
		
		
		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

		// Damp the height
		currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

		// Convert the angle into a euler axis rotation using Quaternions
		Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

		// Set the position of the camera on the x-z plane behind the target
//		rb.velocity = Vector3.Lerp (rb.velocity, playerrb.velocity, Time.fixedDeltaTime * speed);

		if( playerrb.velocity.magnitude > 10){
			newCameraFOV =  camrea.fieldOfView + (playerrb.velocity.magnitude*Time.fixedDeltaTime);
			camrea.fieldOfView = Mathf.Lerp (camrea.fieldOfView, Mathf.Clamp(newCameraFOV, 60, 120), Time.fixedDeltaTime);
		}else{
			camrea.fieldOfView = Mathf.Lerp (camrea.fieldOfView , 60, Time.fixedDeltaTime);
		}

		transform.position = Vector3.Lerp (transform.position, playerTr.position, Time.deltaTime* speed);
		transform.position  -= currentRotation * Vector3.forward * distance;
//
//		// Set the height of the camera
		transform.position  = new Vector3(transform.position.x,currentHeight,transform.position.z);

		// Set the LookAt property to remain fixed on the target
		transform.LookAt(playerTr);
	}
}

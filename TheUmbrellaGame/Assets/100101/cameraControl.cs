using UnityEngine;
using System.Collections;

public class cameraControl : MonoBehaviour
{
	public Transform umbrella;
	public float speed;
	public float rotateSpeed;
	public float xAway;
	public float yAway;
	public float zAway;
	public float restrictAngle;
	public bool annoyingRotation;
	public Rigidbody playerrb;
	public GameObject follow;
	private bool deadDead;
	private Camera camrea;
	private float newCameraFOV;
	private Vector3 source;
	private Vector3 target;
	private Vector3 outputVelocity;
	private float speed2;
	const float DECELERATION_FACTOR = 0.6f;
	Vector3 directionToTarget;
	Vector3 velocityToTarget;
	float distanceToTarget;



	// How much we want to damp the movement
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;

	public bool DeadDead {
		get {
			return deadDead;
		}

		set {
			deadDead = value;
		}
	}

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

	//arrive function
	private Vector3 Arrive (Vector3 source, Vector3 target)
	{
		
		distanceToTarget = Vector3.Distance (source, target);
		directionToTarget = Vector3.Normalize (target - source);

		speed2 = distanceToTarget / DECELERATION_FACTOR;

		velocityToTarget = speed2 * directionToTarget;
		return velocityToTarget; // - GetComponent<Rigidbody>().velocity;
	}

	void Start ()
	{
		transform.position = umbrella.position - transform.TransformDirection (new Vector3 (xAway, yAway, zAway));
		transform.LookAt (umbrella);

		camrea = GetComponent<Camera> ();

	}

	void LateUpdate ()
	{
		if (!deadDead) {
			if (!playerrb || !follow)
				return; //checks to see if there is a rigidbody to work off

			if (playerrb.velocity.magnitude > 2f) {

				source = transform.position;
				target = follow.transform.position;

				outputVelocity = Arrive (source, target);
				print (outputVelocity.magnitude);
				
				// Calculate the current rotation angles (only need quaternion for movement)
				float wantedRotationAngle = follow.transform.localEulerAngles.y;

				float wantedHeight = target.y + yAway;

				float currentRotationAngle = transform.eulerAngles.y;
				float currentHeight = transform.position.y;
				
				
				// Damp the rotation around the y-axis
				currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
				
				// Damp the height
				currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
				
				// Convert the angle into a euler axis rotation using Quaternions
				Quaternion currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
				
				// Set the position of the camera on the x-z plane behind the target

				transform.position = Vector3.Lerp (transform.position, follow.transform.position, Time.deltaTime * speed);
				transform.position -= currentRotation * Vector3.forward * zAway;

				// Set the height of the camera
				transform.position = new Vector3 (transform.position.x, currentHeight, transform.position.z);
				
				// Set the LookAt property to remain fixed on the target
				transform.LookAt (follow.transform);

//				newCameraFOV = camrea.fieldOfView + ((playerrb.velocity.magnitude) * speed);
//				camrea.fieldOfView = Mathf.Lerp (camrea.fieldOfView, Mathf.Clamp (newCameraFOV, 60, 80), Time.fixedDeltaTime);

			} else {
//				print ("else");
				Vector3 chillPos = umbrella.position - transform.TransformDirection (new Vector3 (xAway, yAway, zAway));
				transform.position = Vector3.Lerp (transform.position, chillPos, Time.deltaTime);
				camrea.fieldOfView = Mathf.Lerp (camrea.fieldOfView, 60, Time.fixedDeltaTime * speed);

			}
//			RotateYaw ();
//			RotatePitch ();

//-------------------------------------------- Camera Changes on Death -------------------------------------------------------//

		} else {

			if (GetComponent<GmaeManage> ().Timer > 2) {
				transform.position = transform.position - transform.TransformDirection (new Vector3 (xAway, yAway, zAway));
			} else {

				transform.LookAt (umbrella);
			}

		}

	}
	
	void RotateYaw ()
	{
		if (Mathf.Abs (Input.GetAxis ("Horizontal_R")) > 0) {
			transform.RotateAround (umbrella.position, Vector3.up, Input.GetAxis ("Horizontal_R") * rotateSpeed); 
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
			
				float rotAng = -1 * rotateSpeed * (Input.GetAxis ("Vertical_R"));
			
				// calculate the clamped angle after rotation:
				var newAngle = Mathf.Clamp (angle + rotAng, -restrictAngle, restrictAngle);
				// find how much you can rotate without violating limits:
				float rotAngle = newAngle - angle;
				// rotate it:
				transform.RotateAround (umbrella.position, transform.TransformDirection (Vector3.right), rotAngle);
			}
			if (!annoyingRotation) {
				transform.RotateAround (umbrella.position, transform.TransformDirection (Vector3.right), -1 * Input.GetAxis ("Vertical_R") * rotateSpeed); 
			}
//				transform.localEulerAngles = new Vector3(rotationClamp (transform.rotation.x, 0, 40), transform.eulerAngles.y, transform.eulerAngles.z);

		}
	}

	void OnTriggerStay (Collider col)
	{
		if (col.gameObject.GetComponent<MeshRenderer> ()) {
			col.gameObject.GetComponent<MeshRenderer> ().enabled = false;
		}
	}

	void OnTriggerExit (Collider col)
	{
		if (col.gameObject.GetComponent<MeshRenderer> ()) {
			col.gameObject.GetComponent<MeshRenderer> ().enabled = true;
		}
	}
}

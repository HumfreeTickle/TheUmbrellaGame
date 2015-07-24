using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
	private Rigidbody rb;
	private Camera camrea;
	private float newCameraFOV;
	public GameObject umbrella;
	private Transform umbrellaTr;
	private Rigidbody umbrellaRb;
	public float speed;
	public float rotateSpeed;
	public float xAway;
	public float yAway;
	public float zAway;
	
	// The target we are following
	public Transform target;

	// The distance in the x-z plane to the target
	public float distance = 10.0f;
	public float side = 2f;
	// the height we want the camera to be above the target
	public float height = 5.0f;
	// How much we want to damp the movement
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;
	private bool deadDead;

	public bool DeadDead {
		get {
			return deadDead;
		}
		
		set {
			deadDead = value;
		}
	}
	
	void Start ()
	{
		camrea = GetComponent<Camera> ();
		umbrellaTr = umbrella.transform;
		umbrellaRb = umbrella.GetComponent<Rigidbody> ();
	}

	void Update ()
	{
		//Temporary stuff
		if (umbrellaTr.position.y < -20) {
			DeadDead = true;
		}

		//-------------------------------------------- Other Function Calls -------------------------------------------------------//
		if (Time.timeScale == 0) {
			RotateYaw ();
			RotatePitch ();
		}
	}

	void FixedUpdate ()
	{
		if (!deadDead) {

			if (!umbrella) {
				return;
			}

			if (Time.timeScale != 0) {
				// Calculate the current rotation angles (only need quaternion for movement)
				float wantedRotationAngle = umbrellaTr.eulerAngles.y;

				float wantedHeight = umbrellaTr.position.y + height;

				float currentRotationAngle = transform.eulerAngles.y;

				float currentHeight = transform.position.y;
		
				// Damp the rotation around the y-axis
				currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.fixedDeltaTime);

				// Damp the height
				currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.fixedDeltaTime);

				// Convert the angle into a euler axis rotation using Quaternions
				Quaternion currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);

				// Set the position of the camera on the x-z plane behind the target

				if (umbrellaRb.velocity.magnitude > 10) {
					newCameraFOV = camrea.fieldOfView + (umbrellaRb.velocity.magnitude * Time.fixedDeltaTime);
					camrea.fieldOfView = Mathf.Lerp (camrea.fieldOfView, Mathf.Clamp (newCameraFOV, 60, 80), Time.fixedDeltaTime * speed);
				} else {
					camrea.fieldOfView = Mathf.Lerp (camrea.fieldOfView, 60, Time.fixedDeltaTime);
					RotateYaw ();
					RotatePitch ();
				}

				transform.position = Vector3.Lerp (transform.position, umbrellaTr.position, Time.fixedDeltaTime * speed);
				transform.position -= currentRotation * Vector3.forward * distance;

				// Set the height of the camera
				transform.position = new Vector3 (transform.position.x, currentHeight, transform.position.z);

				// Set the LookAt property to remain fixed on the target
				transform.LookAt (umbrellaTr);
			} 
		}
	//-------------------------------------------- Camera Changes on Death -------------------------------------------------------//
		else {
		
			if (GetComponent<GmaeManage> ().Timer > 2) {
				transform.position = transform.position - transform.TransformDirection (new Vector3 (xAway, yAway, zAway));
			} else {
			
				transform.LookAt (umbrellaTr);
			}
		}
	}

//-------------------------------------------- Right Analouge Stick Stuff -------------------------------------------------------//

	void RotateYaw ()
	{
		if (Mathf.Abs (Input.GetAxis ("Horizontal_R")) > 0) {
			transform.RotateAround (umbrellaTr.position, Vector3.up, Input.GetAxis ("Horizontal_R") * rotateSpeed); 
		}
	}
	
	void RotatePitch ()
	{
		if (Mathf.Abs (Input.GetAxis ("Vertical_R")) > 0) {
			transform.RotateAround (umbrellaTr.position, transform.TransformDirection (Vector3.right), -1 * Input.GetAxis ("Vertical_R") * rotateSpeed); 
		}
	}


//-------------------------------------------- Stops Blocked View -------------------------------------------------------//

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


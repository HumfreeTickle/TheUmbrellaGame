using UnityEngine;
using System.Collections;

public class controller : MonoBehaviour
{

	//Empty gameobject that controls on a single plane
	//The umbrella is a facade


	public Rigidbody leftsphere;
	public Rigidbody rightsphere;
	public Rigidbody frontsphere;
	public Rigidbody backsphere;
	public ForceMode movementForce;
	public ForceMode backwardForce;
	public ForceMode rotationForce;
	public Rigidbody handle;
	public Transform movement;
	private Rigidbody rb;
	private float lsphereMass;
	private float rsphereMass;
	private float fsphereMass;
	private float bsphereMass;

//	private float rbMass;
	private float handleMass;
	public float forceAppliedToTilt; // used for tilting purposes
	public float speed;
	public float floating;
	public float turningSpeed;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
//		rbMass = rb.mass;
		lsphereMass = leftsphere.mass;
		rsphereMass = rightsphere.mass;
		fsphereMass = frontsphere.mass;
		bsphereMass = backsphere.mass;
		handleMass = handle.mass;
	}

	void FixedUpdate ()
	{
		Movement ();
		HorizontalMass ();
		VerticalMass ();
		if (Input.GetButton ("DropFromSky")) {
			TheDescent ();
		} else {
			if (GetComponent<CreateWind> ().charge > 1) {
				GetComponent<upwardForce> ().enabled = true;
			}
		}
	}

//----------------------------- OTHER FUNCTIONS ------------------------------------------------------------------------

	void Movement ()
	{

		//*** _____ The direction the umbrella is moving in is an arc, regardless of the force applied ______ ***
		//*** _____ So I need to figure out a way to keep the movement vector going in the same direction ___ ***

		// needs to keep using the absolute value so the player can rock back and forth to gain height
		// after we work out how wind is going to work then it can change

		if (Input.GetAxis ("Vertical_L") > 0.1f) { // Probably should only use forward for this and have back be a kind of breaking system
			rb.AddForce (movement.forward * Input.GetAxis ("Vertical_L") * speed, movementForce); //Add force in the direction it is facing
		} 
//		else if (Input.GetAxis ("Vertical_L") > 0.5f){
//			rb.AddForce (transform.position * Input.GetAxis ("Vertical_L") * speed, movementForce); //Add force in the direction it is facing
//
//		}
		if (Input.GetAxis ("Vertical_L") < 0.1f) { // Probably should only use forward for this and have back be a kind of breaking system




			if (Input.GetAxis ("Vertical_L") > 0) { // Probably should only use forward for this and have back be a kind of breaking system
				rb.AddForce (transform.forward * Input.GetAxis ("Vertical_L") * speed, movementForce); //Add force in the direction it is facing
			}
			if (Input.GetAxis ("Vertical_L") < 0) { // Probably should only use forward for this and have back be a kind of breaking system

				rb.AddForce (transform.forward * Input.GetAxis ("Vertical_L"), movementForce); //Add force in the direction it is facing

				rb.AddForce (transform.forward * Input.GetAxis ("Vertical_L"), backwardForce); //Add force in the direction it is facing


				rb.AddForce (transform.forward * Input.GetAxis ("Vertical_L"), movementForce); //Add force in the direction it is facing

			}

			if (Mathf.Abs (Input.GetAxis ("Horizontal_L")) > 0) { //This shoould rotate the player rather than move sideways
				rb.AddTorque (transform.up * Input.GetAxis ("Horizontal_L") * turningSpeed, rotationForce);
			} else {
				rb.angularVelocity = Vector3.Lerp (rb.angularVelocity, Vector3.zero, Time.deltaTime * 10);
			}
		}
	}

	void HorizontalMass ()
	{
		if (Input.GetAxisRaw ("Horizontal_L") < 0) {
			leftsphere.mass = lsphereMass + forceAppliedToTilt;
		} else if (Input.GetAxisRaw ("Horizontal_L") > 0) {
			rightsphere.mass = rsphereMass + forceAppliedToTilt;
		} else if (Input.GetAxisRaw ("Horizontal_L") == 0) {
			leftsphere.mass = lsphereMass;
			rightsphere.mass = rsphereMass;
		}
	}

	void VerticalMass ()
	{
		if (Input.GetAxisRaw ("Vertical_L") > 0) {
			frontsphere.mass = fsphereMass + forceAppliedToTilt;
			handle.mass = handleMass + forceAppliedToTilt / 2;
		} else if (Input.GetAxisRaw ("Vertical_L") < 0) {
			backsphere.mass = bsphereMass + forceAppliedToTilt * 2;
		} else if (Input.GetAxisRaw ("Vertical_L") == 0) {
			frontsphere.mass = fsphereMass;
			backsphere.mass = bsphereMass;
			handle.mass = handleMass;
		}
	}

	void TheDescent ()
	{

		GetComponent<upwardForce> ().enabled = !GetComponent<upwardForce> ().enabled;
	}
}


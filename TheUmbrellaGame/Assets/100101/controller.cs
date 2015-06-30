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
	public ForceMode theForce;
	public Rigidbody handle;
	private Rigidbody rb;
	private float lsphereMass;
	private float rsphereMass;
	private float fsphereMass;
	private float bsphereMass;
	private float rbMass;
	private float handleMass;
	public float forceApplied;
	public float speed;
	public float floating;
	public float turningSpeed;
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		rbMass = rb.mass;
		lsphereMass = leftsphere.mass;
		rsphereMass = rightsphere.mass;
		fsphereMass = frontsphere.mass;
		bsphereMass = backsphere.mass;
		handleMass = handle.mass;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		Movement ();
		HorizontalMass ();
		VerticalMass ();
		TheDescent ();
	}
	
	void Movement ()
	{
		// needs to keep using the absolute value so the player can rock back and forth to gain height
		// after we work out how wind is going to work then it can change
		if (Mathf.Abs(Input.GetAxis ("Vertical")) > 0) { // Probably should only use forward for this and have back be a kind of breaking system
			rb.AddForce (transform.forward * Input.GetAxis ("Vertical") * speed, theForce); //Add force in the direction it is facing
		}

		if (Mathf.Abs (Input.GetAxis ("Horizontal")) > 0) { //This shoould rotate the player rather than move sideways
			rb.AddTorque (transform.up * Input.GetAxis ("Horizontal") * turningSpeed, theForce);
		} else {
			rb.angularVelocity = Vector3.Lerp (rb.angularVelocity, Vector3.zero, Time.deltaTime * 10);
		}
	}

	void HorizontalMass ()
	{
		if (Input.GetAxisRaw ("Horizontal") < 0) {
			leftsphere.mass = lsphereMass + forceApplied;
		} else if (Input.GetAxisRaw ("Horizontal") > 0) {
			rightsphere.mass = rsphereMass + forceApplied;
		} else if (Input.GetAxisRaw ("Horizontal") == 0) {
			leftsphere.mass = lsphereMass;
			rightsphere.mass = rsphereMass;
		}
	}

	void VerticalMass ()
	{
		if (Input.GetAxisRaw ("Vertical") > 0) {
			frontsphere.mass = fsphereMass + forceApplied;
			handle.mass = handleMass + forceApplied / 2;
		} else if (Input.GetAxisRaw ("Vertical") < 0) {
			backsphere.mass = bsphereMass + forceApplied;
		} else if (Input.GetAxisRaw ("Vertical") == 0) {
			frontsphere.mass = fsphereMass;
			backsphere.mass = bsphereMass;
			handle.mass = handleMass;
		}
	}

	void TheDescent ()
	{
		Mathf.Clamp(rb.mass, 1, 20);
		if (Input.GetKey (KeyCode.Space)) {
			rb.mass += forceApplied;
			rb.mass = Mathf.Clamp(rb.mass, 1, 40);
		} else {
			rb.mass = rbMass;
		}
//		if(Input.GetKeyUp (KeyCode.Space)){
//			rb.AddForce(Vector3.up * 2000);
//		}

	}
}

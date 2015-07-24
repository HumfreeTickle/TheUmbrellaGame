using UnityEngine;
using System.Collections;
using Player.PhysicsStuff;

namespace Player
{
	public class controller : MonoBehaviour
	{

		public GmaeManage gameManager;


		//Empty gameobject that controls on a single plane
		//The umbrella is a facade


		// add in an instatiation of trial renderer objects when the umbrella is moving
		// destroy them when it stops

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
			lsphereMass = leftsphere.mass;
			rsphereMass = rightsphere.mass;
			fsphereMass = frontsphere.mass;
			bsphereMass = backsphere.mass;
			handleMass = handle.mass;
		}

		void FixedUpdate ()
		{
			if (gameManager.gameState == GameState.Game) {
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
			} else if (gameManager.gameState == GameState.GameOver) {
				TheDescent ();
			}
		}

//----------------------------- OTHER FUNCTIONS ------------------------------------------------------------------------

		void Movement ()
		{
			// Tilting now only going to affect the umbrella facade
			// Particles need to be added to the turning to show that movement is happening
			// Also there should be a better slow down for the turning so the player doesn't end up spinning for ages


			//*** _____ The direction the umbrella is moving in is an arc, regardless of the force applied ______ ***
			//*** _____ So I need to figure out a way to keep the movement vector going in the same direction along a y rotation ___ ***
			//if(velocity > 0.1f){
			//instatiate new trial reneder gameObjects
//	}



			if (Input.GetAxis ("Vertical_L") > 0.1f) { // Probably should only use forward for this and have back be a kind of breaking system
				rb.AddForce (transform.TransformDirection (movement.forward) * Input.GetAxis ("Vertical_L") * speed, movementForce); //Add force in the direction it is facing
				//instatiate new trial reneder gameObject
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
					// depending on the direction instatiate a trail renederer
					// if one already exists increase the size
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
}


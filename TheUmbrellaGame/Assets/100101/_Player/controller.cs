﻿using UnityEngine;
using System.Collections;
using Player.PhysicsStuff;
using Inheritence;

namespace Player
{
	public class controller : MonoBehaviour
	{
		public DestroyObject destroyStuff = new Inheritence.DestroyObject ();
		public GmaeManage gameManager;
		public Rigidbody frontsphere;
		public Rigidbody backsphere;
		public Rigidbody leftsphere;
		public Rigidbody rightsphere;
		public Rigidbody handle;
		public GameObject leftTrail;
		public GameObject rightTrail;
		private GameObject trail_L;
		private GameObject trail_R;
		public Animator umbrellaAnim;
		public ForceMode movementForce;
		public ForceMode backwardForce;
		public ForceMode rotationForce;
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
		private string controllerTypeVertical;
		private string controllerTypeHorizontal;
		
		void Start ()
		{
			rb = GetComponent<Rigidbody> ();
			lsphereMass = leftsphere.mass;
			rsphereMass = rightsphere.mass;
			fsphereMass = frontsphere.mass;
			bsphereMass = backsphere.mass;
			handleMass = handle.mass;
			
			controllerTypeVertical = gameManager.ControllerTypeVertical;
			controllerTypeHorizontal = gameManager.ControllerTypesHorizontal;
		}
		
		void FixedUpdate ()
		{
			if (gameManager.gameState == GameState.Game || gameManager.gameState == GameState.Idle) {
				Movement ();
				HorizontalMass ();
				VerticalMass ();

//				if(rb.velocity.magnitude <= 2){
//					gameManager.gameState = GameState.Idle;
//				}else{
//					gameManager.gameState = GameState.Game;
//				}

				if (Input.GetButton ("DropFromSky")) {
					TheDescent ();
				} 
			} else if (gameManager.gameState == GameState.GameOver) {
				TheDescent ();
			}
		}
		
		//----------------------------- OTHER FUNCTIONS ------------------------------------------------------------------------
		
		void Movement ()
		{
			umbrellaAnim.SetFloat ("Input_Vertical", Input.GetAxis (controllerTypeVertical));

			if (Input.GetAxis (controllerTypeVertical) > 0.1f) { // Probably should only use forward for this and have back be a kind of breaking system
				rb.AddForce (transform.TransformDirection (Vector3.forward) * Input.GetAxis (controllerTypeVertical) * speed, movementForce); //Add force in the direction it is facing

				//instatiate new trial reneder gameObject
				if (leftsphere.transform.childCount == 0) {
					trail_L = Instantiate (leftTrail, leftsphere.transform.position, Quaternion.identity) as GameObject;
					trail_L.transform.parent = leftsphere.transform;
				}

				if (rightsphere.transform.childCount == 0) {
					trail_R = Instantiate (rightTrail, rightsphere.transform.position, Quaternion.identity) as GameObject;
					trail_R.transform.parent = rightsphere.transform;
				}
			}
			
			if (Input.GetAxis (controllerTypeVertical) < 0.1f) { // Probably should only use forward for this and have back be a kind of breaking system
				
				if (Input.GetAxis (controllerTypeVertical) > 0) { // Probably should only use forward for this and have back be a kind of breaking system
					rb.AddForce (transform.forward * Input.GetAxis (controllerTypeVertical) * speed, movementForce); //Add force in the direction it is facing
				}
				if (Input.GetAxis (controllerTypeVertical) < 0) { // Probably should only use forward for this and have back be a kind of breaking system
					
					rb.AddForce (transform.forward * Input.GetAxis (controllerTypeVertical), movementForce); //Add force in the direction it is facing
					
					rb.AddForce (transform.forward * Input.GetAxis (controllerTypeVertical), backwardForce); //Add force in the direction it is facing

					rb.AddForce (transform.forward * Input.GetAxis (controllerTypeVertical), movementForce); //Add force in the direction it is facing
					
				}
				
				if (Mathf.Abs (Input.GetAxis (controllerTypeHorizontal)) > 0) { //This shoould rotate the player rather than move sideways
					rb.AddTorque (transform.up * Input.GetAxis (controllerTypeHorizontal) * turningSpeed, rotationForce);

					//--------------------------------------- create trail -----------------------------------------------//
					if (Input.GetAxis (controllerTypeHorizontal) > 0) {
						if (leftsphere.transform.childCount == 0) {
							trail_L = Instantiate (leftTrail, leftsphere.transform.position, Quaternion.identity) as GameObject;
							trail_L.transform.parent = leftsphere.transform;
						}
					} else if (Input.GetAxis (controllerTypeHorizontal) < 0) {
						if (rightsphere.transform.childCount == 0) {
							trail_R = Instantiate (rightTrail, rightsphere.transform.position, Quaternion.identity) as GameObject;
							trail_R.transform.parent = rightsphere.transform;
						}
					}
				} 

				if (rb.velocity.magnitude < 0.3f) {
					destroyStuff.DestroyOnTimer (trail_L, 1);
					destroyStuff.DestroyOnTimer (trail_R, 1);
				}
			}
		}
		
		void HorizontalMass ()
		{
			if (Input.GetAxisRaw (controllerTypeHorizontal) < 0) {
				leftsphere.mass = lsphereMass + forceAppliedToTilt;
			} else if (Input.GetAxisRaw (controllerTypeHorizontal) > 0) {
				rightsphere.mass = rsphereMass + forceAppliedToTilt;
			} else if (Input.GetAxisRaw (controllerTypeHorizontal) == 0) {
				leftsphere.mass = lsphereMass;
				rightsphere.mass = rsphereMass;
			}
		}
		
		void VerticalMass ()
		{
			if (Input.GetAxisRaw (controllerTypeVertical) > 0) {
				frontsphere.mass = fsphereMass + forceAppliedToTilt;
				handle.mass = handleMass + forceAppliedToTilt / 2;
			} else if (Input.GetAxisRaw (controllerTypeVertical) < 0) {
				backsphere.mass = bsphereMass + forceAppliedToTilt * 2;
			} else if (Input.GetAxisRaw (controllerTypeVertical) == 0) {
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


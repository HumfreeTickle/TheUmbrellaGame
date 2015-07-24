using UnityEngine;
using System.Collections;

namespace CameraScripts
{
	public class CameraController : MonoBehaviour
	{
		private float smoothing = 1f; //speed at which the camera moves to the new location
		public float smoothSpeed;
		public Transform umbrella; // this is used to assign the variables with, should be the main sphere
		public Transform whatToLookAt; // what the camer should posiiton itself to look at;

		public float distance = 10;
		public float height = 1;
		private float umbrellaRotation; //Holds the umbrella's y axis rotation
		private float umbrellaHeight; //Holds the umbrella's y axis position

		private float currentRotationAngle; // current rotation along the y axis


		private float umbrellaRotationY; //Holds the umbrella's y axis rotation
		private float umbrellaRotaionX; //Holds the umbrella's x axis rotation


		private float currentRotationAngleY; // current rotation along the y axis
		private float currentRotationAngleX; // current rotation along the x axis



//	private float umbrellaRotationY; //Holds the umbrella's y axis rotation
//	private float umbrellaRotaionX; //Holds the umbrella's x axis rotation
//
//	private float currentRotationAngleY; // current rotation along the y axis
//	private float currentRotationAngleX; // current rotation along the x axis

		private float currentHeight; // current position along the y axis

		private Quaternion currentRotation;




		//**** something is making the camera jerk about awkwardly. But only really when the umbrella is moving upwards. ****


		void LateUpdate ()
		{

			//Sets all the variables
			umbrellaRotationY = umbrella.eulerAngles.y;
			umbrellaRotaionX = umbrella.eulerAngles.x; //clamping does this weird thing where it makes the camera zoom in and out awkwardly

			umbrellaHeight = umbrella.position.y;

			currentRotationAngleY = transform.eulerAngles.y;
			currentRotationAngleX = transform.eulerAngles.x;

			currentHeight = transform.position.y;
		
			//Smoothly transitions between the camera and the umbrella
			currentRotationAngleY = Mathf.LerpAngle (currentRotationAngleY, umbrellaRotationY, smoothing * Time.deltaTime);
			currentRotationAngleX = Mathf.LerpAngle (currentRotationAngleX, umbrellaRotaionX, smoothing * Time.deltaTime);

			//This might be the problem with the jittering. Seems like it calculates tooooo slowly
			currentHeight = Mathf.Lerp (currentHeight, umbrellaHeight + height, smoothing * Time.deltaTime); // This is jst for rising and falling
			currentRotation = Quaternion.Euler (0, currentRotationAngleY, 0); 


			//Actually moves the camera into position
			transform.position = umbrella.position;
			transform.position -= currentRotation * Vector3.forward * distance;
			transform.position = new Vector3 (transform.position.x, currentHeight, transform.position.z);
		
			// Always look at the target
			transform.LookAt (whatToLookAt); // Handy little thing, without it the camera just follows the umbrella awkwardly

			//Changes how smoothly movement is (Probably shouldn't go higher that 1
			smoothing = Mathf.Clamp (smoothSpeed, -1, 1);
//		print (currentRotation);
			//Changes how smoothly movement is (Probably shouldn't go higher that 1)
			smoothing = Mathf.Clamp (smoothSpeed, -1, 1);

			//Changes how smoothly movement is (Probably shouldn't go higher that 1)
			smoothing = Mathf.Clamp (smoothSpeed, -1, 1);


			//Changes how smoothly movement is (Probably shouldn't go higher that 1)
			smoothing = Mathf.Clamp (smoothSpeed, -1, 1);

		}
	}
}
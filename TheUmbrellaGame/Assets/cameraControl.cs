using UnityEngine;
using System.Collections;

public class cameraControl : MonoBehaviour {

	public Transform umbrella;
	public float rotationSpeed;
	public float xAway = -3;
	public float yAway = 3;
	public float zAway = -20;
	public float timeSpeed;
	private float currentHeight;
	private float umbrellaHeight;
	public float smoothing;

	private float umbrellaRotationY; //Holds the umbrella's y axis rotation
	private float umbrellaRotaionX; //Holds the umbrella's x axis rotation

	private float currentRotationAngleY; // current rotation along the y axis
	private float currentRotationAngleX; // current rotation along the x axis

	private Quaternion currentRotation;

	void LateUpdate(){

	// so for position I need to do it in a few steps
		// 1. moves camera to umbrella
		// 2. move the camrea away from the umbreall along the z axis
		// 3. position the height of the camera
		// 4. move the umbrella to right side of the screen
	// there needs to be some form of clamping so you can't spin around too much

		// rotating around the vertical definitely needs a lot of work
		// the horizontal just needs to be smoothed out, there's some jittering going on but pretty sure that has to do with currentRotation

		umbrellaRotationY = umbrella.eulerAngles.y;
//		umbrellaRotaionX = umbrella.eulerAngles.x; //clamping does this weird thing where it makes the camera zoom in and out awkwardly

		currentRotationAngleY = transform.eulerAngles.y;
//		currentRotationAngleX = transform.eulerAngles.x;
		

		//Smoothly transitions between the camera and the umbrella
		currentRotationAngleY = Mathf.LerpAngle (currentRotationAngleY, umbrellaRotationY, smoothing * Time.deltaTime);
//		currentRotationAngleX = Mathf.LerpAngle (currentRotationAngleX, umbrellaRotaionX, smoothing * Time.deltaTime);
		
		//This might be the problem with the jittering. Seems like it calculates tooooo slowly
		currentRotation = Quaternion.Euler (0, currentRotationAngleY, 0); 


//		Vector3 umbrellaPosition = new Vector3(umbrella.position.x + xAway, umbrella.position.y + yAway, umbrella.position.z + zAway);
		umbrellaHeight = umbrella.position.y;

		currentHeight = transform.position.y;
		currentHeight = Mathf.Lerp(currentHeight, umbrellaHeight + yAway, smoothing * Time.deltaTime);

		transform.position = umbrella.position;
		transform.position -= currentRotation * Vector3.forward * zAway;
		transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

		transform.position -= Vector3.right * xAway;
//		Vector3.Lerp(transform.position, umbrellaPosition, Time.deltaTime* timeSpeed);

		verticalMovement();
		horizontalMovement();
		transform.LookAt(umbrella); //kinda works but there's definitely a lot that's interfering with it
	}

	void verticalMovement(){
		if(Mathf.Abs(Input.GetAxis("Vertical_R")) > 0){
//			Mathf.Clamp(transform.rotation.x, -45, 45);
			transform.RotateAround(umbrella.position, Vector3.right, Input.GetAxis("Vertical_R") * rotationSpeed * Time.deltaTime);
//			Mathf.Clamp(transform.rotation.x, -45, 45);
//			transform.rotation = Quaternion.Euler(Mathf.Lerp(transform.eulerAngles.x, 75 * Input.GetAxisRaw("Vertical_R"), Time.deltaTime), transform.rotation.y,transform.rotation.z);
			Vector3 objRotation = transform.rotation.eulerAngles;
			float clampedX = Mathf.Clamp(objRotation.x, -30f, 30f);
			transform.rotation = Quaternion.Euler(new Vector3(clampedX, objRotation.y, objRotation.z));
		}else{
			transform.rotation = Quaternion.Euler(Mathf.Lerp(transform.eulerAngles.x, 0 , Time.deltaTime), transform.rotation.y,transform.rotation.z);

		}
		if(Input.GetAxis("Vertical_R") > 0){
			yAway = Mathf.Lerp(yAway, 10, Time.deltaTime * timeSpeed);
			zAway = Mathf.Lerp(zAway, 20, Time.deltaTime * timeSpeed);
		}
		else if(Input.GetAxis("Vertical_R") < 0){
			yAway = Mathf.Lerp(yAway, 1, Time.deltaTime * timeSpeed);
			zAway = Mathf.Lerp(zAway, 10, Time.deltaTime * timeSpeed);
		}else{
			yAway = Mathf.Lerp(yAway, 3, Time.deltaTime * timeSpeed);
			zAway =  Mathf.Lerp(zAway, 25, Time.deltaTime * timeSpeed);
		}
	}

	void horizontalMovement(){
		if(Mathf.Abs(Input.GetAxis("Horizontal_R")) > 0){
			print ("H_R");
			transform.RotateAround(umbrella.position, Vector3.up, Input.GetAxis("Horizontal_R") * rotationSpeed * Time.deltaTime);
		}
	}
}

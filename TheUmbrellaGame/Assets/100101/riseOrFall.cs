using UnityEngine;
using System.Collections;

public class riseOrFall : MonoBehaviour {
	
	private Rigidbody umbrellaBody;
	private bool umbrellaBobbing;
	public float velocityOfFall;
	public bool falling = false;

	public GameObject leaves;
	// Use this for initialization
	void Start () {
		umbrellaBody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!falling) {
			Fall ();
		} else {
			Rise();
		}

		umbrellaBody.velocity = Vector3.Lerp (umbrellaBody.velocity, new Vector3 (umbrellaBody.velocity.x, 0, umbrellaBody.velocity.z), Time.deltaTime);
	}

	void Rise(){

		if(Input.GetKey(KeyCode.Space)){
			velocityOfFall += Time.deltaTime*10;
		}
//		if(!Input.GetKey(KeyCode.Space)){
//			velocityOfFall = Mathf.Abs(umbrellaBody.velocity.y);
//		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			//GetComponent<Bobbing> ().enabled = true;

			umbrellaBody.useGravity = !enabled; //Turns the gravity off
			falling = false; // Changes umbrella to not falling
			umbrellaBody.AddForce (Vector3.up * (3 * velocityOfFall), ForceMode.VelocityChange); //Adds an upward force and nulls the gravity force already applied to the umbrella
//			print (Vector3.up * (3 * velocityOfFall));
			Instantiate (leaves, transform.position + new Vector3(0, -1, 0), Quaternion.identity); //Create some leaf particles
			velocityOfFall = 0; //Reverts fall Velocity to 0
		}
	}

	void Fall(){
		if(Input.GetKeyUp(KeyCode.Space))
		{
			//GetComponent<Bobbing> ().enabled = false;
			umbrellaBody.useGravity = enabled;
			falling = true;
		}

	}
}

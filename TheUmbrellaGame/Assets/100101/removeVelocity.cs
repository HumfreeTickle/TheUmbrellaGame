using UnityEngine;
using System.Collections;

public class removeVelocity : MonoBehaviour {

	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!Input.anyKeyDown) {
			rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.fixedDeltaTime);
		}
	}
}

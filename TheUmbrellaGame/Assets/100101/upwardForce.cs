using UnityEngine;
using System.Collections;

public class upwardForce : MonoBehaviour {

	public float upwardsforce = 10f;
	private Rigidbody rb;

	public float conterBalance = 1;
	private float mainMass;

	private float sw;
	public float sine;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		mainMass = rb.mass;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
//		SineWave ();
		Vector3 force = Vector3.up * upwardsforce;
		rb.AddForce (force);
	}

	void SineWave(){
		sine = 0;
		sine += Time.time;
		sw = Mathf.Sin (sine);
		print (sw);
		rb.mass = mainMass + sw/conterBalance;
	}
}

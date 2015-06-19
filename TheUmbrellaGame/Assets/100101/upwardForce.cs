﻿using UnityEngine;
using System.Collections;

public class upwardForce : MonoBehaviour {

	public float upwardsforce = 10f;
	private Rigidbody rb;

	public float conterBalance = 1;
	private float mainMass;

	private float sw;
	public float sine;

	public ForceMode theForce;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		mainMass = rb.mass;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		SineWave ();
		Vector3 force = Vector3.up * upwardsforce;
		rb.AddForce (force, theForce);
	}

	void SineWave(){
		if(sine >= (Mathf.Sin(Mathf.PI/2))){
			sine = 0;
		}else{
			sine += Time.time;
		}

		sw = Mathf.Sin (sine);
		upwardsforce = upwardsforce + sw/conterBalance;
	}
}

﻿using UnityEngine;
using System.Collections;

public class Push : MonoBehaviour {

	public float Speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown ("Horizontal")){
			//	Engine.PlayOneShot(Tank, 1);
		}
		transform.Translate (Vector3.right * Speed *Time.deltaTime * Input.GetAxis("Horizontal"));//every frame the character move
	
	}
	void OnCollisionStay(Collision collision) {
		if (collision.rigidbody)
			collision.rigidbody.AddForce(Vector3.right * 50);
		
	}
}
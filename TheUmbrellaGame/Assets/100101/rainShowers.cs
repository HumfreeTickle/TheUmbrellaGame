﻿using UnityEngine;
using System.Collections;

public class rainShowers : MonoBehaviour
{
	private Material cloudColor;
	private Vector3 cloudSize = new Vector3 (10.5f, 7.5f, 7.5f);
	private Vector3 originalCloudSize;
	private Color soNowItRains = new Vector4 (0.3f, 0.3f, 0.3f, 1);
	public GameObject rainSystem;
	public bool raining = false;
	public float _timer = 0;
	private Quaternion cloudRotation;
	private GameObject newCloud;
	private float _expire;
	private float _whenShoulditRain;
	

	//Needs to rain only for a little bit then slowly stop
	//Clouds should get bigger the darker they get and the light is gonna have to reflect that. I had them casting shadows at one time....hmm

	void Start ()
	{
		cloudColor = GetComponent<MeshRenderer> ().materials [0];
		originalCloudSize = transform.localScale;
	}
	
	// Update is called once per frame
	void Update ()
	{  
		_whenShoulditRain = GameObject.Find("Clouds").GetComponent<WhenShouldItRain>().whenToRain;
		if (_whenShoulditRain < 60) {
			
		}
		if (_whenShoulditRain >= 60) {
			ChangeColour ();
		}
	}

	void ChangeColour ()
	{
		Color darker = new Vector4 (0.2f, 0.2f, 0.2f, 1);
		cloudColor.color = Color.Lerp (cloudColor.color, darker, Time.deltaTime / 10);


		if (!raining) {
			MakeItRain ();
		}
		if (raining) {

			_expire = 60;
			_timer += Time.deltaTime;

			/// Stops the rain/// -- Happens to quickly
			if (_timer > _expire / 1.5) {
				StopTheRain ();
			}
		}
	}

	void MakeItRain ()
	{
		if (cloudColor.color.r >= soNowItRains.r) {
			transform.localScale = Vector3.Lerp (transform.localScale, cloudSize, Time.deltaTime / 10);
		}
		if (cloudColor.color.r <= soNowItRains.r) {
			foreach (Transform child in transform) {  
				child.gameObject.SetActive (true);   
			} 
			raining = true;
		}
	}

	void StopTheRain ()
	{
		transform.localScale = Vector3.Lerp (transform.localScale, originalCloudSize, Time.deltaTime / 10);

		if (_timer >= _expire) {
			cloudColor.color = Color.white;
//			
			foreach (Transform child in transform) {  
				child.gameObject.SetActive (false);   
			} 
			if (transform.localScale.x < originalCloudSize.x) {
				_timer = 0;
				_whenShoulditRain = 0;
				raining = false;
			}
		}
	}
}

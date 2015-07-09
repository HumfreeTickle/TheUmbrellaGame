using UnityEngine;
using System.Collections;

public class CreateWind : MonoBehaviour
{

	public GameObject windSystem;
	private GameObject instatiatedWind;
	private Vector3 spawnDistance;
	public float charge = 0;
	private float _timer;

	void Update ()
	{
		if (transform.position.y <= 30 && transform.childCount == 0) {
			windFalling ();
		}
		if (Input.GetButtonDown ("Fire1") && transform.childCount == 0) {
			SummonWind ();
		}
//		Charging ();
		Mathf.Clamp (_timer, 0, 20);
	}

	void windFalling ()
	{
		spawnDistance = transform.position - new Vector3 (0, 10, 0);

		instatiatedWind = Instantiate (windSystem, spawnDistance, Quaternion.Euler (Vector3.forward)) as GameObject;
		instatiatedWind.transform.parent = this.transform;                             
		instatiatedWind.GetComponent<wind> ().windForce = 2000;
	}

	void SummonWind ()
	{
		print ("wind");
		spawnDistance = transform.position - new Vector3 (0, 10, 0);
		
		instatiatedWind = Instantiate (windSystem, spawnDistance, Quaternion.Euler (Vector3.forward)) as GameObject;
		instatiatedWind.transform.parent = this.transform; 
		instatiatedWind.GetComponent<ParticleSystem> ().enableEmission = true;
		instatiatedWind.GetComponent<wind> ().windForce = charge;

		if (Input.GetButtonUp ("Fire1")) {
			instatiatedWind.GetComponent<ParticleSystem> ().enableEmission = false;
		}

		_timer = 0;
	}

	void Charging ()
	{

		_timer += Time.fixedDeltaTime;
		charge = Mathf.Clamp (_timer, 0, 40) * 100;
	}
}

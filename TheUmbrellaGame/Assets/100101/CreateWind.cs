using UnityEngine;
using System.Collections;

public class CreateWind : MonoBehaviour {

	public GameObject windSystem;
	private GameObject instatiatedWind;
	private Vector3 spawnDistance;

	void Update () {
		if(transform.position.y <= 50 && transform.childCount == 0){
			windFalling();
		}
		if(Input.GetButtonDown("Fire1") && transform.childCount == 0){
			SummonWind();
		}
	}

	void windFalling(){
		spawnDistance = transform.position - new Vector3(0, 10, 0);

		instatiatedWind = Instantiate(windSystem, spawnDistance, Quaternion.Euler(Vector3.forward)) as GameObject;
		instatiatedWind.transform.parent = this.transform;                             
	}

	void SummonWind(){
		print ("wind");
//		spawnDistance = transform.position - new Vector3(0, 10, 0);
//		
//		instatiatedWind = Instantiate(windSystem, spawnDistance, Quaternion.Euler(Vector3.forward)) as GameObject;
//		instatiatedWind.transform.parent = this.transform;  
	}
}

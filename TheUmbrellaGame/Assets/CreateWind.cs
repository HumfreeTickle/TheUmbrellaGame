using UnityEngine;
using System.Collections;

public class CreateWind : MonoBehaviour {

	public GameObject windSystem;
	private GameObject instatiatedWind;
	private float distanceToTheGround;

	void Update () {
		Rays();
		if(transform.position.y <= 50 && transform.childCount < 2){
			windCreation();
		}
	}

	void Rays(){
		RaycastHit hit;
		Vector3 downwards = Vector3.down * 100;
		LayerMask ignoredLayer = LayerMask.NameToLayer("Ignore Raycast");
		if(Physics.Raycast(transform.position, downwards, out hit, ignoredLayer)){
			print(hit.collider.name);
			if(hit.collider.tag == "Terrain"){
				distanceToTheGround = hit.distance;
			}
		}
		Debug.DrawRay(transform.position, downwards, Color.green, 500);
	}

	void windCreation(){
		Vector3 spawnDistance;
		if(distanceToTheGround > 20){
			spawnDistance = transform.position - new Vector3(0, 10, 0);
		}else{
			spawnDistance = transform.position - new Vector3(0, distanceToTheGround, 0);
		}

		instatiatedWind = Instantiate(windSystem, spawnDistance, Quaternion.LookRotation(transform.position)) as GameObject;
		instatiatedWind.transform.parent = this.transform;                             
	}
}

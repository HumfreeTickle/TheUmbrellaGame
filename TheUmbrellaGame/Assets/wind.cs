using UnityEngine;
using System.Collections;

public class wind : MonoBehaviour {

	public float windForce = 100;
	public Transform umbrellaObject;

	void Update(){
		umbrellaFalls();
	}

	void umbrellaFalls(){
		print (umbrellaObject.position.y);
		if(umbrellaObject.position.y <= 40){
			GetComponent<ParticleSystem>().enableEmission = true;
			print (GetComponent<ParticleSystem>().enableEmission);
		}else{
			GetComponent<ParticleSystem>().enableEmission = false;
		}
	}

	void OnParticleCollision(GameObject umbrella){
		if(umbrella.name == "main_Sphere"){
			print("Hit");
			umbrella.GetComponent<Rigidbody>().AddForce(Vector3.up * windForce);
		}
	}
}

using UnityEngine;
using System.Collections;

public class wind : MonoBehaviour {

	public float windForce = 100;
	public Transform umbrellaObject;

	void Awake(){
		umbrellaObject = GameObject.Find("Umbrella").transform;
	}

	void Update(){
		transform.LookAt(GameObject.Find("main_Sphere").transform);
		umbrellaFalls();
		Death();
	}

	void umbrellaFalls(){
		if(umbrellaObject.position.y <= 40){
			GetComponent<ParticleSystem>().enableEmission = true;
		}else{
			GetComponent<ParticleSystem>().enableEmission = false;
		}
	}

	void OnParticleCollision(GameObject umbrella){
		if(umbrella.name == "main_Sphere"){
			umbrella.GetComponent<Rigidbody>().AddForce(Vector3.up * windForce);
		}
	}

	void Death(){
		Destroy(this.gameObject, 5f);
	}
}

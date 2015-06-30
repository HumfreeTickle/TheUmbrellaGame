using UnityEngine;
using System.Collections;

public class Barriers : MonoBehaviour {

	public float knockBack;
	public Vector3 directionOfForce;


	void OnTriggerStay(Collider barrier){
		if(barrier.tag == "Player"){
			barrier.GetComponent<Rigidbody>().AddForce(knockBack * directionOfForce);
		}
	}
}
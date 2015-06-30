using UnityEngine;
using System.Collections;

public class constraint : MonoBehaviour {

// not sure if what this exactly does or if it needs to be kept or not

	public float maxAngle;


	void FixedUpdate () {
		Constrain();
	}

	void Constrain(){
		if(transform.rotation.x > maxAngle){
			transform.rotation = Quaternion.Euler(Mathf.Clamp(transform.rotation.x, 0, maxAngle), transform.rotation.y, transform.rotation.z);
		}
	}
}

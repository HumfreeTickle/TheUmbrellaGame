using UnityEngine;
using System.Collections;

public class constraint : MonoBehaviour {


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

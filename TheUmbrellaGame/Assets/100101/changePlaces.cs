using UnityEngine;
using System.Collections;

public class changePlaces : MonoBehaviour {

	public Transform physicsTransform;
	void Start(){
		transform.position = new Vector3(transform.position.x, physicsTransform.position.y, transform.position.z);
//		transform.rotation = Quaternion.Euler(physicsTransform.rotation.x + 270, physicsTransform.rotation.y, physicsTransform.rotation.z); 
	}
}

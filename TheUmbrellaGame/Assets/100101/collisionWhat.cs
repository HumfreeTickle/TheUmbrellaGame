using UnityEngine;
using System.Collections;

//probably stil useful for testing and what not

public class collisionWhat : MonoBehaviour {

	void OnCollisionEnter(Collision col){
		print (col.gameObject);
	}
}

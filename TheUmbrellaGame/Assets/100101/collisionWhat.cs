using UnityEngine;
using System.Collections;

public class collisionWhat : MonoBehaviour {

	void OnCollisionEnter(Collision col){
		print (col.gameObject);
	}
}

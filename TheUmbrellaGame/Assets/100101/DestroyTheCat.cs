using UnityEngine;
using System.Collections;

public class DestroyTheCat : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnCollisionEnter (Collision Col) {
		if (Col.gameObject.tag == "Collectable") {
			Col.gameObject.SetActive(false);
		}
	}
}

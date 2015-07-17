using UnityEngine;
using System.Collections;

public class DestroyTheCat : MonoBehaviour {

//----------------------------------------- Achievement Unlocked -------------------------------------------------------

	void OnCollisionEnter (Collision Col) {
		if (Col.gameObject.tag == "Collectable") {
			Col.gameObject.SetActive(false);
		}
	}
}

using UnityEngine;
using System.Collections;

public class destroyParticle : MonoBehaviour {
	private float _timer;	

//------------------------------ Destroys particles and the like -----------------------------------

	void Update () {
		_timer += Time.deltaTime;
		if (_timer > 3) {
			Destroy(gameObject);
		}
	}
}

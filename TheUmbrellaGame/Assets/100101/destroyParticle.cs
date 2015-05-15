using UnityEngine;
using System.Collections;

public class destroyParticle : MonoBehaviour {
	private float _timer;	

	// Update is called once per frame
	void Update () {
		_timer += Time.deltaTime;
		if (_timer > 3) {
			Destroy(gameObject);
		}
	}
}

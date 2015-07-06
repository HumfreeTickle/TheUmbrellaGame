using UnityEngine;
using System.Collections;

public class moveSomewhere : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		transform.position += new Vector3(0, 0, Time.fixedDeltaTime);
	}
}

using UnityEngine;
using System.Collections;

public class Barriers : MonoBehaviour
{

	public float knockBack;
	public Vector3 directionOfForce;


	void OnTriggerStay (Collider barrier)
	{
		if (barrier.tag == "Player") {


			barrier.GetComponent<Rigidbody> ().drag = Mathf.Lerp (barrier.GetComponent<Rigidbody> ().drag, 10, Time.fixedDeltaTime / 10);
			if (barrier.GetComponent<Rigidbody> ().drag > 9) {
				barrier.GetComponent<Rigidbody> ().AddForce (knockBack * directionOfForce);
			}
		}
	}

	void OnTriggerExit (Collider barrier)
	{
		if (barrier.tag == "Player") {
			barrier.GetComponent<Rigidbody> ().drag = 0;
		}
	}
}
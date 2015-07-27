using UnityEngine;
using System.Collections;

namespace Player
{
	public class wind : MonoBehaviour
	{

		public float windForce;
		public Transform umbrellaObject;

		void Awake ()
		{
			umbrellaObject = GameObject.Find ("Umbrella").transform;
		}

		void Update ()
		{
			transform.LookAt (GameObject.Find ("main_Sphere").transform);
			umbrellaFalls ();
			Death ();
		}

		//----------------------------- OTHER FUNCTIONS ------------------------------------------------------------------------

		void umbrellaFalls ()
		{ //this was to make it a once off thing
			if (umbrellaObject.position.y <= 20) {
				GetComponent<ParticleSystem> ().enableEmission = true;
			} else if (GetComponent<ParticleSystem> () != null) {
				GetComponent<ParticleSystem> ().enableEmission = false;
			}
		}

		void OnParticleCollision (GameObject umbrella)
		{
			if (umbrella.name == "main_Sphere") {
				umbrella.GetComponent<Rigidbody> ().AddForce (Vector3.up * windForce);
			}
		}

		void Death ()
		{
			Destroy (this.gameObject, 5f);
		}
	}
}
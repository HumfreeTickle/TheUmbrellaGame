﻿using UnityEngine;
using System.Collections;

namespace Player
{
	public class upwardForce : MonoBehaviour
	{

		public float upwardsforce = 10f;
		private Rigidbody rb;
		public float conterBalance = 1;
//	private float mainMass;

		private float sw;
		public float sine;
		public ForceMode theForce;

		void Start ()
		{
			rb = GetComponent<Rigidbody> ();

		}
	
		void FixedUpdate ()
		{
			SineWave ();
			Vector3 force = Vector3.up * upwardsforce;
			rb.AddForce (force, theForce);
		}

		//----------------------------- OTHER FUNCTIONS ------------------------------------------------------------------------

		void SineWave ()
		{
			if (sine >= (Mathf.Sin (Mathf.PI / 2))) {
				sine = 0;
			} else {
				sine += Time.time;
			}

			sw = Mathf.Sin (sine);
			upwardsforce = upwardsforce + sw / conterBalance;
		}
	}
}

﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Inheritence;

namespace Useless
{
	public class EnterGame : MonoBehaviour
	{
		private FadeScript fading = new FadeScript ();
		public Image whiteIN;
		public float speed;
		public bool Starting;
	
		void Update ()
		{
			if (Starting) {
				fading.Fades (whiteIN, 1, speed);

				if (whiteIN.color.a > 0.5) {
					Invoke ("whichLevel", 3); //delays the loading of Level-1
				}
			}
		}
	
		void OnTriggerEnter (Collider other)
		{
			if (other.gameObject.tag == "Player") {
				Starting = true;
			}
		}
		
		void whichLevel ()
		{
			Application.LoadLevel (1); //Changes to the next scene
		}
	}
}

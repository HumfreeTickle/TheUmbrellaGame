using UnityEngine;
using System.Collections;

public class ToPause : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyUp (KeyCode.T)){

			Time.timeScale = 0f;
			Debug.Log("PAUSE!!!!!!!!");
		}
	
	}
}

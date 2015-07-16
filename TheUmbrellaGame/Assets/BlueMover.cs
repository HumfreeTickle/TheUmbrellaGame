using UnityEngine;
using System.Collections;

public class BlueMover : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if ( Input.GetKey(KeyCode.RightArrow)){

			gameObject.transform.Translate(Vector3.right * Time.deltaTime);

		}
		if ( Input.GetKey(KeyCode.LeftArrow)){
			
			gameObject.transform.Translate(Vector3.left * Time.deltaTime);
			
		}
		if ( Input.GetKey(KeyCode.UpArrow)){
			
			gameObject.transform.Translate(Vector3.forward * Time.deltaTime);
      
		}
		if ( Input.GetKey(KeyCode.DownArrow)){
			
			gameObject.transform.Translate(Vector3.back * Time.deltaTime);
      
		}
	}
}

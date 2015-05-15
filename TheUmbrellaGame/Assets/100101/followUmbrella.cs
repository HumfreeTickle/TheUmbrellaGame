using UnityEngine;
using System.Collections;

public class followUmbrella : MonoBehaviour {

	public Transform umbrella;
	private float umbrellaX;
	private float umbrellaZ;
	private float followY;
	private float umbrellaY;
	public float umbrellaYoffSet = 0;
//	private float speed = 300;

	void Start(){
		followY = transform.position.y;
	}
	// Update is called once per frame
	void Update () {
		if (umbrella.position.y > - 30) {
			umbrellaY = umbrella.position.y + umbrellaYoffSet;
			umbrellaX = umbrella.position.x;
			umbrellaZ = umbrella.position.z;
			transform.position = new Vector3 (umbrellaX, followY, umbrellaZ);
		}
			updateY ();
		//rotateCamera ();
	}

	void updateY(){
		if (umbrella.position.y >= transform.position.y + 2 || umbrella.position.y <= transform.position.y - 2) {

			followY = Mathf.Lerp(followY, umbrellaY, Time.deltaTime);
		}
	}

	void rotateCamera(){
	}
}

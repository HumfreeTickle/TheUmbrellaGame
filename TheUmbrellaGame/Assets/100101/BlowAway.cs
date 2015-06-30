using UnityEngine;
using System.Collections;

public class BlowAway : MonoBehaviour {

	public float blow;
	public Vector3 way;


	// Use this for initialization
	void Start () {

	}

	void Update(){
		transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime * .25f);
//		transform.position = new Vector3(Mathf.PingPong(Time.time, 3), transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other){
		if(other.gameObject.tag == "Player"){
		other.GetComponent<Rigidbody>().AddForce(blow * way);//blow back the umbrella
			Debug.Log("ShouldBlow");
		}
	}
}

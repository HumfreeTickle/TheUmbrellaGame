using UnityEngine;
using System.Collections;

public class TheFollower : MonoBehaviour {

	Transform THEtarget;
	public float maxRange;
	public float minRange;
	public bool inSight;
	public float speed;
	public GameObject dropOFF;

	// Use this for initialization
	void Start () {
		inSight = false;
		THEtarget = GameObject.FindGameObjectWithTag("Player").transform;

	}
	
	// Update is called once per frame
	void Update () {
	
		if((Vector3.Distance(transform.position,THEtarget.position)<maxRange)
		   && (Vector3.Distance(transform.position,THEtarget.position)>minRange)){

			inSight = true;

		}
		if(Vector3.Distance(transform.position,THEtarget.position)>maxRange){

			Debug.Log("ShouldStop");
			inSight = false;
		}

		if(inSight){

			Debug.Log ("Should See");
			transform.LookAt(THEtarget);
			transform.Translate(Vector3.forward * Time.deltaTime * speed);
			dropOFF.SetActive(true);

		}
		if(!inSight){

			dropOFF.SetActive(false);
		}
	
	}
	void OnTriggerEnter(Collider other){

		if(other.gameObject.tag == "Dropper"){

			Destroy(gameObject);
			Destroy(dropOFF);
		}
	}
	}


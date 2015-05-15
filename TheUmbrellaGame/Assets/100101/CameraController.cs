using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {


	public GameObject playerToFollow;
	public float zMargin = 5;
	public float xMargin = 10;
	public float cameraDistance;

	private float zTarget;
	private float xTarget;
	private bool movingX;

	// Use this for initialization
	void Start () {
		zTarget = transform.position.z;
		xTarget = transform.position.x;
	}

	private bool CheckZMargin()
	{
		// Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
		return Mathf.Abs(transform.position.z - playerToFollow.transform.position.z) > zMargin;
	}

	private bool CheckXMargin()
	{
		// Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
		return Mathf.Abs(transform.position.x - playerToFollow.transform.position.x) > xMargin;
	}

	// Update is called once per frame
	void Update () {
		print (CheckZMargin());
		if (CheckZMargin()) {
			print (transform.position.z - playerToFollow.transform.position.z);
			zTarget = Mathf.Lerp(transform.position.z, playerToFollow.transform.position.z - cameraDistance, Time.deltaTime);
		}
		if (CheckXMargin ()) {
			movingX = true;
			if(movingX){
				xTarget = Mathf.Lerp(transform.position.x, playerToFollow.transform.position.x, Time.deltaTime);
				if(transform.position.x == playerToFollow.transform.position.x){
					movingX = false;
				}
			}
		}


		transform.position = new Vector3(xTarget, transform.position.y, zTarget);
	}
}

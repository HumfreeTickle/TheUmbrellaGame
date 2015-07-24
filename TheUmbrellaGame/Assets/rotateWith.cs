using UnityEngine;
using System.Collections;

public class rotateWith : MonoBehaviour
{

	public Transform player;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.Find ("Umbrella").transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		RotateWith (player);
	}

	void RotateWith (Transform turning)
	{
		float yRotation = turning.rotation.y;
		transform.rotation = Quaternion.Euler (transform.rotation.x, yRotation, transform.rotation.z);
	}
}

using UnityEngine;
using System.Collections;

public class CloudBehave : MonoBehaviour
{

	// This should move the cloud from right to left,slowly. When a cloud gets to a certain point it instatiates a new cloud then deletes itself.
	// Would be better if this was a class that generated all the clouds at random positions.


	private Rigidbody cloud;
	public float speedOfCloud;
	public GameObject newCloud;

	[System.NonSerialized]
	public Quaternion cloudRotation = new Quaternion (0.7f, 0, 0, -0.7f);

	// Use this for initialization
	void Start ()
	{	

		cloud = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		cloud.AddForce(Vector3.right*-1* Time.fixedDeltaTime*speedOfCloud);
		createSomeClouds ();
		ignoreOthers ();
	}

	void createSomeClouds ()
	{
		if (transform.position.x <= -600) {
			Instantiate (newCloud, new Vector3 (500, transform.position.y, transform.position.z), cloudRotation);
			Destroy (gameObject);
		}
	}

	void ignoreOthers ()
	{
		Physics.IgnoreCollision(GameObject.FindWithTag("Clouds").GetComponent<MeshCollider>(), GameObject.FindWithTag("Clouds").GetComponent<MeshCollider>());
	}
}

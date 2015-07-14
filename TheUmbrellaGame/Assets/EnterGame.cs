using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnterGame : MonoBehaviour
{

	public Image whiteIN;
	private Color whitey;
	public float speed;
	public bool Starting;

	// Use this for initialization
	void Start ()
	{
	
		whitey = whiteIN.color;
	}

	void Update ()
	{

		if (Starting) {
			fadeWhite ();
		}

	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player") {
			Debug.Log ("SceneShouldStart");
			Starting = true;
		}
	}

	void fadeWhite ()
	{
		whitey.a = Mathf.Lerp (whitey.a, 1, Time.deltaTime * speed);
		whiteIN.color = whitey;
	}
}

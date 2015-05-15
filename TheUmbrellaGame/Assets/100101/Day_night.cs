using UnityEngine;
using System.Collections;

public class Day_night : MonoBehaviour
{

	public int nightSpeed; //Speed the light rotate at.
	private Light sun;

	void Start ()
	{
		sun = GetComponent<Light> ();
	}
	// Update is called once per frame
	void Update ()
	{
		dayNight ();
	}

	void dayNight ()
	{
		transform.Rotate (new Vector3 (-1 * Time.deltaTime * nightSpeed, 0, 0));
//		sun.intensity = Mathf.Lerp(sun.intensity, 0, Time.deltaTime/100);
	}
}

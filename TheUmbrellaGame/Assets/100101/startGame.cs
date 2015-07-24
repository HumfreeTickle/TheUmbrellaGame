using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class startGame : MonoBehaviour
{

//------------------------------------------ Needs to be moved into the GmaeManager script ------------------------------------------


	private bool nextLevel; // has the transtion to Level-1 been activated
	public float changeTime = 1.0f;
	public float timer;
	public bool starting;
	private Image umbrellaGame;
	private Color fading;
	public GameObject startButton;
	private Color startButtonImage;
	public float Speed;
	public bool timeStart;
	public GameObject brolly;
	public float thrust;
	private Rigidbody umbrella;
	public float softly;
	private float lerpNumber = 1;

//--------------------------------------------------- Sets up all the relevent stuff ------------------------------------------

	void Start ()
	{

		starting = false;
		fading.a = 0;
		umbrellaGame = GetComponent<Image> ();
		fading = GetComponent<Image> ().color;
		umbrella = brolly.GetComponent<Rigidbody> ();
		startButtonImage = startButton.GetComponent<Image> ().color;

	}

//----------------------------------------------- Checks and calls ------------------------------------------

	void Update ()
	{

		if (Input.GetButtonDown ("Submit")) {
			if (!startButton) {
				return;
			}
			timeStart = true;
			startButton.GetComponent<Animator> ().enabled = false;
		}
		if (timeStart) {
			startButton.GetComponent<Image> ().color = startButtonImage;
			umbrellaGame.color = fading;

			if (fading.a > 0.9f) {
				lerpNumber = -1;
				starting = true;
			}

			Fade ();
		}

		if (starting) {
			FlyUmbrellaFly ();
		}
	}

//----------------------------------------------- Other Funcitons ------------------------------------------
	
	void FlyUmbrellaFly ()
	{
		umbrella.AddForce (2, 5, 0 * softly);
	}

	void Fade ()
	{
		startButtonImage.a = Mathf.Lerp (startButtonImage.a, 0, Time.deltaTime * 10);
		fading.a = Mathf.Lerp (fading.a, lerpNumber, Time.deltaTime * Speed);
	}
}


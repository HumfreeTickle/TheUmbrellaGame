using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class startGame : MonoBehaviour {

	private bool nextLevel; // has the transtion to Level-1 been activated
	public float changeTime = 1.0f;
	public float timer;
	public bool starting;
	private Image umbrellaGame;
	private Color fading;
	public float Speed;
	public bool timeStart;
	public GameObject brolly;
	public float thrust;
	private Rigidbody umbrella;
	public float softly;


	void Start(){

		timer = 200;
		starting = false;
		fading.a = 0;
		umbrellaGame = GetComponent<Image>();
		fading = GetComponent<Image>().color;
		umbrella = brolly.GetComponent<Rigidbody>();

	}


	void Update(){

		if (Input.GetKeyDown (KeyCode.Q)){

			timeStart = true;
		}

		if(timeStart == true){

			timer--;
			fadeIn();
			umbrellaGame.color = fading;
		}

		if(timer < 0){

			Debug.Log ("Should fly");
			fadeOut();
			starting = true;
		}
		if(starting == true){

			umbrella.AddForce(2,5,0 * softly);

		}


	}

	void fadeIn(){

		fading.a = Mathf.Lerp (fading.a, 1, Time.fixedDeltaTime * Speed);

	}
	void fadeOut(){

		fading.a = Mathf.Lerp (fading.a, -1, Time.fixedDeltaTime * Speed);
	}
}


//	public void LoadLevel ()
//	{ //USed by the buttons onCLick function to change the level
//		if (!nextLevel) { //To stop you calling the function multiple times
//			Invoke ("whichLevel", changeTime); //delays the loading of Level-1
//			nextLevel = true;
//		}

//}

//	void whichLevel ()
//	{
//		Application.LoadLevel (1); //Changes to the next scene
//	}

// Lerp colour thing
// 


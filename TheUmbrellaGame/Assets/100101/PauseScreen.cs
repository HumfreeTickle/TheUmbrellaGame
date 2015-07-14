using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{


	private float Timer;
	private GameObject Greyness;
	public bool isPaused;
	private Image GreyFade;
	public float speed;
	private Image Screen;
	private Color fader;
	public float SlowDown;

	void Start ()
	{

		Timer = 200f;//this is the timer to pause the game if no input is detected
		isPaused = false;
		GreyFade = GetComponent<Image> ();
		fader = GetComponent<Image> ().color;
		fader.a = 0;
		Time.timeScale = 0f;
	}
	
	void Update ()
	{
		if (!isPaused) {
			
			Time.timeScale = 1f;//runs at regular time
			FadeOut ();
			Debug.Log ("NotPaused");
			GreyFade.color = fader;
			
		}
		
		if (isPaused) {
			
			Time.timeScale = 0f;
			FadeIn ();
			Debug.Log ("Paused");
			GreyFade.color = fader;
			
			
			if (Input.GetKeyDown (KeyCode.L)) {
				
				Application.LoadLevel ("Start_Screen");
			}
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel ("Boucing");
			}
			
			
			
		}
		Debug.Log (Time.timeScale);

		if (Input.GetKeyDown (KeyCode.B)) {
			
			isPaused = !isPaused;//!isPaused;//toggle between the 2 pause statements
			Debug.Log ("ItsSwitching");

		}



	}

	void FixedUpdate ()
	{

		if (!Input.anyKey) {
			
			Timer --;
			Debug.Log ("NothingHappening");
			
		}
		
		if (Timer == 0f) {
			
			isPaused = true;//when the timer reaches 0 then the pause screen will activate
		}
		
		if (Input.anyKey) {
			
			Timer = 200f;//once a key is pressed the timer should revert back to 500
		}
    
	}

	void FadeIn ()
	{

		fader.a = Mathf.Lerp (fader.a, 1, Time.fixedDeltaTime * SlowDown);

	}

	void FadeOut ()
	{

		fader.a = Mathf.Lerp (fader.a, 0, Time.fixedDeltaTime * SlowDown);//this should lerp between the opacity values of the pause screen the a.value

	}

}

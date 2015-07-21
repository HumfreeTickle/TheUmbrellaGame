using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class GmaeManage : MonoBehaviour
{
	
	private bool pauseGame;
	private bool gameOver;
	private GameObject cameraController;
	private float _gameOverTimer;
	private float _charge;

	//Pause script stuff
	public float autoPauseTimer;
	private float _timer;
	private GameObject Greyness;
	public bool isPaused;
	private Image GreyFade;
	public float speed;
	private Image Screen;
	private Color fader;
	public float SlowDown;

	//Start script Stuff
	private bool nextLevel; // has the transtion to Level-1 been activated
	public float changeTime = 1.0f;
	public float timer;
	public bool starting;
	private Image umbrellaGame;

	//---------------------
	private Color fading; // needs to be made into a parameter
	//---------------------

	public float Speed;
	public bool timeStart;
	public GameObject brolly;
	public float thrust;
	private Rigidbody umbrella;
	public float softly;
	

//------------------------------------ Getters and Setters ------------------------------------------------------------

	public float Timer { //timer used elsewhere to end the game
		get {
			return _gameOverTimer;
		}
	}

	public float Charge {
		get {
			return _charge;
		}
		//set
	}


	// Might need a check to see what scene the game is in 
	// Just for the start function more then anything else

	// Gonna need to add a load of getters so other functions can get access to various variables

//-------------------------------------- All the calls -----------------------------------------------------------------


	void Update ()
	{
		gameOver = GetComponent<CameraControl> ().DeadDead;
		RestartGame ();
//		PauseGame ();
		EndGame ();
	}

//-------------------------------------- What controller is being used -----------------------------------------------------------------
	void PS3Controller ()
	{
		//checks to see if there is a ps3 controller attached
		//changes the controls accordingly
	}

	void KeyboardController ()
	{
		//checks to see if there is only a keyboard
		//changes the controls accordingly
	}

//-------------------------------------- Start Game is elsewhere for some reason -----------------------------------------------------------------


	void StartGame ()
	{
		if (Application.loadedLevel == 0) {
			startScreen (); //called during the startscreen only
		} else if (Application.loadedLevel == 1) {
			playScreen (); //called during the mainscreen only
		}
		//Fades in from white
	}

	void startScreen ()
	{

		// just wondering if these should be booleans that affect other areas of the game rather then have it all in one
		// could use the Application.loadedLevel to determine what should be used and when
		FlyUmbrellaFly ();
	}

	void playScreen ()
	{

	}
	
	void FlyUmbrellaFly ()
	{
		umbrella.AddForce (2, 5, 0 * softly);
	}


	// public functions that can be assigned to and called from any other script
	// the WhiteOut script contains very similar funcitons so would be easier to just combine them

	void fadeIn (/*needs a parameter which would be the image that needs to be faded*/)
	{
		
		fading.a = Mathf.Lerp (fading.a, 1, Time.fixedDeltaTime * Speed);
		
	}

	void fadeOut (/*needs a parameter which would be the image that needs to be faded*/)
	{
		
		fading.a = Mathf.Lerp (fading.a, -1, Time.fixedDeltaTime * Speed);
	}


//-------------------------------------- Ending the game is here (sort of) -----------------------------------------------------------------
	
	void EndGame ()
	{
		if (gameOver) {
			_gameOverTimer += Time.deltaTime;
		}
	}

//-------------------------------------- Not used -----------------------------------------------------------------
	
	void PauseGame ()
	{
		if (Input.GetButtonDown ("Submit")) {
			isPaused = !isPaused;
			//toggle between the 2 pause statements
		}
			
		if (!isPaused) {
			NotPaused ();
		} else if (isPaused) {
			Paused ();
		}
			
		if (Time.timeScale == 0) {
			// calls a function once time has stopped
		}
	}

	void Paused ()
	{
		Time.timeScale = 0;
		
		GetComponent<Grayscale> ().enabled = true;
		GetComponent<BlurOptimized> ().enabled = true;
		
		FadeIn ();
		GreyFade.color = fader;
		
		
		if (Input.GetKeyDown (KeyCode.L)) {
			
			Application.LoadLevel ("Start_Screen");
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel ("Boucing");
		}
	}
	
	void NotPaused ()
	{
		Time.timeScale = 1f;//runs at regular time
		GetComponent<Grayscale> ().enabled = false;
		GetComponent<BlurOptimized> ().enabled = false;
		
		FadeOut ();
		GreyFade.color = fader;
	}

	void FixedPause ()
	{
		if (!Input.anyKey) {
			_timer -= Time.fixedDeltaTime;
		}
				
		if (_timer <= 0) {
					
			isPaused = true;//when the timer reaches 0 then the pause screen will activate
		}
				
		if (Input.anyKey) {
					
			_timer = autoPauseTimer;//once a key is pressed the timer should revert back to 500
		}
	}

	//---------------------------- Image Fades -----------------------------------------------------------------------
	
	
	void FadeIn ()
	{
		fader.a = Mathf.Lerp (fader.a, 1, Time.fixedDeltaTime * SlowDown);
	}
	
	void FadeOut ()
	{
		fader.a = Mathf.Lerp (fader.a, 0, Time.fixedDeltaTime * SlowDown);//this should lerp between the opacity values of the pause screen the a.value
		
	}
	


//-------------------------------------- Resets the game -----------------------------------------------------------------
	
	void RestartGame ()
	{
		if (Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel ("Boucing");
		}
	}
}


////----------------------------------------------- White Out Stuff ------------------------------------------
/// 
//private Color whiteIN;
//public float speed = 0.5f;
//public float _timer;
//private Image _image;
//private GameObject gmeaOVer;
//
//void Start ()
//{
//	whiteIN = GetComponent<Image> ().color;
//	whiteIN = Color.white;
//	_image = GetComponent<Image> ();
//	gmeaOVer = GameObject.Find("Follow Camera");
//}
//
//void Update ()
//{
//	startWhite ();
//	_image.color = whiteIN;
//	if(gmeaOVer.GetComponent<GmaeManage>().Timer > 2){
//		endWhite();
//	}
//}
//
//void startWhite ()
//{
//	whiteIN.a = Mathf.Lerp (whiteIN.a, 0, Time.deltaTime * speed);
//}
//
//void endWhite ()
//{
//	whiteIN.a = Mathf.Lerp (whiteIN.a, 2, Time.deltaTime * speed);
//	if (whiteIN.a >= 0.95) {
//		if (Application.loadedLevel == 0) {
//			Application.LoadLevel ("Boucing");
//		}
//		else if(Application.loadedLevel == 1){
//			Application.LoadLevel ("Start_Screen");
//		}
//	}
//}
//} 

////----------------------------------------------- Other Funcitons ------------------------------------------




using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using Inheritence;

public enum GameState // sets what game state is currently being viewed
{
	NullState,
	Intro,
	Game
}

public class GmaeManage : MonoBehaviour
{

	// Doesn't allow another instance of GmaeManage to be used within the scene --- http://rusticode.com/2013/12/11/creating-game-manager-using-state-machine-and-singleton-pattern-in-unity3d/
	protected GmaeManage ()
	{
		//not sure why this is empty
	}

	private static GmaeManage _instance = null;
	
	// Singleton pattern implementation
	public static GmaeManage Instance { 
		get {
			if (GmaeManage._instance == null) {
				GmaeManage._instance = new GmaeManage (); 
			}  
			return GmaeManage._instance;
		} 
	}

	public GameState gameState { get; private set; }


//------------------------------------------- Inherited Classes ------------------------------------//

	public FadeScript fading = new FadeScript ();

//--------------------------------------------------------------------------------------------------//


	private GameObject cameraController; // 
	private bool gameOver; // is the game over
	private bool isPaused; // is the game paused or not 

	public float autoPauseTimer; // idle timer till game auto pauses
	public float transitionSpeed; // speed of transitions
	
	private float _gameOverTimer; // 
	private float _charge; // the umbrella's energy charge

	public Image PauseScreen; // pause screen image
	
	//Start script Stuff
	private bool nextLevel; // has the transtion to Level-1 been activated
	private Image umbrellaGame;
	public GameObject brolly;
	private Rigidbody umbrella;
	public float softly;

//------------------------------------ Getters and Setters ------------------------------------------------------------

	//Needs to be renamed to gameOverTimer
	public float Timer { //timer used elsewhere to end the game
		get {
			return _gameOverTimer;
		}
	}

	//Needs to be renamed to UmbrellaCharge or WindCharge
	public float Charge {
		get {
			return _charge;
		}
		//set
	}

	// Might need a check to see what scene the game is in 
	// Just for the start function more then anything else

	// Gonna need to add a load of getters so other functions can get access to various variables

//-------------------------------------- The Setup -----------------------------------------------------------------

	void Start ()
	{
		if (Input.GetJoystickNames () [0] != null) {// checks to see if a controller is connected
			PS3Controller ();
		} else if (Input.GetJoystickNames () [0] == null) {
			KeyboardController ();
		}
	}

//-------------------------------------- All the calls -----------------------------------------------------------------

	void Update ()
	{
		gameOver = GetComponent<CameraControl> ().DeadDead;
		RestartGame ();
		PauseGame ();
		EndGame ();
	}

//-------------------------------------- What controller is being used -----------------------------------------------------------------
	void PS3Controller ()
	{
		print ("PS3 Controller");
		//checks to see if there is a ps3 controller attached
		//changes the controls accordingly
	}

	void KeyboardController ()
	{
		print ("Keyboard");
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
			GetComponent<Grayscale> ().enabled = isPaused;
			GetComponent<BlurOptimized> ().enabled = isPaused;
		}
			
		if (!isPaused) {
			NotPaused ();
		} else if (isPaused) {
			Paused ();
		}

		FixedPause ();	
	}

	void Paused ()
	{
		Time.timeScale = 0; //game paused
		fading.Fades (PauseScreen, 1, transitionSpeed);
//		fading.FadeIn(0, PauseScreen);

	}
	
	void NotPaused ()
	{
		Time.timeScale = 1f; //runs at regular time
		fading.Fades (PauseScreen, 0, transitionSpeed);
	}

	void FixedPause ()
	{
		if (!Input.anyKey) {
			autoPauseTimer += Time.deltaTime;

			if (autoPauseTimer >= 60) {
				isPaused = true;//when the timer reaches 0 then the pause screen will activate
			}

		} else if (Input.anyKey || Mathf.Abs (Input.GetAxis ("Horizontal_L")) > 0 || Mathf.Abs (Input.GetAxis ("Vertical_L")) > 0) {
			autoPauseTimer = 0;//once a key is pressed the timer should revert back to 0
		}
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




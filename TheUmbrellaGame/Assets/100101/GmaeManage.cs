using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using Inheritence;
using CameraScripts;

public enum GameState // sets what game state is currently being viewed
{
	NullState,
	Intro,
	Pause,
	Game,
	GameOver
}

public enum ControllerType
{
	NullState,
	Keyboard,
	ConsoleContoller
}

public class GmaeManage : MonoBehaviour
{

	// Doesn't allow another instance of GmaeManage to be used within the scene --- http://rusticode.com/2013/12/11/creating-game-manager-using-state-machine-and-singleton-pattern-in-unity3d/

//	protected GmaeManage ()
//	{
//		//not sure why this is empty
//	}
//
//	private static GmaeManage _instance = null;
//	
//	// Singleton pattern implementation
//	public static GmaeManage Instance { 
//		get {
//			if (GmaeManage._instance == null) {
//				GmaeManage._instance = new GmaeManage (); 
//			}  
//			return GmaeManage._instance;
//		} 
//	}



//------------------------------------------- Inherited Classes ------------------------------------//

	public FadeScript fading = new FadeScript ();

//--------------------------------------------------------------------------------------------------//


	private GameObject cameraController; // 

	private Image PauseScreen; // pause screen image
	private Image WhiteScreen;
	private Image umbrellaGame;
	private Image startButton;
	private GameObject umbrella;
	private Rigidbody umbrellaRb;
	public float autoPauseTimer; // idle timer till game auto pauses
	public float transitionSpeed; // speed of transitions
	public float _gameOverTimer; // 

	private float _charge; // the umbrella's energy charge

	//Start script Stuff
	private bool nextLevel; // has the transtion to Level-1 been activated
	public bool timeStart;
	

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


	//------------------------------------------ State Checks ------------------------------------------//
	
	public GameState gameState { get; set; }

	private GameState currentState = GameState.NullState;
	
	public ControllerType controllerType { get; set; }

	private ControllerType currentController = ControllerType.NullState;
	
	//--------------------------------------------------------------------------------------------------//

//-------------------------------------- The Setup -----------------------------------------------------------------

	void Start ()
	{
		//-------------------- For the different controllers ---------------------------------

		if (Input.GetJoystickNames () [0] != null) {// checks to see if a controller is connected
			controllerType = ControllerType.ConsoleContoller;
		} else if (Input.GetJoystickNames () [0] == null) {
			controllerType = ControllerType.Keyboard;
		}

		//-------------------- For the different Scenes ---------------------------------

		if (Application.loadedLevel == 0) {
			gameState = GameState.Intro; //called during the startscreen only
			startButton = GameObject.Find ("Start Button").GetComponent<Image> ();
			umbrellaGame = GameObject.Find ("Umbrella Logo").GetComponent<Image> ();
			WhiteScreen = GameObject.Find ("WhiteScreen").GetComponent<Image> ();

			umbrella = GameObject.Find ("Umbrella");
			umbrellaRb = umbrella.GetComponent<Rigidbody> ();

			if (!startButton || !umbrellaGame || !umbrella || !WhiteScreen) {
				return;
			}

		} else if (Application.loadedLevel == 1) {
			gameState = GameState.Intro; //called during the mainscreen only
			PauseScreen = GameObject.Find ("Pause Screen").GetComponent<Image> ();
			WhiteScreen = GameObject.Find ("WhiteScreen").GetComponent<Image> ();
			WhiteScreen.color = Color.white;

			if (!PauseScreen || !WhiteScreen) {
				return;
			}
		}
	}

//-------------------------------------- All the calls -----------------------------------------------------------------

	void Update ()
	{
		if (gameState == GameState.Intro) {
			StartGame ();

		} else if (gameState != GameState.Intro) {//gameState == GameState.Game || gameState == GameState.Pause || gameState == GameState.GameOver ) {

			RestartGame ();
			PauseGame ();
			EndGame ();
		}


		CheckStates ();
	}

//-------------------------------------- Start Game is elsewhere for some reason -----------------------------------------------------------------
	
	void StartGame ()
	{
		if (Application.loadedLevel == 0) {
			if (Input.GetButtonDown ("Submit")) {
				if (!startButton) {
					return;
				}
				
				timeStart = true;
				startButton.GetComponent<Animator> ().enabled = false;
			}
			
			if (timeStart) {
				fading.FadeINandOUT (umbrellaGame, 1);
				FlyUmbrellaFly ();
				if (umbrellaGame.color.a > 0.9f) {
					WhiteScreenTransisitions ();
				}	
			}

		} else if (Application.loadedLevel == 1) {
			if (transform.position.y < 200) {
				gameState = GameState.Game;
			}

			WhiteScreenTransisitions ();
		}
	}

//-------------------------------------- Resets the game -----------------------------------------------------------------
	
	void RestartGame ()
	{
		if (Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel ("Boucing");
		}
	}

//-------------------------------------- Pauses the game -----------------------------------------------------------------
	
	void PauseGame ()
	{
		if (Input.GetButtonDown ("Submit")) {
			if (gameState == GameState.Game) {
				gameState = GameState.Pause;

			} else if (gameState == GameState.Pause) {
				gameState = GameState.Game;

			}
		}
		
		if (gameState == GameState.Pause) {
			Paused ();
		} else if (gameState != GameState.Pause) {
			NotPaused ();
		}
		
		FixedPause ();	
	}
	
//-------------------------------------- Ending the game is here (sort of) -----------------------------------------------------------------
	
	void EndGame ()
	{
		if (gameState == GameState.GameOver) {

			_gameOverTimer += Time.deltaTime;
			WhiteScreenTransisitions ();
		}
	}

//-------------------------------------- State Checking ---------------------------------------------------------------------------
	void CheckStates ()
	{
		if (controllerType != currentController) {
			Debug.Log (controllerType);
		}
		
		if (gameState != currentState) {
			Debug.Log (gameState);
		}
		
		currentState = gameState;
		currentController = controllerType;
	}
	
//----------------------------------------------- Other Funcitons ------------------------------------------

	void FlyUmbrellaFly ()
	{
		umbrellaRb.AddForce (2, 5, 0);
	}


	//------------------------------------- Pause State Calls ------------------------------------------------------------

	// using fixed Delta Time is not a good solution - Fabrizio


	void Paused ()
	{
		GetComponent<Grayscale> ().enabled = true;
		GetComponent<BlurOptimized> ().enabled = true;
		
		Time.timeScale = 0; //game paused
		fading.FadeIN (PauseScreen, transitionSpeed);
	}
	
	void NotPaused ()
	{
		GetComponent<Grayscale> ().enabled = false;
		GetComponent<BlurOptimized> ().enabled = false;
		
		Time.timeScale = 1f; //runs at regular time
		fading.FadeOUT (PauseScreen, transitionSpeed);
	}
	
	void FixedPause ()
	{
		if (!Input.anyKey) {
			autoPauseTimer += Time.deltaTime;
			
			if (autoPauseTimer >= 60) {
				gameState = GameState.Pause;//when the timer reaches 0 then the pause screen will activate
			}
			
		} else if (Input.anyKey || Mathf.Abs (Input.GetAxis ("Horizontal_L")) > 0 || Mathf.Abs (Input.GetAxis ("Vertical_L")) > 0) {
			autoPauseTimer = 0;//once a key is pressed the timer should revert back to 0
		}
	}
	
	
	
	//-------------------------------------- White Screen Stuff -----------------------------------------------------------------
	void WhiteScreenTransisitions ()
	{
		if (gameState == GameState.Intro) {
			fading.FadeOUT (WhiteScreen, 3);
			
		} else if (gameState == GameState.GameOver) {
			if (_gameOverTimer > 2) {
				fading.FadeIN (WhiteScreen, 1);

				if (WhiteScreen.color.a >= 0.95) {
					if (Application.loadedLevel == 0) {
						Application.LoadLevel ("Boucing");
					} else if (Application.loadedLevel == 1) {
						Application.LoadLevel ("Start_Screen");
					}
				}
			}
		}
	}


}//End of Class


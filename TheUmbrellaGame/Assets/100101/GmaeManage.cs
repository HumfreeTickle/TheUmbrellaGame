
﻿using UnityEngine;
using System.Collections;

public class GmaeManage : MonoBehaviour
{
	
	private bool pauseGame;
	private bool gameOver;
	private GameObject cameraController;
	private float _gameOverTimer;

	public float Timer{ //timer used elsewhere to end the game
		get{
			return _gameOverTimer;
		}
	}

//-------------------------------------- All the calls -----------------------------------------------------------------


	void Update ()
	{
		gameOver = GetComponent<cameraControl>().DeadDead;
		RestartGame ();
//		PauseGame ();
		EndGame ();
	}

//-------------------------------------- Start Game is elsewhere for some reason -----------------------------------------------------------------


	void StartGame ()
	{
		//Fades in from white
	}

//-------------------------------------- Ending the game is here (sort of) -----------------------------------------------------------------
	
	void EndGame ()
	{
		if(gameOver){
			_gameOverTimer += Time.deltaTime;
		}
	}

//-------------------------------------- Not used -----------------------------------------------------------------
	
	void PauseGame ()
	{
		if (Input.GetButtonDown ("Submit")) {
			pauseGame = !pauseGame;
		}
		if (pauseGame) {
			if (Time.timeScale > 0.1f) {
				Time.timeScale -= Time.fixedDeltaTime*2;
			}else{
				Time.timeScale = 0;
			}
		} else {
			Time.timeScale = 1;
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



﻿using UnityEngine;
using System.Collections;

public class GmaeManage : MonoBehaviour
{
	
	private bool pauseGame;
	private bool gameOver;
	private GameObject cameraController;
	private float _gameOverTimer;

	public float Timer{
		get{
			return _gameOverTimer;
		}
	}

	void Update ()
	{
		gameOver = GetComponent<cameraControl>().DeadDead;
		RestartGame ();
		PauseGame ();
		EndGame ();
	}
	
	void StartGame ()
	{
		//Fades in from white
	}
	
	void EndGame ()
	{
		if(gameOver){
			_gameOverTimer += Time.deltaTime;
		}

	}

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
	
	void RestartGame ()
	{
		if (Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel ("Boucing");
		}

	}
}

//=======
﻿//using UnityEngine;
//using System.Collections;
//
//public class GmaeManage : MonoBehaviour
//{
//	
//	private bool pauseGame;
//	private bool gameOver;
//	private GameObject cameraController;
//	private float _gameOverTimer;
//
//	public float Timer{
//		get{
//			return _gameOverTimer;
//		}
//	}
//
//	void Update ()
//	{
//		gameOver = GetComponent<cameraControl>().DeadDead;
//		GameRestart ();
//		PauseGame ();
//		EndGame ();
//	}
//	
//	void StartGame ()
//	{
//		//Fades in from white
//	}
//	
//	void EndGame ()
//	{
//		if(gameOver){
//			_gameOverTimer += Time.deltaTime;
//		}
//
//	}
//
//	void PauseGame ()
//	{
//		if (Input.GetButtonDown ("Submit")) {
//			pauseGame = !pauseGame;
//		}
//		if (pauseGame) {
//			if (Time.timeScale > 0.1f) {
//				Time.timeScale -= Time.fixedDeltaTime*2;
//			}else{
//				Time.timeScale = 0;
//			}
//		} else {
//			Time.timeScale = 1;
//		}
//	}
//	
//
//	void RestartGame(){
////		if(Input.GetKeyDown(KeyCode.R)){
////			Application.LoadLevel ("Boucing");
//		}
//
//	void GameRestart ()
//	{
//		if (Input.GetKeyDown (KeyCode.R)) {
//			Application.LoadLevel ("Boucing");
//		}
//
//	}
//}

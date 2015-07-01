using UnityEngine;
using System.Collections;

public class GmaeManage : MonoBehaviour
{
	
	private bool pauseGame;
	
	void Update ()
	{
		RestartGame ();
		PauseGame ();
	}
	
	void StartGame ()
	{
		//Fades in from white
	}
	
	void EndGame ()
	{
		//Fades out to white
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
		//Fades to white
		//Call StartGame()
	}
}

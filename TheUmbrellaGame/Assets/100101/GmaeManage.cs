using UnityEngine;
using System.Collections;

public class GmaeManage : MonoBehaviour {

	
	//Adds a timer to the game to end gameplay after 2 minutes (When the kitten has been completed)
	//Restarting game with a series of keys
	
	
	void Update () {
		RestartGame();
	}
	
	void StartGame(){
		//Fades in from white
	}
	
	void EndGame(){
		//Fades out to white
	}
	
	void RestartGame(){
		if(Input.GetKeyDown(KeyCode.R)){
			Application.LoadLevel ("Boucing");
		}
		//Fades to white
		//Call StartGame()
	}
}

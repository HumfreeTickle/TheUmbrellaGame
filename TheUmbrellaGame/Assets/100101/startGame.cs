using UnityEngine;
using System.Collections;

public class startGame : MonoBehaviour {

	private bool nextLevel; // has the transtion to Level-1 been activated
	public float changeTime = 1.0f;

	public void LoadLevel ()
	{ //USed by the buttons onCLick function to change the level
		if (!nextLevel) { //To stop you calling the function multiple times
			Invoke ("whichLevel", changeTime); //delays the loading of Level-1
			nextLevel = true;
		}
		
	}

	void whichLevel ()
	{
		Application.LoadLevel (1); //Changes to the next scene
	}
}

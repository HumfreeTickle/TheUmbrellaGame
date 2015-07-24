using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//--------------------------------------- ANDREW'S CODE ------------------------------------------------//


public class FadeScript
{

//---------------------------- Image Fades -----------------------------------------------------------------------
	public void Fades(Image myImage, float fadeTo, float speed){
		Debug.Log ("called");
		Color alphaColour = myImage.color;
		alphaColour.a = Mathf.Lerp(alphaColour.a, fadeTo, Time.fixedDeltaTime * speed);
		myImage.color = alphaColour;
	}
	
//	public IEnumerator FadeIn (float startDelay, Image myImage)
//	{
////		yield return new WaitForSeconds (startDelay);
//		myImage.enabled = true;
//		Color placeholderColor = myImage.color;
//		placeholderColor.a = 0.01f;
//					
//		while (placeholderColor.a < 0.98f) {
//
//			placeholderColor.a = Mathf.Lerp (placeholderColor.a, 1f, Time.fixedDeltaTime);
//			myImage.color = placeholderColor;
//			yield return null;
//		}
//	}
//		
//	public IEnumerator FadeOut (float startDelay, Image myImage)
//	{
//		yield return new WaitForSeconds (startDelay);
//			
//		Color placeholderColor = myImage.color;
//		placeholderColor.a = 1f;
//
//			
//		while (placeholderColor.a > 0.02f) {
//			placeholderColor.a = Mathf.Lerp (placeholderColor.a, 0f, Time.fixedDeltaTime);
//			myImage.color = placeholderColor;
//			yield return null;
//		}
//		myImage.enabled = false;
//	}
}


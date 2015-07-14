using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class whiteOut : MonoBehaviour
{
	private Color whiteIN;
	public float speed = 0.5f;
	public float _timer;
	private Image _image;
//	private GameObject gmeaOVer;

	void Start ()
	{
		whiteIN = GetComponent<Image> ().color;
		whiteIN = Color.white;
		_image = GetComponent<Image> ();
//		gmeaOVer = GameObject.Find("Follow Camera");
	}
	
	void Update ()
	{
		startWhite ();
		_image.color = whiteIN;
//		if(gmeaOVer.GetComponent<GmaeManage>().Timer > 2){
//			endWhite();
//		}
	}

	void startWhite ()
	{
		whiteIN.a = Mathf.Lerp (whiteIN.a, 0, Time.deltaTime * speed);
	}

	void endWhite ()
	{
		whiteIN.a = Mathf.Lerp (whiteIN.a, 2, Time.deltaTime * speed);
		if (whiteIN.a >= 0.95) {
			if (Application.loadedLevel == 0) {
				Application.LoadLevel ("Boucing");
			}
			else if(Application.loadedLevel == 1){
				Application.LoadLevel ("Start_Screen");
			}
		}
	}
}

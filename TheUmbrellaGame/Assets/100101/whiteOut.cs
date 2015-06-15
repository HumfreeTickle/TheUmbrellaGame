using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class whiteOut : MonoBehaviour
{
	public Color whiteIN;
	public float speed = 0.5f;
	public float _timer;
	private Image _image;

	// Use this for initialization
	void Start ()
	{
		whiteIN = GetComponent<Image> ().color;
		whiteIN = Color.white;
		_image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		startWhite ();
		_timer += Time.deltaTime;
		if (_timer >= 360) {
			endWhite ();
		}
		_image.color = whiteIN;
	}

	void startWhite ()
	{
		whiteIN.a = Mathf.Lerp (whiteIN.a, 0, Time.deltaTime * speed);
	}

	void endWhite ()
	{
		whiteIN.a = Mathf.Lerp (whiteIN.a, 5, Time.deltaTime * speed);
		if (whiteIN.a >= 0.95) {
			Application.LoadLevel ("Boucing");
		}
	}
}

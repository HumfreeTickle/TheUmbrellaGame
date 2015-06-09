using UnityEngine;
using System.Collections;

public class DestroyParticles : MonoBehaviour {

	public float timer;
	// Update is called once per frame
	void Update () {
		Invoke("RemovePArticles", timer);
	}
	void RemovePArticles(){
		Destroy(gameObject);
	}
}

using UnityEngine;
using System.Collections;

public class MousePointer : MonoBehaviour {

	private Ray ray;
	public GameObject particles;

	void Update () {
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit)){
			Instantiate(particles, hit.point , Quaternion.Euler(new Vector3(180, 0, 0)));
		}

	}
}

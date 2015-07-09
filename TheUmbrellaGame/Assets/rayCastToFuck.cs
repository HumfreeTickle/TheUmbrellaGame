using UnityEngine;
using System.Collections;

public class rayCastToFuck : MonoBehaviour
{
	public GameObject hitPoint;
	public float chargeDistance;

	void Update ()
	{
		Vector3 downRayDown = Vector3.down;
		RaycastHit hit;
		
		if (Physics.Raycast (transform.position, downRayDown, out hit, Mathf.Clamp(chargeDistance, 1, Mathf.Infinity))) {
			if(hit.collider.tag == "Terrain"){
				Instantiate(hitPoint, hit.point, Quaternion.identity);
				GameObject.Find("main_Sphere") .GetComponent<CreateWind>().charge += chargeDistance;
			}
		}
		

	}
}

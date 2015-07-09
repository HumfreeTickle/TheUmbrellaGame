using UnityEngine;
using System.Collections;

public class rayCastToFuck : MonoBehaviour
{
	public float chargeDistance;
	private Material umbrellaColour;
	private Vector3 baseUmbrella = new Vector3 (0f, -2f, 0f);

	void Start ()
	{
		umbrellaColour = GetComponent<Renderer> ().material;
		chargeDistance = GameObject.Find ("main_Sphere").GetComponent<CreateWind> ().charge; 
		chargeDistance = 100;
	}

	void Update ()
	{
		umbrellaColour.SetColor ("_Color", new Vector4 (chargeDistance / 100, chargeDistance / 100, chargeDistance / 100, 1));
		chargeDistance = GameObject.Find ("main_Sphere").GetComponent<CreateWind> ().charge; 
		Vector3 downRayDown = Vector3.down;
		RaycastHit hit;
		
		if (Physics.Raycast (transform.position + baseUmbrella , downRayDown, out hit, Mathf.Infinity)) {
			Debug.DrawRay(transform.position, downRayDown, Color.green, Mathf.Infinity, false);
			if (hit.collider.tag == "Terrain") {
				print (hit.distance);
				chargeDistance = Mathf.Lerp (chargeDistance, 100, Time.time/(hit.distance*100));
				GameObject.Find ("main_Sphere").GetComponent<CreateWind> ().charge = chargeDistance;
			}
		}	
	}
}

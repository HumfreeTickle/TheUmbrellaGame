using UnityEngine;
using System.Collections;

public class rayCastToFuck : MonoBehaviour
{
	public float chargeDistance;
	public float maxTerrainDistance = 1.0f;
	private Material umbrellaColour;
	private Vector3 baseUmbrella = new Vector3 (0f, -2f, 0f);
	private GameObject sphere;
	private LayerMask ignoremask;


	void Start ()
	{
		ignoremask = LayerMask.NameToLayer("Player");
		umbrellaColour = GetComponent<Renderer> ().material;
		sphere = GameObject.Find ("main_Sphere");
		chargeDistance = sphere.GetComponent<CreateWind> ().charge; 
//		chargeDistance = 100;
	}

	void Update ()
	{
		Color newUmbrellaColour = Vector4.Lerp(umbrellaColour.color,  new Vector4 (chargeDistance / 100, chargeDistance / 100, chargeDistance / 100, 1), Time.deltaTime*(chargeDistance + 1)); 
		umbrellaColour.SetColor ("_Color", newUmbrellaColour);
		chargeDistance = sphere.GetComponent<CreateWind> ().charge; 
		Vector3 downRayDown = Vector3.down;
		RaycastHit hit;

		
		if (Physics.Raycast (transform.position + baseUmbrella , downRayDown, out hit, Mathf.Infinity)) {
			Debug.DrawRay(transform.position, downRayDown, Color.green, Mathf.Infinity, false);
//			print (hit.collider.name);
			if (hit.collider.tag == "Terrain" && hit.distance < maxTerrainDistance) {
//				print (hit.distance);

				chargeDistance = Mathf.Lerp (chargeDistance, 100, Time.time/(hit.distance*100));
				sphere.GetComponent<CreateWind> ().charge = chargeDistance;
			}
		}	
	}
}

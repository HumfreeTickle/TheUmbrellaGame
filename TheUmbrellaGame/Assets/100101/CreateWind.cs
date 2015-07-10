using UnityEngine;
using System.Collections;

public class CreateWind : MonoBehaviour
{

	public GameObject windSystem;
	private GameObject instatiatedWind;
	private Vector3 spawnDistance;
	public float charge = 100;
	public float maxTerrainDistance = 1.0f;
	private Material umbrellaColour;
	private Vector3 baseUmbrella = new Vector3 (0f, -5f, 0f);
	private GameObject umbrella;
	private GameObject cameraControl;

	void Start ()
	{
		cameraControl = GameObject.Find("Follow Camera");
		umbrella = GameObject.Find ("Umbrella");
		umbrellaColour = umbrella.GetComponent<Renderer> ().material;
	}

	void Update ()
	{

		if (Input.GetButtonDown ("CrateWind") && charge >= 1) {
			SummonWind ();
			charge = Mathf.Lerp (charge, 0, Time.fixedDeltaTime * 10);
		}

		//---------------- TURN OFF UPWARDFORCE ---------------------
		if (charge <= 1) {
			GetComponent<upwardForce> ().enabled = false;

		} else {
			GetComponent<upwardForce> ().enabled = true;
		}

//---------------------------- COLOUR CHANGING ------------------------------------------------------------------------

		Color newUmbrellaColour = Vector4.Lerp (umbrellaColour.color, new Vector4 (charge / 100, charge / 100, charge / 100, 1), Time.deltaTime * (charge + 1)); 
		umbrellaColour.SetColor ("_Color", newUmbrellaColour);


//---------------------------- RAYCASTING STUFF -----------------------------------------------------------------------

		Vector3 downRayDown = Vector3.down;
		RaycastHit hit;
		
		
		if (Physics.Raycast (transform.position + baseUmbrella, downRayDown, out hit, Mathf.Infinity)) {

			//------------- DEBUGGING -----------------------------
			Debug.DrawRay (transform.position + baseUmbrella, downRayDown, Color.green, Mathf.Infinity, false);
//			print (hit.collider);
//			print ("Distance: " + hit.distance);

			//------------- CONDITIONS ----------------------------
			if (hit.collider.tag == "Terrain" && hit.distance < maxTerrainDistance) {
				charge = Mathf.Lerp (charge, 100, Time.time / (hit.distance * 100));
			}

		} else {
			charge = Mathf.Lerp (charge, 0, Time.deltaTime);
			if(charge <= 1){
				cameraControl.GetComponent<cameraControl>().DeadDead = true;
			}
//			GetComponent<Rigidbody> ().drag = Mathf.Lerp (GetComponent<Rigidbody> ().drag, 20, Time.fixedDeltaTime / 10);
		}
	}

//----------------------------- OTHER FUNCTIONS ------------------------------------------------------------------------
	

	void SummonWind ()
	{

		//-------------------- CREATING THE WIND ----------------------------------
		spawnDistance = transform.position - new Vector3 (0, 10, 0);
		
		instatiatedWind = Instantiate (windSystem, spawnDistance, Quaternion.Euler (Vector3.forward)) as GameObject;
		instatiatedWind.transform.parent = this.transform; 
		instatiatedWind.GetComponent<ParticleSystem> ().enableEmission = true;
		instatiatedWind.GetComponent<wind> ().windForce = charge * 10;

		//--------------------	TURNS OFF PARTICLES AFTER ONE CYCLE -----------------
		if (Input.GetButtonUp ("CrateWind")) {
			instatiatedWind.GetComponent<ParticleSystem> ().enableEmission = false;
		}
	}
}

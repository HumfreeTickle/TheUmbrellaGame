using UnityEngine;
using System.Collections;

public class NPC_Movements : MonoBehaviour {

	private NavMeshAgent npcNavMeshAgent;
	public Transform waypoint1;
	public Transform waypoint2;
	private Transform destination;

	// Use this for initialization
	void Start () {
		npcNavMeshAgent = GetComponent<NavMeshAgent>();
		destination = waypoint1;
	}
	
	// Update is called once per frame
	void Update () {
//		Invoke ("Destination", 5f);
		Movement(destination);
	}

	void Movement(Transform waypoint){
		npcNavMeshAgent.SetDestination(waypoint.position);
		transform.LookAt(waypoint.position);
		npcNavMeshAgent.stoppingDistance = 5f;
		if(npcNavMeshAgent.remainingDistance <= 5f){
			print ("Stopped");
		}
	}

	void Destination(){
		if(destination == waypoint1){
			destination = waypoint2;
		}else{
			destination = waypoint1;
		}
	}
}

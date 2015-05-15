using UnityEngine;
using System.Collections;

public class TestQuaternions : MonoBehaviour {

	private Quaternion P = new Quaternion (4, 4, 4, 4);
	private Quaternion Q = new Quaternion (2, 2, 2, 2);
	// Use this for initialization
	void Start () {
		Quaternion PQ = P * Q;
		Quaternion QP = Q * P;
		print (PQ);
		print (QP);
	}
}

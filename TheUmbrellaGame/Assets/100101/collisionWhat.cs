﻿using UnityEngine;
using System.Collections;

//probably stil useful for testing and what not

public class collisionWhat : MonoBehaviour {

//---------------------------------------- For When Shit Hits Other Shit --------------------------------------//

	void OnCollisionEnter(Collision col){
		print (col.gameObject);
	}
}

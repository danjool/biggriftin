using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropOffScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D( Collider2D other ){
		
		if (other.gameObject.tag == "stealable") {
			//we just dropped off a stealabe!

			//make the hand that held that object open again
			other.gameObject.GetComponentInParent<hands>().holding = false;
			//set the mass of the hand back to that of its baseMass
			other.gameObject.GetComponentInParent<hands> ().resetMass ();

			//attacht the stoled object to the dropoff
			other.gameObject.transform.parent = this.transform;

			//let the static total know:  
			ApplicationModel.totalValueStole += other.gameObject.GetComponent<stealable>().Value;


		}
	}
}

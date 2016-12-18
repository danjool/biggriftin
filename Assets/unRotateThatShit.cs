using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unRotateThatShit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		this.gameObject.transform.rotation = Quaternion.AngleAxis( 0.0f, Vector3.right);
	}
}

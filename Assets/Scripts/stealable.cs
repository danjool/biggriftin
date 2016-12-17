using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stealable : MonoBehaviour {
	public bool stole;
	public float Mass = 1.0f;
	// Use this for initialization
	void Start () {
		stole = false;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void onCollisionEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "hands") {
			//this.transform.parent = coll.transform.parent;
			//GetComponent<Rigidbody2D>().parent = coll.transform.parent;

		}
	}
}

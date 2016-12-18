using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpen : MonoBehaviour {
	private Vector3 initPos;
	private Vector3 goalPos;
	public bool openDoor;

	public float slideDistance = 1.0f;
	private float dir;
	// Use this for initialization
	void Start () {
		initPos = this.gameObject.transform.position;
		goalPos = initPos + new Vector3 ( slideDistance, 0f, 0f );
		dir = slideDistance / Mathf.Abs (slideDistance);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate(){
		if (dir < 0) {
			if (openDoor) {
				this.gameObject.transform.position = Vector3.Min (initPos + dir * Time.deltaTime * (new Vector3 (0.000001f, 0f, 0f)), goalPos);
			} else {
				this.gameObject.transform.position = Vector3.Max (goalPos + dir * Time.deltaTime * (new Vector3 (-0.001f, 0f, 0f)), initPos);
			}
		} else {
			if (openDoor) {
				this.gameObject.transform.position =  Vector3.Max ( initPos + Time.deltaTime*(new Vector3( 0.001f,0f,0f) ), goalPos );
			} else {
				this.gameObject.transform.position =  Vector3.Min ( goalPos + Time.deltaTime*(new Vector3(-0.001f,0f,0f) ), initPos );
			}
		}

	}

	public void openTheDoor( ){
		openDoor = true;
	}

	public void closeTheDoor( ){
		openDoor = false;

	}
}

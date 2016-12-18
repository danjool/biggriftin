using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorSensor : MonoBehaviour {

	public Sprite open,closed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter2D( Collider2D other )
	{
		//GameObject.FindGameObjectWithTag ("door").GetComponent<doorOpen> ().openTheDoor ();
		foreach (GameObject door in GameObject.FindGameObjectsWithTag("door")) {
			door.GetComponent<doorOpen> ().openTheDoor ();
		}
	}

	void OnTriggerExit2D( Collider2D other )
	{
		//GameObject.FindGameObjectWithTag ("door").GetComponent<doorOpen> ().closeTheDoor ();
		foreach (GameObject door in GameObject.FindGameObjectsWithTag("door")) {
			door.GetComponent<doorOpen> ().closeTheDoor ();
		}
	}
}

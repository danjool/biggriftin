using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StealTextScript : MonoBehaviour {

	private int flashes = 5;
	private float flashtime = 0.5f;
	private float temptime = 0f;
	private Text t;
	// Use this for initialization
	void Start () {
		t = gameObject.GetComponent<Text> ();
		temptime = Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.realtimeSinceStartup - temptime > flashtime)
		{
			t.enabled = !t.enabled;
			temptime = Time.realtimeSinceStartup;
			flashes--;
		}
		if (flashes <= 0)
		{
			GameObject.Destroy (gameObject);
		}
	}
}

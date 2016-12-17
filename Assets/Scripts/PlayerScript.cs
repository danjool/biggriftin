using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	public float baseAccel = 100.0f;
	public float baseMass = 1.0f;
	public float baseFriction = 1.0f;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D> ().mass = baseMass;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate()
	{
		float hInput = Input.GetAxis ("Horizontal");
		float vInput = Input.GetAxis ("Vertical");

		//friction
		Vector2 vel = GetComponent<Rigidbody2D> ().velocity;
		Vector2 vDir = vel.normalized;
		float vMagSq = vel.magnitude * vel.magnitude;
		Vector2 frictionForce = -1.0f * vMagSq * vDir;
		GetComponent<Rigidbody2D>().AddForce( frictionForce );

		//user input, for now just from arrow keys
		Vector2 input = new Vector2( hInput, vInput );
		float accel = baseAccel / GetComponent<Rigidbody2D> ().mass;
		GetComponent<Rigidbody2D>().AddForce(input * accel * Time.deltaTime);

	}
}

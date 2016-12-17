using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	public float baseAccel = 100.0f;
	public float baseMass = 1.0f;
	public float baseFriction = 1.0f;
	public float reachDistance = 1.0f;

	public Vector2 baseRightHand = new Vector2(1.7f, 0.0f);

	private LayerMask stealablesLayer;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D> ().mass = baseMass;
		stealablesLayer =  LayerMask.NameToLayer("stealables");
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

		moveHands ();
	}

	void moveHands(){
		baseRightHand = GetComponent<Rigidbody2D> ().position + new Vector2(0.3f, 0.0f);

		//transform.Find ("HandR").transform.position = baseRightHand + new Vector2( 2.0f*Mathf.Sin(Time.realtimeSinceStartup/1.0f), 0.0f );
		//transform.Find ("HandR").position = baseRightHand + new Vector2( 0.4f*Mathf.Sin(Time.realtimeSinceStartup/1.0f), 0.0f );

		/*
		//find near shit:
		Collider2D[] nearStealables = Physics2D.OverlapCircleAll( baseRightHand, reachDistance, stealablesLayer );

		//find nearest grabbable
		if (nearStealables.Length != 0) {
			Collider2D nearest = nearStealables[0];
			float nearestDistMag = 100.0f;
			Vector2 nearestDir = new Vector2(1.0f, 1.0f);
			Vector2 distVec;
			foreach (Collider2D stealable in nearStealables) {
				distVec = (Vector2)stealable.transform.position - ( (Vector2)GetComponent<Rigidbody2D> ().position );
				float distMag = distVec.magnitude;
				Vector2 distDir = distVec.normalized;
				if (nearestDistMag > distMag) {
					nearest = stealable;
					nearestDir = distDir;
				}
			}

			transform.Find ("HandR").position = baseRightHand + 2.0f*nearestDir;

		} else {
			transform.Find ("HandR").position = baseRightHand;
		}


		//if within range, reach for it
		*/

		/*
		foreach(Collider2D collider in Physics2D.OverlapCircleAll(baseRightHand, reachDistance, stealablesLayer) ){
			Vector2 forceDirection = (Vector2)baseRightHand - (Vector2)collider.transform.position;
			collider.attachedRigidbody.AddForce (forceDirection.normalized * 10.0f * Time.fixedDeltaTime);

		}
		*/

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {
	public float baseAccel = 100.0f;
	public float baseMass = 1.0f;
	public float baseFriction = 1.0f;
	public float reachDistance = 1.0f;
	public float angle = 0.0f;

	public Vector2 baseRightHand = new Vector2(1.7f, 0.0f);

	private LayerMask stealablesLayer;
	private Animator animator;
	//private string animDirection = "walkDown";

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D> ().mass = baseMass;
		stealablesLayer =  LayerMask.NameToLayer("stealables");
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate()
	{
		float hInput = Input.GetAxis ("Horizontal");
		float vInput = Input.GetAxis ("Vertical");

		angle = Mathf.Atan2 ( vInput, hInput ) * Mathf.Rad2Deg;


		if ( Mathf.Abs( 0.0f - angle ) < 45.0f ) animator.SetTrigger ("walkRight");
		if ( Mathf.Abs( 90.0f - angle ) < 45.0f ) animator.SetTrigger ("walkUp");
		if ( Mathf.Abs( 180.0f - Mathf.Abs(angle) ) < 45.0f ) animator.SetTrigger ("walkLeft");
		if ( Mathf.Abs( -90.0f - angle ) < 45.0f ) animator.SetTrigger ("walkDown");

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

		if (Time.timeSinceLevelLoad > 60) {
			SceneManager.LoadScene ("EndSceneGood");
		}
	}

	void moveHands(){
		baseRightHand = GetComponent<Rigidbody2D> ().position + new Vector2(0.3f, 0.0f);

	}

	void OnTriggerEnter2D( Collider2D other ){
		//figure out if the guard hit us
		if (other.gameObject.tag == "guard") {
			Application.LoadLevel ("EndScene");
		}
	}
}

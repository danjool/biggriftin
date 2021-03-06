﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GuardScript : MonoBehaviour {

	public AudioClip alertSound;
	private GameObject player;
	private Vector2 lastKnownLocation;

	private AudioSource speaker;

	public float speed, force, sightRange, sightAngle;

	public GameObject bottomLeft, topRight;
	public GameObject[] patrolRoute;

	private int patrolIndex = 0;

	private Rigidbody2D rb;
	private Animator animator;

	private Vector2 destination;
	[SerializeField]
	private Vector2[] pathToDestination;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("body");
		destination = patrolRoute [patrolIndex].transform.position;
		pathToDestination = BreadthFirstSearch.GetPath (
			new Vector2 (bottomLeft.transform.position.x, bottomLeft.transform.position.y),
			new Vector2 (topRight.transform.position.x, topRight.transform.position.y),
			transform.position,
			destination);

		rb = gameObject.GetComponent<Rigidbody2D> ();

		animator = GetComponentInChildren<Animator> ();

		speaker = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		LayerMask playerAndWalls = LayerMask.GetMask ("Player", "Unpassable");
		if (
			Physics2D.Linecast (transform.position + 0.4f * transform.up, destination, LayerMask.GetMask ("Unpassable")) ||
			Physics2D.Linecast (transform.position - 0.4f * transform.up, destination, LayerMask.GetMask ("Unpassable")) ||
			Physics2D.Linecast (transform.position + 0.4f * transform.right, destination, LayerMask.GetMask ("Unpassable")) ||
			Physics2D.Linecast (transform.position - 0.4f * transform.right, destination, LayerMask.GetMask ("Unpassable")))
		{
			pathToDestination = BreadthFirstSearch.GetPath (
				new Vector2 (bottomLeft.transform.position.x, bottomLeft.transform.position.y),
				new Vector2 (topRight.transform.position.x, topRight.transform.position.y),
				transform.position,
				destination);
		}
		else
		{
			pathToDestination = new Vector2[] { transform.position, destination };
		}
		DrawPath (pathToDestination);

		Debug.DrawRay (transform.position, transform.right, Color.cyan, 0f, false);
		Debug.DrawRay (transform.position, Quaternion.AngleAxis ( sightAngle, Vector3.forward ) * transform.right * sightRange, Color.magenta, 0f, false);
		Debug.DrawRay (transform.position, Quaternion.AngleAxis ( -sightAngle, Vector3.forward ) * transform.right * sightRange, Color.magenta, 0f, false);

		Vector2 playerVector = player.transform.position - transform.position;

		RaycastHit2D playerCast = Physics2D.Raycast (transform.position, playerVector.normalized, sightRange, playerAndWalls);

		if (Vector2.Angle (transform.right, playerVector) < sightAngle)
		{
			if (playerCast.collider == null)
			{
				Debug.DrawRay (transform.position, playerVector.normalized * sightRange, Color.red, 0f, false);
				//destination = patrolRoute [patrolIndex];
				if (((Vector2)transform.position - destination).sqrMagnitude < 0.25f)
				{
					patrolIndex = (patrolIndex + 1) % patrolRoute.Length;
					destination = patrolRoute [patrolIndex].transform.position;
				}
			}
			else if (playerCast.collider.gameObject == player)
			{
				if (destination != lastKnownLocation)
				{
					speaker.PlayOneShot (alertSound);
				}
				Debug.DrawLine (transform.position, playerCast.point, Color.green, 0f, false);
				lastKnownLocation = player.transform.position;
				destination = lastKnownLocation;
			}
			else
			{
				Debug.DrawLine (transform.position, playerCast.point, Color.red, 0f, false);
				//destination = patrolRoute [patrolIndex];
				if (((Vector2)transform.position - destination).sqrMagnitude < 0.25f)
				{
					patrolIndex = (patrolIndex + 1) % patrolRoute.Length;
					destination = patrolRoute [patrolIndex].transform.position;
				}
			}
		}
		else
		{
			if (((Vector2)transform.position - destination).sqrMagnitude < 0.25f)
			{
				patrolIndex = (patrolIndex + 1) % patrolRoute.Length;
				destination = patrolRoute [patrolIndex].transform.position;
			}
		}

		Debug.DrawLine (transform.position, destination, Color.white);

		if (Vector3.Distance( transform.position, player.transform.position)  < 0.7f ) {
			//got too close, catch the player
			Application.LoadLevel ("EndScene");
		}
	}

	void FixedUpdate()
	{
		Vector2 walkDirection = pathToDestination [1] - pathToDestination [0];
		transform.right = Vector3.Slerp(transform.right,walkDirection,0.25f);
		if (Vector2.Dot (transform.right, rb.velocity) < speed)
		{
			rb.AddForce (transform.right * force);
		}
		rb.velocity = rb.velocity - (Vector2)transform.up * Vector2.Dot (transform.up, rb.velocity);

		//determine which animations play based on the walkDirection:
		float angle = Mathf.Atan2 ( walkDirection.y, walkDirection.x) * Mathf.Rad2Deg;


		if ( Mathf.Abs( 0.0f - angle ) < 45.0f ) animator.SetTrigger ("guardWalkRight");
		if ( Mathf.Abs( 90.0f - angle ) < 45.0f ) animator.SetTrigger ("guardWalkUp");
		if ( Mathf.Abs( 180.0f - Mathf.Abs(angle) ) < 45.0f ) animator.SetTrigger ("guardWalkLeft");
		if ( Mathf.Abs( -90.0f - angle ) < 45.0f ) animator.SetTrigger ("guardWalkDown");
	}

	private void DrawPath(Vector2[] path)
	{
		for (int i = 0; i < path.Length - 1; i++)
		{
			Debug.DrawLine (path [i], path [i + 1], Color.yellow);
		}
	}
}

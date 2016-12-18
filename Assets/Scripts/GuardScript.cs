using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GuardScript : MonoBehaviour {

	private GameObject player;
	private Vector2 lastKnownLocation;

	public float speed, force, sightRange, sightAngle;

	public Vector2[] patrolRoute;
	private int patrolIndex = 0;

	private Rigidbody2D rb;

	private Vector2 destination;
	[SerializeField]
	private Vector2[] pathToDestination;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("body");
		destination = patrolRoute [patrolIndex];
		pathToDestination = BreadthFirstSearch.GetPath (
			new Vector2 (-6f, -5f),
			new Vector2 (6f, 6f),
			transform.position,
			destination);

		rb = gameObject.GetComponent<Rigidbody2D> ();
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
				new Vector2 (-6f, -5f),
				new Vector2 (6f, 6f),
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
					destination = patrolRoute [patrolIndex];
				}
			}
			else if (playerCast.collider.gameObject == player)
			{
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
					destination = patrolRoute [patrolIndex];
				}
			}
		}
		else
		{
			if (((Vector2)transform.position - destination).sqrMagnitude < 0.25f)
			{
				patrolIndex = (patrolIndex + 1) % patrolRoute.Length;
				destination = patrolRoute [patrolIndex];
			}
		}

		Debug.DrawLine (transform.position, destination, Color.white);
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
	}

	private void DrawPath(Vector2[] path)
	{
		for (int i = 0; i < path.Length - 1; i++)
		{
			Debug.DrawLine (path [i], path [i + 1], Color.yellow);
		}
	}
}

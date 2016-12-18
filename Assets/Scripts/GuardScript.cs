using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardScript : MonoBehaviour {

	private GameObject player;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		Vector2[] pathToPlayer = BreadthFirstSearch.GetPath (
			                         new Vector2 (-6f, -5f),
			                         new Vector2 (6f, 6f),
			                         transform.position,
			                         player.transform.position);
		DrawPath (pathToPlayer);
		
		Debug.DrawRay (transform.position, Vector2.right, Color.cyan, 0f, false);
		Debug.DrawRay (transform.position, (Vector2.right + Vector2.up).normalized * 3f, Color.magenta, 0f, false);
		Debug.DrawRay (transform.position, (Vector2.right + Vector2.down).normalized * 3f, Color.magenta, 0f, false);

		Vector2 playerVector = player.transform.position - transform.position;

		LayerMask playerAndWalls = LayerMask.GetMask ("Player", "Unpassable");

		RaycastHit2D playerCast = Physics2D.Raycast (transform.position, playerVector.normalized, 3f, playerAndWalls);

		if (playerCast.collider == null)
		{
			Debug.DrawRay (transform.position, playerVector.normalized * 3f, Color.red, 0f, false);
		}
		else if (playerCast.collider.gameObject == player)
		{
			Debug.DrawLine (transform.position, playerCast.point, Color.green, 0f, false);
		}
		else
		{
			Debug.DrawLine (transform.position, playerCast.point, Color.red, 0f, false);
		}
	}

	private void DrawPath(Vector2[] path)
	{
		for (int i = 0; i < path.Length - 1; i++)
		{
			Debug.DrawLine (path [i], path [i + 1], Color.yellow);
		}
	}
}

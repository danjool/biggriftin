using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dijkstra : MonoBehaviour 
{
	[SerializeField]
	private bool drawPath = true;

	[SerializeField]
	private GameObject goal;

	[SerializeField]
	private Vector2 bottomLeft, topRight;
	// Use this for initialization

	public Vector2[] GetPath()
	{
		bool[,] passable = new bool[(int)(topRight.x - bottomLeft.x), (int)(topRight.y - bottomLeft.y)];

		for (int i = 0; i < passable.GetLength(0); i++) 
		{
			for (int j = 0; j < passable.GetLength (1); j++) 
			{
				passable[i,j] = (Physics2D.OverlapPoint(
					new Vector2(bottomLeft.x + i, bottomLeft.y + j), 
					LayerMask.GetMask("Unpassable"))==null);
			}
		}

		int[,] distance = new int[passable.GetLength (0), passable.GetLength (1)];

		return null;
	}
}
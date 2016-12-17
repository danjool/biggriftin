using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.CodeDom;
using System.Collections.Specialized;
using System.Xml.Linq;
using System.Security;

public static class BreadthFirstSearch
{
	private class GridPosition
	{
		public int x, y;
		public GridPosition(Vector2 position) : this((int)Mathf.Round(position.x),(int)Mathf.Round(position.y)){}
		public GridPosition(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
		public Vector2 ToVector2()
		{
			return new Vector2 (x, y);
		}
	}

	private class SearchNode
	{
		public GridPosition position;
		public SearchNode previousNode;
		public SearchNode(GridPosition position, SearchNode previousNode)
		{
			this.position = position;
			this.previousNode = previousNode;
		}
	}

	private class SearchMap
	{
		private bool[,] searched;
		private GridPosition bottomLeft, topRight;
		public SearchMap(GridPosition bottomLeft, GridPosition topRight)
		{
			this.bottomLeft = bottomLeft;
			this.topRight = topRight;
			searched = new bool[topRight.x - bottomLeft.x + 1, topRight.y - bottomLeft.y + 1];

			for (int i = 0; i < searched.GetLength(0); i++) 
			{
				for (int j = 0; j < searched.GetLength (1); j++) 
				{
					searched[i,j] = (Physics2D.OverlapPoint(
						new Vector2(bottomLeft.x + i, bottomLeft.y + j), 
						LayerMask.GetMask("Unpassable"))!=null);
				}
			}
		}
		public bool CheckSearched(GridPosition position)
		{
			if (!InGrid (position))
				return true;

			return searched [position.x - bottomLeft.x, position.y - bottomLeft.y];
		}
		public void SetSearched(GridPosition position)
		{
			if (InGrid (position))
			{
				searched [position.x - bottomLeft.x, position.y - bottomLeft.y] = true;
			}
		}

		private bool InGrid(GridPosition position)
		{
			return !(position.x < bottomLeft.x || position.y < bottomLeft.y || position.x > topRight.x || position.y > topRight.y);
		}
	}

	public static Vector2[] GetPath(Vector2 bottomLeft, Vector2 topRight, Vector2 start, Vector2 goal)
	{
		GridPosition startGrid = new GridPosition (start);
		GridPosition goalGrid = new GridPosition (goal);

		Queue<SearchNode> nodeQueue = new Queue<SearchNode> ();
		nodeQueue.Enqueue(new SearchNode(startGrid,null));
		SearchMap map = new SearchMap (new GridPosition(bottomLeft), new GridPosition(topRight));
		map.SetSearched (startGrid);
		Vector2[] gridPath = Search (nodeQueue, goalGrid, map);
		Vector2[] fullPath = new Vector2[gridPath.Length + 2];
		fullPath [0] = start;
		fullPath [fullPath.Length - 1] = goal;
		for (int i = 0; i < gridPath.Length; i++)
		{
			fullPath [i + 1] = gridPath [i];
		}
		return fullPath;
	}

	private static Vector2[] Search(Queue<SearchNode> nodeQueue, GridPosition goal, SearchMap searchMap)
	{
		if (nodeQueue.Count == 0)
			return null;

		SearchNode node = nodeQueue.Dequeue ();

		if (node.position.x == goal.x && node.position.y == goal.y)
		{
			Stack<SearchNode> nodeStack = new Stack<SearchNode> ();
			nodeStack.Push (node);
			while (nodeStack.Peek().previousNode != null)
			{
				nodeStack.Push (nodeStack.Peek ().previousNode);
			}

			Vector2[] path = new Vector2[nodeStack.Count];
			for (int i = 0; i < path.Length; i++)
			{
				path [i] = nodeStack.Pop ().position.ToVector2();
			}

			return path;
		}

		GridPosition top = new GridPosition (node.position.x, node.position.y + 1);
		GridPosition right = new GridPosition (node.position.x + 1, node.position.y);
		GridPosition bottom = new GridPosition (node.position.x, node.position.y - 1);
		GridPosition left = new GridPosition (node.position.x - 1, node.position.y);

		if(!searchMap.CheckSearched(top))
		{
			searchMap.SetSearched (top);
			nodeQueue.Enqueue (new SearchNode (top, node));
		}
		if(!searchMap.CheckSearched(right))
		{
			searchMap.SetSearched (right);
			nodeQueue.Enqueue (new SearchNode (right, node));
		}
		if(!searchMap.CheckSearched(bottom))
		{
			searchMap.SetSearched (bottom);
			nodeQueue.Enqueue (new SearchNode (bottom, node));
		}
		if(!searchMap.CheckSearched(left))
		{
			searchMap.SetSearched (left);
			nodeQueue.Enqueue (new SearchNode (left, node));
		}

		return Search(nodeQueue,goal,searchMap);
	}
}
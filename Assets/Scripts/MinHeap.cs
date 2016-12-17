using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//A heap for the purposes of AI
public class MinHeap
{
	private List<Task> taskHeap = new List<Task>();//Change this to an array later?
	private Dictionary<Task, int> indeces = new Dictionary<Task, int>();//Keeps track of heap element indeces so they can be removed easily
	public MinHeap() { }

	public Task GetTopTask() 
	{
		return GetTask(0);
	}
	public Task GetTask(int i) 
	{
		return taskHeap[i];
	}
	public int GetLength() 
	{
		return taskHeap.Count;
	}
	public void AddTask(Task t) 
	{
		indeces.Add(t, taskHeap.Count);
		taskHeap.Add(t);
		HeapFloatUp(taskHeap.Count - 1);
	}
	public void RemoveTask(Task t) 
	{
		RemoveTask(indeces[t]);
		indeces.Remove(t);
	}
	public void RemoveTask(int index) 
	{
		taskHeap[index] = taskHeap[taskHeap.Count - 1];
		taskHeap.RemoveAt(taskHeap.Count - 1);
		HeapFloatUp(index);
	}
	public void BuildMinHeap() 
	{
		for (int i = taskHeap.Count - 1; i >= 0; i--) 
		{
			MinHeapify(i);
		}
	}
	private void MinHeapify(int current)
	{
		int left = GetLeft(current);
		int right = GetRight(current);
		int smallest;
		//Find which has the smallest priority out of left, right, and current
		if (left < taskHeap.Count && taskHeap[left].Priority < taskHeap[current].Priority)
		{
			smallest = left;
		}
		else smallest = current;
		if (right < taskHeap.Count && taskHeap[right].Priority < taskHeap[current].Priority) 
		{
			smallest = right;
		}
		//If current isn't the smallest, swap current with the smallest and Min-Heapify the swapped child.
		if (smallest != current) 
		{
			Task temp = taskHeap[smallest];
			taskHeap[smallest] = taskHeap[current];
			indeces[taskHeap[smallest]] = smallest;
			taskHeap[current] = temp;
			indeces[taskHeap[current]] = current;

			MinHeapify(smallest);
		}
	}
	private void HeapFloatUp(int current) 
	{
		MinHeapify(current);
		if (current > 0 && taskHeap[current].Priority < taskHeap[GetParent(current)].Priority) HeapFloatUp(GetParent(current));
	}
	/*
    public void ChangePriority(int i, float priority) 
    {
        if (priority < taskHeap[i].GetPriority())
        {
            taskHeap[i].GetPriority = priority;
            HeapFloatUp(i);
        }
        else 
        {
            taskHeap[i].GetPriority = priority;
            MinHeapify(i);
        }
    }*/
	private int GetParent(int i) 
	{
		return i / 2;
	}
	private int GetLeft(int i) 
	{
		return 2 * i;
	}
	private int GetRight(int i) 
	{
		return 2 * i + 1;
	}
	public Task[] ToArray() 
	{
		return taskHeap.ToArray();
	}
}
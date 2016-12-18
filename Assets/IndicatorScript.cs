using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorScript : MonoBehaviour {


	public GameObject dropoff;
	private GameObject player;
	private Camera mainCamera;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("body");
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
	}

	// Update is called once per frame
	void Update () {
		Vector2 toDropoff = dropoff.transform.position - mainCamera.transform.position;
		Vector2 position = (Vector2)mainCamera.transform.position + toDropoff.normalized * mainCamera.orthographicSize * 0.9f;
		transform.up = toDropoff.normalized;
		transform.position = position;

		GameObject[] hands = GameObject.FindGameObjectsWithTag ("hands");
		bool full = true;
		for (int i = 0; i < hands.Length; i++)
		{
			if (!hands [i].GetComponent<hands> ().holding)
			{
				full = false;
			}
		}

		gameObject.GetComponent<SpriteRenderer> ().enabled = full;
	}
}

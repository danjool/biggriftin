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
		float posY = mainCamera.ScreenToWorldPoint(new Vector3(0, mainCamera.pixelHeight - 10, 0)).y;
		float posX = toDropoff.x / toDropoff.y * posY;
		transform.position = new Vector2 (posX, posY);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SetOrderInLayer : MonoBehaviour {

	private SpriteRenderer sr;
	// Use this for initialization
	void Start () 
	{
		sr = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		sr.sortingOrder = (int)-transform.position.y;
	}
}

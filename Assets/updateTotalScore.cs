using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class updateTotalScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject.FindGameObjectWithTag ("totalValue").GetComponent<Text> ().text = ApplicationModel.totalValueStole.ToString ();

		if (Input.GetKey( KeyCode.Space )) {
			SceneManager.LoadScene ("IntroScene");
		}

	}
}

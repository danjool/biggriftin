using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hands : MonoBehaviour {

	public AudioClip grab;
	private AudioSource speaker;
	public float baseMass = 0.05f;
	public bool holding = false;
	private Collider2D heldObject = null;
	Text blankText;

	//private LineRenderer arm;
	// Use this for initialization
	void Start () {
		ApplicationModel.totalValueStole = 0f;
		speaker = gameObject.transform.parent.GetComponent<AudioSource> ();
		blankText = GameObject.FindGameObjectWithTag ("blankLabel").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		//point those hands towards the player
		Vector3 bodyP = GameObject.FindGameObjectWithTag("body").transform.position;
		//this.gameObject.transform.Rotate ( new Vector3( 0.0f, 0.0f, 10.0f ));
		Vector3 diff = bodyP - this.transform.position;
		float rot =  Mathf.Atan2(diff.y, diff.x)*Mathf.Rad2Deg;
		this.gameObject.transform.rotation = Quaternion.AngleAxis ( rot, Vector3.forward );
	}

	void FixedUpdate(){
		//update arm
		//got nixed for visual reasoins


		//hand
		//GetComponent<LineRenderer>().SetPosition(1, this.GetComponent<Rigidbody2D>().transform.position + new Vector3(0.0f,0.0f,11.0f) );
		//body
		//GetComponent<LineRenderer>().SetPosition(0, GameObject.FindGameObjectWithTag("body").transform.position + new Vector3(0f,0.15f,11.0f) );
	}

	public void resetMass(){
		//for when we drop off the loot, make the hands light again
		//the mass change for picking stuff up is fudged so the loot isn't rigidbody
		this.GetComponent<Rigidbody2D> ().mass = 0.05f;
	}	

	void OnTriggerEnter2D( Collider2D other ){
		if (other.gameObject.tag == "stealable" && holding == false && other.GetComponent<stealable>().stole ==false ) {
			
			//pickup the stealable, set the hand to holding so it doesn't grab more
			other.gameObject.transform.parent = this.transform;
			other.gameObject.transform.localPosition= new Vector3 (0.0f, 0.0f, 1.0f);
			heldObject = other;
			heldObject.GetComponent<stealable> ().stole = true;
			holding = true;

			speaker.PlayOneShot (grab);
			//up the mass of the hand by the mass of the stole object
			this.GetComponent<Rigidbody2D>().mass += heldObject.GetComponent<stealable> ().Mass;

			//update the theme text to display the name of what we just grabbed
			blankText.text = "All your " + heldObject.name + " are belong to us!";

			GameObject.FindGameObjectWithTag ("massLabel").GetComponent<Text>().text = heldObject.GetComponent<stealable>().Mass.ToString() + " Brabbples";
			GameObject.FindGameObjectWithTag ("valueLabel").GetComponent<Text>().text = heldObject.GetComponent<stealable>().Value.ToString();


		}

		if (other.gameObject.tag == "body" && holding == true) {
			//attach the object to the body
			heldObject.gameObject.transform.parent = null;//other.gameObject.transform.parent;

		}
	}
}

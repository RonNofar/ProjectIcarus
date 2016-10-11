using UnityEngine;
using System.Collections;

public class BirdMoement2 : MonoBehaviour {

	private Rigidbody rigidbody;

	int rel_tor = 20;
	float rotateAmount = 5;

	// Use this for initialization
	void Start () {
		//Cursor.lockState;
		rigidbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		Rigidbody rigidbody = GetComponent<Rigidbody> ();

		// Turn
		var h = Input.GetAxis("Mouse X");
		var v = Input.GetAxis ("Mouse Y");

		transform.Rotate(0,h*rel_tor,v*rel_tor);

		// Thrust
		if (Input.GetKey(KeyCode.Space)) rigidbody.AddForce(transform.forward);
	}
}

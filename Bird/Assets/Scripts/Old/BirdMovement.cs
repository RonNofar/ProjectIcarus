using UnityEngine;
using System.Collections;

public class BirdMovement : MonoBehaviour {

	public float speed = 6.0f;
	public float jumpSpeed = 4.0f;
	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLength = 100f;
	float distToGround = 10f; 
	bool jumping;
	bool flying;

	// Use this for initialization
	void Start () {
		floorMask = LayerMask.GetMask ("Floor");
		//anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent <Rigidbody> ();
		jumping = false;
		flying = false;
	}
	
	// Update is called once per frame
	void Update () {
		//float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Move (v);
	}

	bool IsGrounded() {
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
	}

	void Move (float h) {
		movement.Set (h, 0f, 0f);
		movement = movement.normalized * speed * Time.deltaTime;
		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Jump (){
		if (Input.GetKeyDown (KeyCode.Space) && IsGrounded ()) {
			if (jumping == false) {
				jumping = true;
			} else {
				flying = true;
			}
		}
	}
	void Fly (){
		if (jumping == true) {
			playerRigidbody.MovePosition (transform.position);
			//x += jumpSpeed;
		}
	}
}

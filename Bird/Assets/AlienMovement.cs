using UnityEngine;
using System.Collections;

public class AlienMovement : MonoBehaviour {

    public float movementSpeed = 1;


    private Transform myTransform;
    private Rigidbody myRigidbody;

	// Use this for initialization
	void Start () {
        SetInitialReference();

    }

    void SetInitialReference () {
        myTransform = GetComponent<Transform>();
        myRigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        float translation_vertical = Input.GetAxis("Vertical") * movementSpeed;
        float translation_horizontal = Input.GetAxis("Horizontal") * movementSpeed;
        translation_vertical *= Time.deltaTime;
        translation_horizontal *= Time.deltaTime;
        myRigidbody.AddForce(myTransform.forward * translation_vertical, ForceMode.Acceleration); // WASD forward/backward
        myRigidbody.AddForce(myTransform.right * translation_horizontal, ForceMode.Acceleration);
    }
}

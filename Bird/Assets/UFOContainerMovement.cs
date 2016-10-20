using UnityEngine;
using System.Collections;

public class UFOContainerMovement : MonoBehaviour {

    public float rotationSpeed = 1000;
    public float movementSpeed = 10;

    private Transform myTransform;
    private Rigidbody myRigidbody;


	// Use this for initialization
	void Start () {
        SetInitialReferences();
	}

    void SetInitialReferences () {
        myTransform = GetComponent<Transform>();
        myRigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (myTransform != null && !PlayerMaster.isPlayerDead) {
            float rotation = Input.GetAxis("Mouse X") * rotationSpeed;
            //myRigidbody.AddTorque(myTransform.right * rotation);
            myTransform.Rotate(myTransform.forward * Time.deltaTime * rotation, Space.World);

            myTransform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        }
	}
}

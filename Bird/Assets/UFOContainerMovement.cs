﻿using UnityEngine;
using System.Collections;

public class UFOContainerMovement : MonoBehaviour {

    public float rotationSpeed = 1000;
    public float movementSpeed = 10;
    public float accelerationCap;

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
        if (GameMaster.isGameStarted && myTransform != null && PlayerMaster.isPlayerDead == false) {
            // Rotation
            float rotation = Input.GetAxis("Mouse X") * rotationSpeed;
            //myRigidbody.AddTorque(myTransform.right * rotation);
            myTransform.Rotate(myTransform.forward * Time.deltaTime * rotation, Space.World);
            // Acceleration
            if (GameMaster.isGameStarted) {
                // Acceleration
                float MouseY = Input.GetAxis("Mouse Y");
                accelerationCap = movementSpeed / 2;
                //Debug.Log(MouseY);
            
                float acceleration = movementSpeed + MouseY;
                if (acceleration > movementSpeed + accelerationCap) acceleration = movementSpeed + accelerationCap;
                else if (acceleration < movementSpeed - accelerationCap) acceleration = movementSpeed - accelerationCap;

                myTransform.Translate(Vector3.forward * Time.deltaTime * acceleration);
            }

        }
    }
}

using UnityEngine;
using System.Collections;

public class AlienCameraControl : MonoBehaviour {

    public Camera myCamera;

    public float cameraThreshhold = 5; // in degrees
    public float cameraTurnSpeed = 1;

    private Transform myTransform;
    private Rigidbody myRigidbody;
    private Transform cameraTransform;

    private Vector3 eulerAngles;
    private Vector3 cameraEulerAngles;

	// Use this for initialization
	void Start () {
        SetInitialReferences();

    }

    void SetInitialReferences ()
    {
        myTransform = GetComponent<Transform>();
        myRigidbody = GetComponent<Rigidbody>();
        cameraTransform = myCamera.GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (cameraEulerAngles.y > eulerAngles.y + cameraThreshhold) // add a camera threshhold
        {
            cameraEulerAngles.y -= cameraTurnSpeed;
        }
        else if (cameraEulerAngles.y < eulerAngles.y - cameraThreshhold)
        {
            cameraEulerAngles.y += cameraTurnSpeed;
        }
	}

    void UpdateEulerAngles ()
    {
        eulerAngles = myTransform.eulerAngles;
        cameraEulerAngles = cameraTransform.eulerAngles;
        
    }
}

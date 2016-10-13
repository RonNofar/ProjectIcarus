using UnityEngine;
using System.Collections;

public class AlienCameraControl : MonoBehaviour {

    public bool __DEBUG__ = true;

    public Camera myCamera;

    public float cameraThreshhold = 5; // in degrees
    public float cameraTurnSpeed = 2;

    private Transform myTransform;
    private Rigidbody myRigidbody;
    private Transform cameraTransform;

    private Vector3 eulerAngles;
    private Vector3 cameraEulerAngles;

    public float momentumCheck = 10; // if momentumCheck = 1, then checks every second.
    private float nextCheck;
    private Vector3 playerPosition;
    private float momentumAngle;
    private float momentumRadians;

    public float frameCheck = 2;
    private float X_1;
    private float X_2;
    private float Z_1;
    private float Z_2;
    private float X_distance;
    private float Z_distance;
    private float H_distance;

    // Use this for initialization
    void Start () {
        SetInitialReferences();
    }

    void SetInitialReferences ()
    {
        myTransform = GetComponent<Transform>();
        myRigidbody = GetComponent<Rigidbody>();
        cameraTransform = myCamera.GetComponent<Transform>();
        UpdatePlayerPosition();
    }
	
	// Update is called once per frame
	void Update () {
        UpdatePlayerPosition();
        UpdateMomentumAngle();
        Debug.Log("eulerAngle: " + eulerAngles.y);
        Debug.Log("momentumAngle: " + momentumAngle);
        // Rotate the parent
        //myTransform.Rotate(Vector3.up * Time.deltaTime, Space.World);
        //Debug.Log("camera y: "+cameraEulerAngles.y);
        if (eulerAngles.y > momentumAngle)// + cameraThreshhold) // add a camera threshhold
        {
            myTransform.Rotate(-Vector3.up * Time.deltaTime * cameraTurnSpeed, Space.World);
        }
        else if (eulerAngles.y < momentumAngle)// - cameraThreshhold)
        {
            myTransform.Rotate(Vector3.up * Time.deltaTime * cameraTurnSpeed, Space.World);
        }
	}

    void UpdateEulerAngles ()
    {
        eulerAngles = myTransform.eulerAngles;
        cameraEulerAngles = cameraTransform.eulerAngles;
        
    }

    void UpdatePlayerPosition()
    {
        playerPosition = myTransform.position;
    }

    void UpdateMomentumAngle()
    {
        //Debug.Log(Time.frameCount);
        if (Time.frameCount % frameCheck != 0) // If odd
        {
            X_1 = playerPosition.x;
            Z_1 = playerPosition.z;
            if (__DEBUG__) Debug.Log("X_1: " + X_1 + " | Z_1: " + Z_1);
        }
        if (Time.frameCount % frameCheck == 0) // If even
        {
            X_2 = playerPosition.x;
            Z_2 = playerPosition.z;
            if (__DEBUG__) Debug.Log("X_2: " + X_2 + " | Z_2: " + Z_2);
            X_distance = X_2 - X_1;
            Z_distance = Z_2 - Z_1;
            H_distance = Mathf.Sqrt(Mathf.Pow(X_distance, 2) + Mathf.Pow(Z_distance, 2));
            //Debug.Log("X_distance: " + X_distance + " | Z_distance: " + Z_distance);
            momentumRadians = Mathf.Acos(Z_distance / H_distance);
            momentumAngle = (momentumRadians * Mathf.Rad2Deg);// + 180;
            //Debug.Log("momentumAngle: " + momentumAngle);
        }



    }
}

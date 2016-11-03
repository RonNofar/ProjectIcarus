using UnityEngine;
using System.Collections;

public class LaserControl : MonoBehaviour {

    public GameObject laserObject;
    public float movementSpeed;
    public float minThreshold = 0;
    public float maxThreshold = 100;

    public bool XAxis = false;
    public bool YAxis = false;
    public bool ZAxis = false;
    public bool isStartingNegative = false;

    private Transform laserTransform;
    private Vector3 startingPosition;

	// Use this for initialization
	void Start () {
        SetInitialReferences();

    }

    void SetInitialReferences () {
        if (laserObject != null) laserTransform = laserObject.GetComponent<Transform>();
        else Debug.Log("laserObject not found.");
        if (isStartingNegative) movementSpeed = -movementSpeed;
        startingPosition = laserTransform.position;
    }

    // Update is called once per frame
    void Update () {
        float tranPos_start = 0;
        float tranPos_curr = 0;
        Vector3 translation = new Vector3(0, 0, 0);

        bool movement = true;

	    if (laserTransform != null) {
            if (XAxis) {
                tranPos_start = startingPosition.x;
                tranPos_curr = laserTransform.position.x;
                translation = new Vector3(movementSpeed, 0, 0);
            }
            else if (YAxis) {
                tranPos_start = startingPosition.y;
                tranPos_curr = laserTransform.position.y;
                translation = new Vector3(0, movementSpeed, 0);
            }
            else if (ZAxis) {
                tranPos_start = startingPosition.z;
                tranPos_curr = laserTransform.position.z;
                translation = new Vector3(0, 0, movementSpeed);
            }
            else {
                Debug.Log("No axis selected.");
                movement = false;
            }

            if (movement) {
                if (tranPos_curr + movementSpeed > tranPos_start + maxThreshold) {
                    movementSpeed = -Mathf.Abs(movementSpeed); // forces to be negative
                } else if (tranPos_curr - movementSpeed < tranPos_start - minThreshold) {
                    movementSpeed = Mathf.Abs(movementSpeed);
                }
                laserTransform.Translate(translation);
            }
        }
	}
}

/*
 *  Notes:
 *      |_For ground check
 *          |_Make objects of tag 'ground'
 *          |_Check for maximumLength raycast 
 *              |_if hits object with tag 'ground'
 *              |_then track length to ground 
 */

using UnityEngine;
using System.Collections;

public class AlienPickup : MonoBehaviour {

    public float maximumLength;
    private float length; // If length(l) to ground(ltg)<maximum length(ml), then l=ml-ltg. Else, l=ml

    private Transform myTransform;
    private Rigidbody myRigidbody;

    private Vector3 center;

	// Use this for initialization
	void Start () {
	
	}

    void SetInitialReferences ()
    {
        myTransform = GetComponent<Transform>();
        myRigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        UpdateCenter();
	    Physics.CheckCapsule(center,center+new Vector3(0,length,0),radius)
	}

    void UpdateCenter ()
    {
        center = myTransform.position;
    }
}

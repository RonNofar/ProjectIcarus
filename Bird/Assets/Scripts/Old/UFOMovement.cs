using UnityEngine;
using System.Collections;

public class UFOMovement : MonoBehaviour {

    [SerializeField] private float movementSpeed_Z = 10;
    [SerializeField] private float movementSpeed_X = 10;
    [SerializeField] private float movementSpeed_Y = 1;

    private Transform myTransform;
    private Rigidbody myRigidbody;

	// Use this for initialization
	void Start () {
        SetInitialReferences();

    }

    void SetInitialReferences ()
    {
        myTransform = GetComponent<Transform>();
        myRigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(Input.GetAxis("Vertical"));
        HorizontalTransform(ref myTransform, ref myRigidbody);
        if (Input.GetButton("Jump")&& Input.GetAxis("Vertical") != -1)
        {
            myRigidbody.AddForce(myTransform.up * movementSpeed_Y, ForceMode.Acceleration);
        }
        else if (Input.GetButton("Jump")&&Input.GetAxis("Vertical")==-1) // If holding "Jump" and back-button
        {
            myRigidbody.AddForce(-myTransform.up * movementSpeed_Y, ForceMode.Acceleration);
        }
	}

    void HorizontalTransform (ref Transform tran, ref Rigidbody rgd)
    {
        if (rgd != null && tran != null)
        {
            Debug.Log("Horizontal: " + Input.GetAxis("Horizontal"));
            Debug.Log("Vertical: " + Input.GetAxis("Vertical"));
            float translation_Z = Input.GetAxis("Vertical") * movementSpeed_Z;
            float translation_X = Input.GetAxis("Horizontal") * movementSpeed_X;
            translation_Z *= Time.deltaTime;
            translation_X *= Time.deltaTime;

            Debug.Log("translation_X: "+translation_X);
            Debug.Log("translation_Z: " + translation_Z);                                                       

            rgd.AddForce(tran.forward * translation_Z, ForceMode.Acceleration); // WASD forward/backward
            rgd.AddForce(tran.right * translation_X, ForceMode.Acceleration); // WASD forward/backward
        }
        else if (rgd != null && tran == null) Debug.Log("Transform is null");
        else if (tran != null && rgd == null) Debug.Log("Rigidbody is null");
        else Debug.Log("Transform and Rigidbody are null");
    }

    void VerticalTransform (Transform tran)
    {

    }
}

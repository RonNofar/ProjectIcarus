using UnityEngine;
using System.Collections;

public class BirdPhysics : MonoBehaviour {

    public bool invertY = false;
    public bool invertX = false;

    public float hoverForce = 5;
    public float jumpRate = 0.1f;
    public float movementSpeed = 250;
    public float turnSpeed = 10;
    public float x_rotationSpeed = 1;
    public float y_rotationSpeed = 1;
    public float glideForce = 5;
    public float floatForce = 0.5f; // between 0 and 1
    public float boostForce = 0.1f;

    public float minRotateHeight = 1;

    private int isYInverted; // bool in the form of -1 || 1
    private int isXInverted;

    public float drag;
    public float thrust;
    public float weight;
    public float liftForce = 1;
    public float wingArea = 1;

    private Transform myTransform;
    private Rigidbody myRigidbody;

    private float lift;
    private float mass;
    private float density;


	// Use this for initialization
	void Start () {
        SetInitialReferences();
    }

    void SetInitialReferences ()
    {
        myTransform = GetComponent<Transform>();
        myRigidbody = GetComponent<Rigidbody>();

        mass = myRigidbody.mass;
        density = 1;

        if (invertY) isYInverted = 1;
        else isYInverted = -1;
        if (invertX) isXInverted = 1;
        else isXInverted = -1;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Jump"))
        {
            if (myRigidbody.velocity.y < 0)
            {
                UpdateLiftFactor();
                myRigidbody.AddForce(myTransform.up * lift);
                myRigidbody.AddForce(myTransform.forward * lift * 2);
            }
        }

        float translation = Input.GetAxis("Vertical") * movementSpeed;
        float turn = Input.GetAxis("Horizontal") * turnSpeed;
        translation *= Time.deltaTime;
        turn *= Time.deltaTime;
        myRigidbody.AddForce(myTransform.forward * translation, ForceMode.Acceleration); // WASD forward/backward
        myRigidbody.AddTorque(Vector3.up * turn); // WASD turn

        if (myTransform.position.y >= minRotateHeight) // Mouse look
        {
            float x_rotation = Input.GetAxis("Mouse X") * x_rotationSpeed;
            float y_rotation = Input.GetAxis("Mouse Y") * y_rotationSpeed;
            myTransform.Rotate(isYInverted * y_rotation, 0, isXInverted * x_rotation);
        }
    }

    void UpdateLiftFactor()
    {
        lift = liftForce * ((density * Mathf.Pow(Mathf.Abs(myRigidbody.velocity.y), 2)) / 2) * wingArea;
    }
}

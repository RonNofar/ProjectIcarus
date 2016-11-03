/*
    Notes:
        |_Drag should be applied
        |_while holding glide, forward momentum is translated to lift
        |_change mouse look?
*/

using UnityEngine;
using System.Collections;

public class BirdPhysics : MonoBehaviour {

    public bool __DEBUG__ = true;

    public bool invertY = false;
    public bool invertX = false;

    public float glidingMagnitudeThreshhold = 5;
    public float maximumGlideMagnitude = 100;

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
 
    public float thrust;
    public float weight;
    public float liftForce = 1;
    public float wingArea = 1;
    public float dragForce = 1;
    public float surfaceArea = 1;
    public float torqueForce = 0.001f;

    private Transform myTransform;
    private Rigidbody myRigidbody;

    private float mass;

    private float lift; // NASA equation for lift factor: https://www.grc.nasa.gov/www/k-12/airplane/lifteq.html
    private float drag; // NASA equation for drag factor: https://www.grc.nasa.gov/www/k-12/airplane/drageq.html
    private float density; // Standard equation for density: http://formulas.tutorvista.com/physics/density-formula.html
    private float torque; // Standard equation for torque: http://formulas.tutorvista.com/physics/torque-formula.html

    private float nextFire;

    private Vector3 eulerAngles;

    // Use this for initialization
    void Start () {
        SetInitialReferences();
    }

    void SetInitialReferences ()
    {
        myTransform = GetComponent<Transform>();
        myRigidbody = GetComponent<Rigidbody>();

        mass = myRigidbody.mass;
        density = mass; // Unity used mass for density

        if (invertY) isYInverted = 1;
        else isYInverted = -1;
        if (invertX) isXInverted = 1;
        else isXInverted = -1;
    }
	
	// Update is called once per frame
	void Update () {
        UpdateEulerAngles();
        //if (__DEBUG__) Debug.Log("magnitude: "+myRigidbody.velocity.magnitude);
        if (Input.GetButton("Jump"))
        {
            if (myRigidbody.velocity.magnitude > glidingMagnitudeThreshhold) // Gliding
            {
                UpdateLiftFactor();
                UpdateTorqueFactor();
                if (myRigidbody.velocity.magnitude < maximumGlideMagnitude)
                {
                    myRigidbody.AddForce(myTransform.up * lift); // Apply lift
                }
                myRigidbody.AddTorque(-myTransform.right * torque); // Apply torque
                //myRigidbody.AddForce(-myTransform.forward * drag); // Apply drag
                //myRigidbody.AddForce(myTransform.forward * lift * 2);
            }
        }/*
        if (Input.GetButtonUp("Jump") && Time.time > nextFire)
        {
            if (__DEBUG__) Debug.Log("Entered Jump");
            myRigidbody.AddForce(myTransform.up * hoverForce, ForceMode.Impulse);
            myRigidbody.AddForce(myTransform.forward * (hoverForce / 2), ForceMode.Impulse);
            nextFire = Time.time + jumpRate; // Delay between jumps ALSO prevents the above code from breaking
        }*/

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
        lift = liftForce * ((density * Mathf.Pow(Mathf.Abs(myRigidbody.velocity.magnitude), 2)) / 2) * wingArea;
    }

    void UpdateDragFactor()
    {
        drag = dragForce * ((density * Mathf.Pow(Mathf.Abs(myRigidbody.velocity.magnitude), 2)) / 2) * surfaceArea;
    }

    void UpdateTorqueFactor()
    {
        torque = Mathf.Abs(myRigidbody.velocity.magnitude) * torqueForce;
    }

    void UpdateEulerAngles()
    {
        eulerAngles = myTransform.rotation.eulerAngles;
        if (__DEBUG__) Debug.Log("eulerAngles || x: " + eulerAngles.x + " y: " + eulerAngles.y + " z: " + eulerAngles.z);
    }

}

/*  Notes:
 *      |_If downward force > some float then When wings open inverse downward speed as impulse force.
 *          |_So when force < float, no upward momentum is gained
 * 
 *      |_When holding the back button ("s"), bird should stop and decend rather than go backwards.
 * 
 */
using UnityEngine;
using System.Collections;

public class BirdMovement3 : MonoBehaviour {

    public bool __DEBUG__ = true;

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


    private float nextFire;
    private Transform myTransform;
    private Rigidbody myRigidbody;

    private int isYInverted; // bool in the form of -1 || 1
    private int isXInverted;

	// Use this for initialization
	void Start () {
        if (__DEBUG__) Debug.Log("Started BirdMovement3");
        SetInitialReferences();

    }

    void SetInitialReferences ()
    {
        myTransform = transform;
        myRigidbody = GetComponent<Rigidbody>();
        if (__DEBUG__) Debug.Log("Assigned rigidbody");
        if (invertY) isYInverted = 1;
        else isYInverted = -1;
        if (invertX) isXInverted = 1;
        else isXInverted = -1;
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("velocity: "+myRigidbody.velocity.y);
        //myRigidbody.AddForce(-Physics.gravity * floatForce);
        if (Input.GetButton("Jump"))
        {
            Debug.Log("adding: " + glideForce*-myRigidbody.velocity.y);
            //myRigidbody.AddForce(Vector3.up * glideForce, ForceMode.Acceleration);
            //myRigidbody.AddForce(-Physics.gravity * floatForce);
            if (myRigidbody.velocity.y < 0) // breaks game when upside down
            {
                myRigidbody.AddForce(myTransform.up * (glideForce * -myRigidbody.velocity.y), ForceMode.Acceleration);
            }
        }
	    if (Input.GetButtonUp("Jump") && Time.time>nextFire)
        {
            if (__DEBUG__) Debug.Log("Entered Jump");
            myRigidbody.AddForce(myTransform.up * hoverForce, ForceMode.Impulse);
            myRigidbody.AddForce(myTransform.forward * (hoverForce / 2), ForceMode.Impulse);
            nextFire = Time.time + jumpRate; // Delay between jumps ALSO prevents the above code from breaking
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // boost player while doing boostCount -= Time.deltaTime;
            myRigidbody.AddForce(myTransform.forward * boostForce, ForceMode.Acceleration);
        }
        float translation = Input.GetAxis("Vertical") * movementSpeed;
        float turn = Input.GetAxis("Horizontal") * turnSpeed;
        translation *= Time.deltaTime;
        turn *= Time.deltaTime;
        //Debug.Log(translation);
        myRigidbody.AddForce(myTransform.forward * translation,ForceMode.Acceleration); // WASD forward/backward
        //myTransform.Translate(0, 0, translation);
        myRigidbody.AddTorque(Vector3.up * turn); // WASD turn
        //myTransform.Rotate(0, rotation, 0);

        if (myTransform.position.y >= minRotateHeight) // Mouse look
        {
            float x_rotation = Input.GetAxis("Mouse X") * x_rotationSpeed;
            float y_rotation = Input.GetAxis("Mouse Y") * y_rotationSpeed;
            myTransform.Rotate(isYInverted * y_rotation, 0, isXInverted * x_rotation);
        }
    }
}

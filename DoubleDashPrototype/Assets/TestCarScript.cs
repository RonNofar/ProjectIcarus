using UnityEngine;
using System.Collections;

public class TestCarScript : MonoBehaviour {

    public float accelerationRate = 1;
    public float steeringRate = 0.1f;

    private Transform m_Transform;
    private Rigidbody m_Rigidbody;

    void Start () {
        SetInitialReferences();
    }

    void SetInitialReferences() {
        m_Transform = GetComponent<Transform>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }
	
	void Update () {
        //Debug.Log(Input.GetAxis("LeftTrigger"));
	}

    void FixedUpdate () {
        float acceleration = Input.GetAxis("LeftTrigger") * -accelerationRate;
        float steering = Input.GetAxis("Horizontal") * steeringRate;
        Move(steering, acceleration);
    }

    void Move (float steering, float acceleration) {
        if (acceleration != 0) {
            //Debug.Log(acceleration);
            m_Rigidbody.AddForce(m_Transform.forward * acceleration, ForceMode.VelocityChange);
        }
        if (steering != 0 && m_Rigidbody.velocity != new Vector3(0,0,0)) {
            m_Rigidbody.AddTorque(m_Transform.up * steering, ForceMode.Force);
        }

    }
}

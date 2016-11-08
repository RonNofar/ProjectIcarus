using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleCarController : MonoBehaviour {

    public List<AxleInfo> axleInfos;
    public float maxMotorTorque = 200;
    public float maxSteeringAngle = 25;

    public void ApplyLocalPositionToVisuals(WheelCollider col) {
        if (col.transform.childCount == 0) return;

        Transform visualWheel = col.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    public void FixedUpdate () {
        float motor = maxMotorTorque * -Input.GetAxis("LeftTrigger");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.frontLeftWheel.steerAngle = steering;
                axleInfo.frontRightWheel.steerAngle = steering;
            }
            if (axleInfo.motor) {
                axleInfo.frontLeftWheel.motorTorque = motor;
                axleInfo.frontRightWheel.motorTorque = motor;
            }
            ApplyLocalPositionToVisuals(axleInfo.frontLeftWheel);
            ApplyLocalPositionToVisuals(axleInfo.frontRightWheel);
        }
    }
}

[System.Serializable]
public class AxleInfo {
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    public bool motor;
    public bool steering;
}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UFOController))]
public class UFOUserController : MonoBehaviour {

    public UFOController controller;

    private float rot, accel;

	void Update () {
        rot = Input.GetAxis("Mouse X");
        accel = Input.GetAxis("Mouse Y");
	}

    void FixedUpdate () {
        controller.Move(rot, accel);
    }
}

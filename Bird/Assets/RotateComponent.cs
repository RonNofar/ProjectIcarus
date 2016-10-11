using UnityEngine;
using System.Collections;

public class RotateComponent : MonoBehaviour {

    [SerializeField]
    private GameObject objectToRotate;
    public float rotationSpeed = 1;

    private Transform objectTransform;

	// Use this for initialization
	void Start () {
        SetInitialReferences();
	}

    void SetInitialReferences()
    {
        objectTransform = objectToRotate.transform;
    }

    // Update is called once per frame
    void Update () {
        RotateTransform(objectTransform);
	}

    void RotateTransform(Transform tran)
    {
        tran.Rotate(new Vector3(0, rotationSpeed, 0));
    }
}

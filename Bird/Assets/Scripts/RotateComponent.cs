using UnityEngine;
using System.Collections;

public class RotateComponent : MonoBehaviour {

    [SerializeField]
    private GameObject objectToRotate;
    public float rotationSpeed = 1;

    [SerializeField] private bool XAxis = false;
    [SerializeField] private bool ZAxis = false;
    [SerializeField] private bool YAxis = false;

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
        if (XAxis) tran.Rotate(new Vector3(rotationSpeed, 0, 0));
        if (ZAxis) tran.Rotate(new Vector3(0, 0, rotationSpeed));
        if (YAxis) tran.Rotate(new Vector3(0, rotationSpeed, 0));
    }
}

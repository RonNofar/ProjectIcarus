using UnityEngine;
using System.Collections;
// Put this script on trigger that activates the door
public class SwingingDoor : MonoBehaviour {

    public GameObject swingingDoor;
    public float rotationSpeed = 1;
    public float rotationFrames = 90;
    public bool XAxis = false;
    public bool YAxis = false;
    public bool ZAxis = false;
    public bool isNegative = false;

    public GameObject[] lightsObjects;
    private Light[] lights;

    private bool isSwinging = false;
    private float totalRotationFrames = 0;

    private Transform doorTransform;

    public GameObject objectLightGlass;
    private Renderer objectRenderer;
    private Material originalMaterial;
    public Material litUpMaterial;

	// Use this for initialization
	void Start () {
        SetInitialReferences();

    }

    void SetInitialReferences () {
        if (swingingDoor != null) doorTransform = swingingDoor.GetComponent<Transform>();
        else Debug.Log("No swingingDoor found.");
        if (lightsObjects != null) {
            int len = lightsObjects.Length;
            lights = new Light[len];
            for (int i = 0; i < len; ++i) {
                lights[i] = lightsObjects[i].GetComponent<Light>();
                lights[i].enabled = false;
            }
        } else { Debug.Log("No lightsObjects found."); };
        if (isNegative) rotationSpeed = -rotationSpeed; rotationFrames = -rotationFrames;
        if (objectLightGlass != null) {
            objectRenderer = objectLightGlass.GetComponent<Renderer>();
            originalMaterial = objectRenderer.material;
        } else Debug.Log("No objectLightGlass found.");
    }

    void OnTriggerEnter(Collider col) {
        // Light
        // after delay, swing door
        isSwinging = true;
        foreach (Light light in lights) {
            light.enabled = true;
        }
        if (litUpMaterial != null) objectRenderer.material = litUpMaterial;
        else Debug.Log("No litUpMaterial found.");

    }
	
	// Update is called once per frame
	void Update () {
	    if (swingingDoor != null && isSwinging) {
            Vector3 rotation = new Vector3(0,0,0);
            if (XAxis) {
                rotation = new Vector3(rotationSpeed, 0, 0);
            } else if (YAxis) {
                rotation = new Vector3(0, rotationSpeed, 0);
            } else if (ZAxis) {
                rotation = new Vector3(0, 0, rotationSpeed);
            } else Debug.Log("No axis selected.");
            if (!isNegative) {
                ++totalRotationFrames;
                if (totalRotationFrames < rotationFrames) doorTransform.Rotate(rotation * Time.deltaTime);
            } else if (isNegative) {
                --totalRotationFrames;
                if (totalRotationFrames > rotationFrames) doorTransform.Rotate(rotation * Time.deltaTime);
            }
        }
	}
}

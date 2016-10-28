using UnityEngine;
using System.Collections;
// Put this script on trigger that activates the door
public class SwingingDoor : MonoBehaviour {

    public GameObject swingingDoor;
    public bool XAxis = false;
    public bool YAxis = false;
    public bool ZAxis = false;
    public bool isNegative = false;

    public GameObject[] lightsObjects;
    private Light[] lights;

    private bool isSwinging = false;


    private Transform doorTransform;

	// Use this for initialization
	void Start () {
        SetInitialReferences();

    }

    void SetInitialReferences () {
        if (swingingDoor != null) doorTransform = swingingDoor.GetComponent<Transform>();
        if (lightsObjects != null) {
            int len = lightsObjects.Length;
            lights = new Light[len];
            for (int i = 0; i < len; ++i) {
                lights[i] = lightsObjects[i].GetComponent<Light>();
                lights[i].enabled = false;
            }
        }
    }

    void OnTriggerEnter(Collider col) {
        // Light
        // after delay, swing door
        isSwinging = true;
        foreach (Light light in lights) {
            light.enabled = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if (swingingDoor != null && isSwinging) {
            if (XAxis) {

            }
            if (YAxis) {

            }
            if (ZAxis) {

            }
        }
	}
}

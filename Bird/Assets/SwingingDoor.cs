using UnityEngine;
using System.Collections;
// Put this script on trigger that activates the door
public class SwingingDoor : MonoBehaviour {

    public GameObject swingingDoor;

    private Transform doorTransform;

	// Use this for initialization
	void Start () {
        SetInitialReferences();

    }

    void SetInitialReferences () {
        if (swingingDoor != null) doorTransform = swingingDoor.GetComponent<Transform>();
    }

    void OnTriggerEnter(Collider col) {
        // Light
        // after delay, swing door
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

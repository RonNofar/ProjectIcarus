using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UFOController))]
public class PointScript : MonoBehaviour {

    public UFOController controller;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    private void OnTriggerEnter(Collider col) {
        if (col.transform.tag == "Player") {
            controller.AddScore();
            Destroy(gameObject);
        }
    }
}

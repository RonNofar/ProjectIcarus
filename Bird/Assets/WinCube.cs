using UnityEngine;
using System.Collections;

public class WinCube : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerEnter(Collider col) {
        if (col.transform.tag == "Player") {
            GameMaster.isGameWon = true;
        }
    }
}

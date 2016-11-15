using UnityEngine;
using System.Collections;


public class UFOCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}

    void SetInitialReferences () {

    }

    void OnCollisionEnter (Collision col) {
        //Debug.Log("Entered OnCollisionEnter");
        if (col.transform.tag == "Obstacles") {
            PlayerDeath();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void PlayerDeath () {
        PlayerMaster.isPlayerDead = true;
        Debug.Log("PlayerDeath: isPlayerDead = " + PlayerMaster.isPlayerDead);
    }
}

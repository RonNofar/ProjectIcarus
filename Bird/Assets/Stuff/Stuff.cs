using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Stuff : PooledObject {

    public Rigidbody Body { get; private set; }

	void Awake () {
        Body = GetComponent<Rigidbody>();
    }
	
	void OnTriggerEnter (Collider col) {
        if (col.CompareTag("Kill Zone")) {
            ReturnToPool();
        }
    }
}

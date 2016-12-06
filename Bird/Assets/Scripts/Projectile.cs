using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : PooledObject {

    public Rigidbody m_Rigidbody { get; private set; }

    void Awake () {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter ( Collider col ) {
        if (col.CompareTag("Kill Zone")) {
            ReturnToPool();
        }
    }
}

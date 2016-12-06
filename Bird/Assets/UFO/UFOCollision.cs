using UnityEngine;
using System.Collections;


public class UFOCollision : MonoBehaviour {

    private UFOController parentController;

    void Awake () {
        parentController = GetComponentInParent<UFOController>();
    }

    void OnCollisionEnter (Collision col) {
        parentController.OnEnterChildCollision(col);
    }
}

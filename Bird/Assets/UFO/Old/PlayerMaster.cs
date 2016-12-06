using UnityEngine;
using System.Collections;

public class PlayerMaster : MonoBehaviour {

    static public bool isPlayerDead = false;
    static public int score = 0;

    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject UFOObject;

    public float explosionDur = 1;

    private Transform UFOTransform;

    private bool deathCheck = false;
    private float deathTime;

	// Use this for initialization
	void Start () {
        SetInitialReferences();

    }

    void SetInitialReferences() {
        if (UFOObject != null) UFOTransform = UFOObject.GetComponent<Transform>();
        else Debug.Log("UFOObject is null");

    }
	
	// Update is called once per frame
	void Update () {
        if (isPlayerDead) Explosion(UFOTransform);
        //if (UFOTransform != null) Debug.Log(UFOTransform.) RIGIDBODY
        if (UFOController.isMovementStarted) FixNegativeScore();
    }

    void Explosion (Transform tranLocation) { // handle through a gamemaster NOT playermaster (player master is destoryed before the rest of the code can execute
        if (!deathCheck) deathTime = Time.time; deathCheck = true;
        Object exp = Instantiate(explosionPrefab, tranLocation.position, tranLocation.rotation);
        Destroy(gameObject);
        if (deathCheck && Time.time>deathTime+explosionDur) Destroy(exp);
    }

    public bool getDeathCheck() {
        return deathCheck;
    }

    public GameObject findGameObjectOfName (string name) {
        return GameObject.Find("MisterMaster");
    }

    static public void FixNegativeScore () {
        if (score < 0) score = 0;
    }
}

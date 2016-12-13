using UnityEngine;
using System.Collections;

public class UFOController : MonoBehaviour {

    #region Structs
    [System.Serializable]
    public struct FloatRange
    {
        public float min, max;

        public float RandomInRange
        {
            get
            {
                return Random.Range(min, max);
            }
        }
    }
    #endregion

    #region Variables
    [Header("Global Variables")]
    static public bool isPlayerDead = false;
    static public bool isMovementStarted = false;
    static public bool isVisible = true;
    static public int score = 0;

    [Header("Rotation")]
    public GameObject UFOContainer;
    public float rotationSpeed = 250;

    private Transform rotTransform;

    [Header("Acceleration")]
    public float initializingThreshold = 10000;
    public float movementSpeed = 15;
    public float accelerationCap = 10;
    private float initializingMovement;

    [Header("Death")]
    [SerializeField] private GameObject explosionPrefab;
    public float explosionDuration;

    private Object exp = null;
    private float deathTime;
    private bool deathCheck = false;

    [Header("Other")]
    public GameObject UFO;
    public FloatRange scoreRange;
    private Transform m_Transform;
    #endregion

    #region Functions
    private void Awake () {
        SetInitialReferences();
    }

    private void SetInitialReferences () {
        isMovementStarted = false;
        isPlayerDead = false;
        isVisible = true;
        score = 0;

        m_Transform = gameObject.GetComponent<Transform>();
        rotTransform = UFOContainer.GetComponent<Transform>();
        accelerationCap = movementSpeed / 2;
    }
	
	// Update is called once per frame
	private void Update () {
	
	}

    public void Move (float rot, float accel) {
        if (!isPlayerDead) {
            Rotate(rot);
            if (isMovementStarted && GameMaster.isGameStarted) {
                Accelerate(accel);
                FixNegativeScore();
            }
            if (!isMovementStarted && GameMaster.isGameStarted) {
                MovementTreshholdCheck(rot);
            }
        } else {
            if (!GameMaster.isGameOver) {
                Explosion();
            }
        }
        Visibility();
    }

    private void Rotate (float rot) {
        rotTransform.Rotate(rotTransform.forward * Time.deltaTime * rot * rotationSpeed, Space.World);
    }

    private void Accelerate (float accel) {
        float acceleration = movementSpeed + accel;
        if (acceleration > movementSpeed + accelerationCap) acceleration = movementSpeed + accelerationCap;
        else if (acceleration < movementSpeed - accelerationCap) acceleration = movementSpeed - accelerationCap; // Cap acceleration

        m_Transform.Translate(m_Transform.forward * Time.deltaTime * acceleration);
    }

    private void MovementTreshholdCheck (float rot) {
        if (initializingMovement < initializingThreshold) initializingMovement += Mathf.Abs(rot);
        else isMovementStarted = true;
    }

    private void Death () {
        isPlayerDead = true;
        isVisible = false;
        Debug.Log("PlayerDeath: isPlayerDead = " + isPlayerDead);
    }

    private void Explosion () {
        Transform UFOTransform = UFO.GetComponent<Transform>();
        if (!deathCheck) {
            exp = Instantiate(explosionPrefab, UFOTransform.position, UFOTransform.rotation);
            deathTime = Time.time;
            deathCheck = true;
        }
        
        if (Time.time > deathTime + explosionDuration) {
            Destroy(exp);
            deathCheck = false;
            GameMaster.isGameOver = true;
        }
    }

    private void FixNegativeScore() {
        if (score < 0) score = 0;
    }

    private void Visibility () {
        if (isVisible) UFO.GetComponent<MeshRenderer>().enabled = true;
        else UFO.GetComponent<MeshRenderer>().enabled = false;
    }

    public void OnEnterChildCollision ( Collision col ) { // Called in UFOCollision
        if (col.transform.tag == "Obstacles") {
            Death();
        }
    }

    public void AddScore() {
        score += (int)scoreRange.RandomInRange;
    }
    #endregion

}

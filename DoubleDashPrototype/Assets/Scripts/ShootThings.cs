using UnityEngine;
using System.Collections;

public class ShootThings : MonoBehaviour {

    public GameObject projectilePrefab;
    public GameObject objectToSpin;
    public GameObject barrelObject;
    public GameObject shootFrom;

    public float fireRate = 1;
    public float rotationRate = 10;
    public float projectileSpeed = 10;

    private Transform myTransform;
    private Transform tranformToSpin;
    private Transform barrelTransform;
    private Transform shootTranform;

    private float nextFire;


    // Use this for initialization
    void Start () {
        SetInitialReferences();
    }

    void SetInitialReferences() {
        myTransform = GetComponent<Transform>();
        if (barrelObject != null) {
            barrelTransform = barrelObject.GetComponent<Transform>();
        } else Debug.Log("No barrelObject found.");
        if (objectToSpin != null) {
            tranformToSpin = objectToSpin.transform;
        } else Debug.Log("No objectToSpin found.");
        if (shootFrom != null) {
            shootTranform = shootFrom.GetComponent<Transform>();
        } else Debug.Log("No shootFromObject found.");
    }
	
	// Update is called once per frame
	void Update () {
        float rotation_X = Input.GetAxis("Horizontal_2") * rotationRate;
        tranformToSpin.Rotate(tranformToSpin.up * rotation_X);
        float rotation_Y = Input.GetAxis("Vertical_2") * rotationRate;
        tranformToSpin.Rotate(tranformToSpin.right * rotation_Y);

        if (Input.GetAxisRaw("Fire1_2") != 0 && nextFire < Time.time) {
            nextFire = Time.time + fireRate;
            GameObject shot = (GameObject)Instantiate(projectilePrefab,shootTranform.position,shootTranform.rotation);
            shot.GetComponent<Rigidbody>().AddForce(shootTranform.forward * projectileSpeed);
        }
	}
}

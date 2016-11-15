using UnityEngine;
using System.Collections;

public class TurnCollider : MonoBehaviour
{

    [SerializeField]
    private bool isNeg = false;
    [SerializeField]
    private float rotRate = 1f;
    [SerializeField]
    private float rotWait = 0.1f;
    [SerializeField]
    private int rotDegrees = 90;

    private int isNegVal = 1;

    // Use this for initialization
    void Start () {
        SetInitialReferences();
    }

    private void SetInitialReferences () {
        if (isNeg) isNegVal = -1;
        rotRate = 1 / rotRate;
    }

    // Update is called once per frame
    void Update () {

    }

    private void OnTriggerEnter () {
        GameObject col = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(rotateTransform(col.transform));
    }

    private IEnumerator rotateTransform ( Transform tran ) {
        float i = 0;
        while (i < rotDegrees) {
            i += rotRate;
            if (i > rotDegrees) { i = rotDegrees; }
            tran.localRotation = Quaternion.Euler(0, i * isNegVal, 0);//Quaternion.Euler(tran.rotation.x, tran.rotation.y + i * isNegVal, tran.rotation.z);
            yield return new WaitForSeconds(rotWait);

        }
    }
}
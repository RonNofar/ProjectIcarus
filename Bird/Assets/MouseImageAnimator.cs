using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseImageAnimator : MonoBehaviour {

    public Image image;

    public float speed, range;

    Vector3 o_Position;

	// Use this for initialization
	void Start () {
        o_Position = image.transform.localPosition;
    }
	
	// Update is called once per frame
	void Update () {
        image.transform.localPosition = new Vector3(
            o_Position.x + Mathf.Sin(Time.time * speed) * range,
            o_Position.y,
            o_Position.z
            );
	}
}

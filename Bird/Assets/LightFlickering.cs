using UnityEngine;
using System.Collections;

public class LightFlickering : MonoBehaviour { // PLANS FOR FUTURE::: public list of gameobjects to change metarial, public metarial for new one (on light/or off), private curr metarial

    public GameObject lightObject;
    public bool isFlickering = true;
    public bool isRandom = true;
    public float timeOn = 1;
    public float timeOff = 3;
    public float minimumTime = 0.1f;

    private Light flickeringLight;
    private float flickerTime = 0;

	// Use this for initialization
	void Start () {
        SetInitialReferences();
    }

    void SetInitialReferences () {
        if (lightObject != null) flickeringLight = lightObject.GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (isFlickering) {
            if (Time.time > flickerTime) {
                flickeringLight.enabled = !flickeringLight.enabled;
                if (flickeringLight.enabled) {
                    float on = timeOn;
                    if (isRandom) on = Random.Range(minimumTime, timeOn);
                    flickerTime = Time.time + on;
                } else {
                    float off = timeOff;
                    if (isRandom) off = Random.Range(minimumTime, timeOff);
                    flickerTime = Time.time + off;
                }
            }
        }
	}
}

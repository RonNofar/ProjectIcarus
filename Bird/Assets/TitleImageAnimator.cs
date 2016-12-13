using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TitleImageAnimator : MonoBehaviour {

    public Image titleImage;
    public Image projectileImage;

    float r;
    float g;
    float b;

    float xScale;
    float yScale;
    float zScale;

    Quaternion orgRot;

    public float speed = 1;
    public float scaleSpeed = 0.1f;
    public float rotSpeed = 0.1f;
    public float rotAngle = 1;

    private float rotRadian;

    

    // Use this for initialization
    void Awake () {
        r = 1;
        g = 0;
        b = 0;
        //Debug.Log("r: " + r + " | g: " + g + " | b: " + b);

        xScale = titleImage.transform.localScale.x;
        yScale = titleImage.transform.localScale.y;
        zScale = titleImage.transform.localScale.z;
        //Debug.Log("xScale: " + xScale + " | yScale: " + yScale + " | zScale: " + zScale);

        orgRot = titleImage.transform.localRotation;

        rotRadian = rotAngle / 180;
    }
	
	// Update is called once per frame
	void Update () {
        float n_speed = speed * Time.deltaTime;
        if (r >= 1 && g < 1 && b <= 0) {
            r = 1;    g += n_speed; b = 0;
        }
        else if (r > 0 && g >= 1 && b <= 0) {
            r -= n_speed; g = 1;    b = 0;
        }
        else if (r <= 0 && g >= 1 && b < 1) {
            r = 0;      g = 1;    b += n_speed;
        }
        else if (r <= 0 && g > 0 && b >= 1) {
            r = 0;      g -= n_speed; b = 1;
        }
        else if (r < 1 && g <= 0 && b >= 1) {
            r += n_speed; g = 0;      b = 1;
        }
        else if (r >= 1 && g <= 0 && b > 0) {
            r = 1;    g = 0;      b -= n_speed;
        }
        else Debug.Log("Something went wrong in RGB flow!");
        Debug.Log("r: " + r + " | g: " + g + " | b: " + b);
        titleImage.color = new Color(r, g, b);

        
        //float pi = Mathf.PI;
        titleImage.transform.localScale = new Vector3(
            (Mathf.Sin(Time.timeSinceLevelLoad * scaleSpeed) + 4f)/4f,//(Mathf.Sin(Mathf.PingPong(Time.timeSinceLevelLoad, pi * 2)) + 2f)/2,
            (Mathf.Sin(Time.timeSinceLevelLoad * scaleSpeed) + 4f)/4f,//(Mathf.Sin(Mathf.PingPong(Time.timeSinceLevelLoad, pi * 2)) + 2f)/2,
            zScale);
        //Debug.Log(Time.time+" | "+titleImage.transform.localScale);
        
        titleImage.transform.localRotation = new Quaternion(
            orgRot.x,// * rotAngle,
            orgRot.y,//Mathf.Sin(Mathf.PingPong(Time.time, pi * 2)),
            Mathf.Sin(Time.timeSinceLevelLoad * rotSpeed) * 0.1f,
            orgRot.w
            );
        //titleImage.transform.localScale = new Vector2()
    }
     /*
    public float MyPingPong (float t, float length, float speed=10) {
        // speed is a ratio S.T. the smaller it is the faster this function will run
        float result = t;
        if (t<length)
        {
            t += length/speed;
        } else if (t)
        return result;
    }*/

    

}

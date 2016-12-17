using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProjectileScript : MonoBehaviour {
    
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

    public FloatRange velocity, direction, rotation;

    Transform m_Transform;
    Image m_Image;
    Vector4 m_Color;

    float r;



	// Use this for initialization
	void Start () {
        m_Transform = GetComponent<Transform>();
        m_Transform.Rotate(m_Transform.forward * direction.RandomInRange);

        m_Image = GetComponent<Image>();

        r = Mathf.Sin(Random.Range(0, Mathf.PI * 2));

    }
	
	// Update is called once per frame
	void Update () {
        m_Transform.Translate(m_Transform.up * velocity.RandomInRange * Time.deltaTime);
        m_Transform.Rotate(m_Transform.forward * rotation.RandomInRange * r * Time.deltaTime);
    }

    public void Fade (Vector4 color, float fadeRate) {
        Debug.Log("Entered Fade()");
        Vector4 newColor = new Vector4(color[0], color[1], color[2], 0);
        //float i = 0;
        
        for (float i=0; i<=10; i+=Time.deltaTime) {
            //Debug.Log("i: " + i);
            //if (i > 1) i = 1;
            m_Image.color = new Vector4(color[0], color[1], color[2], 1-(i/10));
            //i += fadeRate;
            Debug.Log("i: " + i);
        }
        //m_Image.color = new Vector4(color[0], color[1], color[2], 0.5f);
        //Destroy(this);
    }
}

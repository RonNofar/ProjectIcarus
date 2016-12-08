using UnityEngine;
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

    public FloatRange velocity, direction;

    Transform m_Transform;

	// Use this for initialization
	void Start () {
        m_Transform = GetComponent<Transform>();
        m_Transform.Rotate(m_Transform.forward * direction.RandomInRange);
	}
	
	// Update is called once per frame
	void Update () {
        m_Transform.Translate(m_Transform.up * velocity.RandomInRange * Time.deltaTime);
	}
}

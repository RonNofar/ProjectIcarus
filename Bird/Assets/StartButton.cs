using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {

    [System.Serializable]
    public struct FloatRange
    {
        public float min, max;

        public float RandomInRange {
            get {
                return Random.Range(min, max);
            }
        }
    }

    public FloatRange spawnRange, velocityRange, directionRange;

    float spawnTime;

    // Use this for initialization
    void Start () {
        spawnTime = Time.time + spawnRange.RandomInRange;
    }

    // Update is called once per frame
    void Update () {
        if (Time.time > spawnTime) {
            CreateProjectile(velocityRange.RandomInRange, directionRange.RandomInRange);
        }
	}

    void OnPointerEnter () {
        
    }

    void CreateProjectile (float velocity, float direction) {

    }
}

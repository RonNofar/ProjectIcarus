using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public Button btn;

    public FloatRange spawnRange, velocityRange, directionRange;

    float spawnTime;

    // Use this for initialization
    void Start () {
        spawnTime = Time.time + spawnRange.RandomInRange;

        if (btn != null) btn = btn.GetComponent<Button>();
        else Debug.Log("No Button Object attached!");

        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update () {
        /*
        if (Time.time > spawnTime) {
            CreateProjectile(velocityRange.RandomInRange, directionRange.RandomInRange);
        }*/
        
	}

    void TaskOnClick() {
        Debug.Log("Clicked");
        SceneManager.LoadScene("UFOLevelTest", LoadSceneMode.Single);
    }

    void OnPointerEnter () {
        
    }

    void CreateProjectile (float velocity, float direction) {

    }
}

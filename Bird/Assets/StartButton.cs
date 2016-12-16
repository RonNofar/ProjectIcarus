using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

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
    public GameObject parent;
    public GameObject prefab;

    public FloatRange spawnRange, velocityRange, directionRange, deathRange, scaleRange, fadeRange;

    Transform btn_tran;
    float spawnTime;

    bool isOn = false;
    bool isMoving = false;
    GameObject p;

    public Color[] colors;
    Image refImg;

    // Use this for initialization
    void Start () {

        if (btn != null) {
            btn_tran = btn.GetComponent<Transform>();
            btn = btn.GetComponent<Button>();
        }
        else Debug.Log("No Button Object attached!");

        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update () {
        if (isOn) {
            if (Time.time > spawnTime) {
                CreateProjectile(
                    prefab, 
                    velocityRange.RandomInRange, 
                    directionRange.RandomInRange, 
                    deathRange.RandomInRange,
                    scaleRange.RandomInRange,
                    fadeRange.RandomInRange
                    );
                spawnTime = Time.time + spawnRange.RandomInRange;
            }
        }
        //if (p != null) p.GetComponent<Transform>().Translate(p.GetComponent<Transform>().up * Time.deltaTime * 10);
	}

    void TaskOnClick() {
        //Debug.Log("Clicked");
        SceneManager.LoadScene("UFOLevelTest", LoadSceneMode.Single);
    }

    public void OnPointerEnter (PointerEventData eventData) {
        isOn = true;
        Debug.Log("isOn: " + isOn);
    }

    public void OnPointerExit (PointerEventData eventData) {
        isOn = false;
    }

    void CreateProjectile (GameObject prefab, float velocity, 
        float direction, float death, float scale, float fade) {
        //float deathTime = deathRange.RandomInRange;
        //Debug.Log(direction);
        p = (GameObject)Instantiate(
            prefab, 
            btn_tran.position,
            btn_tran.rotation
        );
        
        Transform pt = p.GetComponent<Transform>();
        pt.localScale = new Vector3(scale, scale, scale);
        Image img = p.GetComponent<Image>();
        img.color = colors[Random.Range(0, colors.Length)];
        Vector4 color = img.color;
        img.color = new Vector4(color[0], color[1], color[2], 1);
        pt.SetParent(parent.GetComponent<Transform>());
        //p.GetComponent<Rigidbody>().AddForce(p.GetComponent<Transform>().up * velocity);
        StartCoroutine(KillProjectile(p, death, color, fade));
        //float spawnTime = Time.time;
        //float currTime = 0;
        //Debug.Log(Time.time);
        isMoving = true;
        /*
        while (death > currTime) {
            pt.Translate(pt.up * velocity * Time.deltaTime);
            currTime += 0.0001f;//= Time.time - spawnTime;
            Debug.Log(currTime);
        }*/
    }

    IEnumerator KillProjectile (GameObject o, float secs, Vector4 color, float fadeRate) {
        yield return new WaitForSeconds(secs);
        //o.GetComponent<ProjectileScript>().Fade(color, fadeRate); // handles Destroy(o);
        /*
        float i = 1;
        while (i>=0) {
            Debug.Log("i: " + i);
            //if (i <= 0) i = 0; break;
            img.color = new Vector4(color[0], color[1], color[2], i);
            i -= fadeRate;
            yield return null;
            
        }*/
        Destroy(o);
    }
}

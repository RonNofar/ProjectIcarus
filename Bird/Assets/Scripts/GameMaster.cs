using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    static public bool isGameStarted = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.R)) Restart();
	}

    void Restart () {
        SceneManager.LoadScene("UFOLevelTest");//Application.LoadLevel(Application.loadedLevel);
        isGameStarted = false;
        PlayerMaster.isPlayerDead = false;
    }

    public GameObject[] FindObjectsWithTag ( string tag ) {
        return GameObject.FindGameObjectsWithTag(tag);
    }

}

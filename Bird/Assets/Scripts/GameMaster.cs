using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    static public bool isGameStarted = false;
    static public bool isGamePaused = false;
    static public bool isGameOver = false;
    static public bool isGameWon = false;

	// Use this for initialization
	void Start () {
	}


	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.R)) Restart();
        if (Input.GetKey(KeyCode.S)) Time.timeScale = 0.5f;
        else Time.timeScale = 1f;

        if (Input.GetKeyUp(KeyCode.P)) UFOController.score += Random.Range(1, 100);
        if (Input.GetKeyUp(KeyCode.O)) UFOController.score -= Random.Range(1, 100);
    }

    static public void ReinitializeReferences() {
        isGameStarted = false;
        isGamePaused = false;
        isGameOver = false;
        isGameWon = false;
        UFOController.isMovementStarted = false;
        UFOController.isPlayerDead = false;
        UFOController.isVisible = true;
    }

    static public void Restart () {
        ReinitializeReferences();
        SceneManager.LoadSceneAsync("UFOLevelTest", LoadSceneMode.Single);//Application.LoadLevel(Application.loadedLevel);
    }

    public GameObject[] FindObjectsWithTag ( string tag ) {
        return GameObject.FindGameObjectsWithTag(tag);
    }

}

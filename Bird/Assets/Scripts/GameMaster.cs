using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    static public bool isGameStarted = false;
    static public bool isGamePaused = false;
    static public bool isGameOver = false;
    static public bool isGameWon = false;

    static public bool isRestart = false;

    static private GameMaster instance;

	void Awake () {
        instance = this;
        ReinitializeReferences();
	}


	
	void Update () {
        if (Input.GetKeyUp(KeyCode.R)) isRestart = true;
        if (Input.GetKey(KeyCode.S)) Time.timeScale = 0.5f;
        else Time.timeScale = 1f;

        if (Input.GetKeyUp(KeyCode.P)) UFOController.score += Random.Range(1, 100);
        if (Input.GetKeyUp(KeyCode.O)) UFOController.score -= Random.Range(1, 100);
        if (isRestart) Restart(); // used to bypass Button Listner problem calling a function
    }

    static public void ReinitializeReferences() {
        isGameStarted = false;
        isGamePaused = false;
        isGameOver = false;
        isGameWon = false;
        isRestart = false;
        /* now inside UFOController's Awake function
        UFOController.isMovementStarted = false;
        UFOController.isPlayerDead = false;
        UFOController.isVisible = true;
        */
    }

    static public void Restart () {
        //ReinitializeReferences();
        SceneManager.LoadSceneAsync("UFOLevelTest", LoadSceneMode.Single);//Application.LoadLevel(Application.loadedLevel);
    }

    static public void LoadMainMenu() {

    }

    public GameObject[] FindObjectsWithTag ( string tag ) {
        return GameObject.FindGameObjectsWithTag(tag);
    }

}

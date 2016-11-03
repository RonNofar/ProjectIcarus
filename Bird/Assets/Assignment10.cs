using UnityEngine;
using System.Collections;

public class Assignment10 : MonoBehaviour {

    private GameObject mainPlayer;
    private PlayerMaster playerMaster;
    private GameObject gameMaster;

    // Use this for initialization
    void Start () {
        SetInitialReferences();
	}

    void SetInitialReferences () {
        mainPlayer = GameObject.FindWithTag("Player");
        playerMaster = mainPlayer.GetComponentInChildren<PlayerMaster>();
        Debug.Log("DeathCheck: "+playerMaster.getDeathCheck());
        WaitForSecs(10);
        gameMaster = playerMaster.findGameObjectOfName("MisterMaster");
        GameMaster script = gameMaster.GetComponent<GameMaster>();
        GameObject[] array = script.FindObjectsWithTag("Obstacles");
        foreach (GameObject tagged in array) {
            Debug.Log("Obstacle: "+tagged.name);
        }
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    IEnumerator WaitForSecs (int secs) {
        yield return new WaitForSeconds(secs);
        Debug.Log("Has waited " + secs + " seconds");
    }
}

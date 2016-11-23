/*
    OBSOLETE CODE - Check http://catlikecoding.com/unity/tutorials/object-pools/ to finish
*/

using UnityEngine;
using System.Collections;

public class SceneSwitcher : MonoBehaviour {

	public void SwitchScene () {
        int nextLevel = (Application.loadedLevel + 1) % Application.levelCount;
        Application.LoadLevel(nextLevel);
    }
}

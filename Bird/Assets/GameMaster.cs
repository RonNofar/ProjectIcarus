using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

    public int integer = 1;
    public float flt = 0.1f;
    public string str = "This is a string.";
    public bool bl = true;
    public string[] stringArr = {"Zero","One","Two","Three"};
    public enum directions { NORTH,SOUTH,EAST,WEST };


	// Use this for initialization
	void Start () {
        print();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.anyKey)
        {
            print();
        }
	}

    void print()
    {
        Debug.Log("integer: " + integer);
        Debug.Log("float: " + flt);
        Debug.Log("string: " + str);
        Debug.Log("bool: " + bl);
        if (bl)
        {
            for (int i = 0; i < stringArr.Length; ++i)
            {
                Debug.Log("Array index [" + i + "]: " + stringArr[i]);
            }
        }
    }
}

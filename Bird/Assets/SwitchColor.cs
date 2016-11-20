using UnityEngine;
using System.Collections;

public class SwitchColor : MonoBehaviour {

    public Material nm;

    private MeshRenderer mr;
    private Material om;

	// Use this for initialization
	void Start () {
        mr = GetComponent<MeshRenderer>();
        om = mr.material;
        SetColor(nm);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetColor(Material color) {
        mr.material = color;
    }
}

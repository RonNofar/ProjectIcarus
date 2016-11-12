using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour {

    private readonly Vector3[] directions = {Vector3.up,Vector3.right,Vector3.forward};

    public Mesh mesh;
    public Material material;

    public int maxDepth;

    private int depth;

    public float childScale;

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;
        if (depth < maxDepth) {
            StartCoroutine(CreateChildren());
        }
    }

    void Initialize ( Fractal parent, Vector3 direction ) {
        mesh = parent.mesh;
        material = parent.material;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1;
        childScale = parent.childScale;

        transform.parent = parent.transform;
        transform.localScale = Vector3.one * childScale;
        transform.localPosition = direction * (0.5f + 0.5f * childScale);

    }
	
    private IEnumerator CreateChildren () {
        for (int i = 0; i < directions.Length; ++i) {
            yield return new WaitForSeconds(0.5f);
            new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, directions[i]);
            yield return new WaitForSeconds(0.5f);
            new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, -directions[i]);

        }


    }
}

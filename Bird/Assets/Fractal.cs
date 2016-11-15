using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour {

    private readonly Vector3[] childDirections = {
        Vector3.up,
        Vector3.right,
        Vector3.left,
        Vector3.forward,
        Vector3.back
    };

    private readonly Quaternion[] childOrientations = {
        Quaternion.identity,
        Quaternion.Euler(0,0,90),
        Quaternion.Euler(0,0,-90),
        Quaternion.Euler(90,0,0),
        Quaternion.Euler(-90,0,0)
    };

    private Material[,] materials;

    public Mesh[] meshes;
    public Material material;

    public int maxDepth;

    private int depth;

    public float childScale;

    public float spawnProbability;

    public float maxRotationSpeed;
    private float rotationSpeed;

    public float maxTwist;

    private Transform m_Transform;

    private void Awake () {
        m_Transform = GetComponent<Transform>();
    }

	private void Start () {
        if (materials == null) { // PUTTING THIS IN AWAKE BREAKS SCRIPT (UNSET PUBLIC REFERENCE BEFORE START)
            if (material != null) InitializeMaterials();
            else Debug.Log("material is null");
        }
        SetInitialReferences();
        m_Transform.Rotate(Random.Range(-maxTwist, maxTwist),0,0);
        if (depth < maxDepth) {
            StartCoroutine(CreateChildren());
        }
    }

    private void SetInitialReferences () {
        rotationSpeed = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        gameObject.AddComponent<MeshFilter>().mesh = meshes[Random.Range(0, meshes.Length)];
        gameObject.AddComponent<MeshRenderer>().material = materials[depth, Random.Range(0, 2)];
        //GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.white, Color.yellow, (float)depth / maxDepth);
    }

    private void InitializeMaterials () {
        materials = new Material[maxDepth + 1, 2];
        for (int i = 0; i <= maxDepth; ++i) {
            float t = (float)i / (maxDepth - 1);
            t *= t;
            materials[i, 0] = new Material(material);
            materials[i, 0].color = Color.Lerp(Color.white, Color.yellow, t);
            materials[i, 1] = new Material(material);
            materials[i, 1].color = Color.Lerp(Color.white, Color.cyan, t);
        }
        materials[maxDepth, 0].color = Color.magenta;
        materials[maxDepth, 1].color = Color.red;
    }

    private void Initialize ( Fractal parent, int childIndex ) {
        meshes = parent.meshes;
        materials = parent.materials;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1;
        childScale = parent.childScale;
        spawnProbability = parent.spawnProbability;
        maxRotationSpeed = parent.maxRotationSpeed;

        m_Transform.parent = parent.transform;
        m_Transform.localScale = Vector3.one * childScale;
        m_Transform.localPosition = 
            childDirections[childIndex] * (0.5f + 0.5f * childScale);
        m_Transform.localRotation = childOrientations[childIndex];

    }
	
    private IEnumerator CreateChildren () {
        for (int i = 0; i < childDirections.Length; ++i) {
            if (Random.value < spawnProbability) {
                yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
                new GameObject("Fractal Child").AddComponent<Fractal>().
                    Initialize(this, i);
            }
        }
    }

    private void Update () {
        m_Transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}

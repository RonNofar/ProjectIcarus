using UnityEngine;
using System.Collections;

public class StuffSpawner : MonoBehaviour {

    [System.Serializable] public struct FloatRange
    {
        public float min, max;

        public float RandomInRange {
            get {
                return Random.Range(min, max);
            }
        }
    }

    public FloatRange timeBetweenSpawns, scale, randVelocity, angularVelocity;

    public float velocity;

    public Material stuffMaterial;

    public Stuff[] stuffPrefabs;

    float timeSinceLastSpawn;

    float currSpawnDelay;

	void FixedUpdate () {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= currSpawnDelay) {
            timeSinceLastSpawn -= currSpawnDelay;
            currSpawnDelay = timeBetweenSpawns.RandomInRange;
            SpawnStuff();
        }
    }

    void SpawnStuff () {
        Stuff prefab = stuffPrefabs[Random.Range(0, stuffPrefabs.Length)];
        Stuff spawn = prefab.GetPooledInstance<Stuff>();

        spawn.transform.localPosition = transform.position;
        spawn.transform.localScale = Vector3.one * scale.RandomInRange;
        spawn.transform.localRotation = Random.rotation;

        spawn.Body.velocity = transform.up * velocity +
            Random.onUnitSphere * randVelocity.RandomInRange;
        spawn.Body.angularVelocity = 
            Random.onUnitSphere * angularVelocity.RandomInRange;

        spawn.GetComponent<MeshRenderer>().material = stuffMaterial;
    }
}

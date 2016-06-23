using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenericGridObjectGeneratorScript : MonoBehaviour {
    public Vector3 minPosition;
    public Vector3 maxPosition;
    public Vector3 gridSize = new Vector3(1, 1, 3);

    public float density = 0.12f;
    public bool relative = true;
    public bool destroyWhenDestroyed = true;

    public GameObject[] prefabs;

    private List<GameObject> generatedObjects;

    public void Start() {
        generatedObjects = new List<GameObject>();

        for (var x = minPosition.x; x <= maxPosition.x; x += gridSize.x) {
            for (var y = minPosition.y; y <= maxPosition.y; y += gridSize.y) {
                for (var z = minPosition.z; z <= maxPosition.z; z += gridSize.z) {
                    bool generate = Random.value < density;
                    if (generate) {
                        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
                        var o = (GameObject)Instantiate(prefab, relative ? transform.position + new Vector3(x, y, z) : new Vector3(x, y, z), Quaternion.identity);

                        generatedObjects.Add(o);
                        OnInstantiate(o);
                    }
                }
            }
        }
	}

    public void OnDestroy() {
        if (destroyWhenDestroyed) {
            foreach (var o in generatedObjects) {
                Destroy(o);
            }
        }
    }
        
    protected virtual void OnInstantiate(GameObject o) {
    }
}

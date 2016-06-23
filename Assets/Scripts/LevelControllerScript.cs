using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelControllerScript : MonoBehaviour {
    public int minZ = 3;
    public int lineAhead = 40;
    public int lineBehind = 20;

    public GameObject[] linePrefabs;
    public GameObject coins;

    private Dictionary<int, GameObject> lines;

    private GameObject player;

    public void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        lines = new Dictionary<int, GameObject>();
	}
	
    public void Update() {
        // Generate lines based on player position.
        var playerZ = (int)player.transform.position.z;
        for (var z = Mathf.Max(minZ, playerZ - lineBehind); z <= playerZ + lineAhead; z += 1) {
            if (!lines.ContainsKey(z)) {
                GameObject coin;
                int x = Random.Range(0, 2);
                if (x == 1) {
                    coin = (GameObject)Instantiate(coins);
                    int randX = Random.Range(-4, 4);
                    coin.transform.position = new Vector3(randX, 1, 1.5f);
                }

                var line = (GameObject)Instantiate(
                    linePrefabs[Random.Range(0, linePrefabs.Length)],
                    new Vector3(0, 0, z * 3 - 5), 
                    Quaternion.identity
                );

                line.transform.localScale = new Vector3(1, 1, 3);
                lines.Add(z, line);
            }
        }

        // Remove lines based on player position.
        foreach (var line in new List<GameObject>(lines.Values)) {
            var lineZ = line.transform.position.z;
            if (lineZ < playerZ - lineBehind) {
                lines.Remove((int)lineZ);
                Destroy(line);
            }
        }
	}

    public void Reset() {
        // TODO This kind of reset is dirty, refactor might be needed.
        if (lines != null) {
            foreach (var line in new List<GameObject>(lines.Values)) {
                Destroy(line);
            }
            Start();
        }
    }
}

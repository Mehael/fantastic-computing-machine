using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    public GameObject[] possiblePrefabs;
    public float spawnCooldown = 1f;
    public float delayToStartSpawning = 1f;
	Transform thisObj;

    void Start()
    {
        InvokeRepeating("Spawn", delayToStartSpawning, spawnCooldown);
		thisObj = transform;
	}

    void Spawn ()
    {
        int enemyIndex = Random.Range(0, possiblePrefabs.Length);
        var go = (GameObject)Instantiate(possiblePrefabs[enemyIndex], transform.position, Quaternion.identity);
		go.transform.SetParent(thisObj);
	}
}

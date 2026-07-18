using UnityEngine;
using System.Collections.Generic;

public class KeycardSpawner : MonoBehaviour
{
    public GameObject keycardObject;
    private List<Transform> spawnPoints = new List<Transform>();

    private void Awake() {
        // Find all children named SpawnPoint_
        foreach(Transform child in transform) {
            if (child.name.StartsWith("SpawnPoint_")) {
                spawnPoints.Add(child);
            }
        }
    }

    public void SpawnKeycard()
    {
        if (keycardObject == null) return;
        
        if (spawnPoints.Count > 0) {
            int randomIndex = Random.Range(0, spawnPoints.Count);
            Transform targetSpawn = spawnPoints[randomIndex];
            
            // Just use the exact position of the SpawnPoint the user placed!
            keycardObject.transform.position = targetSpawn.position;
            keycardObject.SetActive(true);
            
            Debug.Log("Keycard spawned at user's spawn point: " + targetSpawn.name);
        }
    }
}

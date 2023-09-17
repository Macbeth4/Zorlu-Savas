using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canspawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject objectPrefab;
    public float spawnInterval = 5f;
    public float destroyDelay = 10f;

    private void Start()
    {
        InvokeRepeating("SpawnObject", 0f, spawnInterval);
    }

    private void SpawnObject()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];
        GameObject spawnedObject = Instantiate(objectPrefab, spawnPoint.position, spawnPoint.rotation);
        Destroy(spawnedObject, destroyDelay);
    }
}

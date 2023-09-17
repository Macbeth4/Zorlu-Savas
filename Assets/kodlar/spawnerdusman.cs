using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerdusman : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnDelay = 2f;
    private float lastSpawnTime = 0f;

    void Update()
    {
        if (Time.time - lastSpawnTime > spawnDelay)
        {
            SpawnObject();
            lastSpawnTime = Time.time;
        }
    }

    public GameObject objectPrefab;
    public Transform centerObject;
    public float radius = 5f;

    void SpawnObject()
    {
        float angle = Random.Range(0f, 360f);
        float xPos = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
        float yPos = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;

        Vector3 spawnPosition = centerObject.position + new Vector3(xPos, yPos, 0f);
        Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
    }

 //   void SpawnEnemy()
   // {
    //    float randomAngle = Random.Range(0f, 360f);
     //   Vector3 spawnPosition = Quaternion.Euler(0f, 0f, randomAngle) * Vector3.up * 5f;
      //  GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        
   // }
    
}
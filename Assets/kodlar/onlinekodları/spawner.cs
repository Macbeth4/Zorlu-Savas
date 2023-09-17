using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class spawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public Transform[] spawnPoints;
    public float spawnInterval = 2f;

    private float spawnTimer;
    private GameObject[] spawnedObjects;
    PhotonView pw;
    private void Start()
    {
        pw = GetComponent<PhotonView>();
        spawnTimer = spawnInterval;
        spawnedObjects = new GameObject[objectsToSpawn.Length];
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            pw.RPC("SpawnObjects", RpcTarget.All);
            spawnTimer = spawnInterval;
        }
    }

    [PunRPC]
    public void SpawnObjects()
    {
        for (int i = 0; i < objectsToSpawn.Length; i++)
        {
            if (spawnedObjects[i] == null)
            {
                int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
                spawnedObjects[i] = PhotonNetwork.Instantiate(objectsToSpawn[i].name, spawnPoints[randomSpawnIndex].position, spawnPoints[randomSpawnIndex].rotation);
            }
        }
    }

    private bool HasActiveObjects()
    {
        for (int i = 0; i < spawnedObjects.Length; i++)
        {
            if (spawnedObjects[i] != null && spawnedObjects[i].activeInHierarchy)
            {
                return true;
            }
        }
        return false;
    }
    
}
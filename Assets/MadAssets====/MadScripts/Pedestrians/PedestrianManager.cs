using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PedestrianManager : MonoBehaviour
{
    [Header("Pedestrian Prefabs")]
    public GameObject[] pedestrianPrefabs; // Assign 3 prefabs here

    [Header("Spawn Timing")]
    public float minSpawnInterval = 2f;
    public float maxSpawnInterval = 5f;

    [Header("Path Settings")]
    public Transform spawnPoint;
    public Transform endPoint;
    public List<Transform> waypoints;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnRandomPedestrian();

            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }

    void SpawnRandomPedestrian()
    {
        if (spawnPoint == null || pedestrianPrefabs == null || pedestrianPrefabs.Length == 0) return;

        int randomIndex = Random.Range(0, pedestrianPrefabs.Length);
        GameObject prefab = pedestrianPrefabs[randomIndex];

        GameObject pedestrian = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        Pedestrian pedScript = pedestrian.GetComponent<Pedestrian>();

        List<Transform> fullPath = new List<Transform>();
        fullPath.Add(spawnPoint);
        fullPath.AddRange(waypoints);
        fullPath.Add(endPoint);

        pedScript.SetPath(fullPath);
    }
}

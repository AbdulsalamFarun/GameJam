using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PedestrianManager : MonoBehaviour
{
    public GameObject pedestrianPrefab;

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
            SpawnPedestrian();

            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }

    void SpawnPedestrian()
    {
        if (spawnPoint == null || pedestrianPrefab == null) return;

        GameObject pedestrian = Instantiate(pedestrianPrefab, spawnPoint.position, spawnPoint.rotation);
        Pedestrian pedScript = pedestrian.GetComponent<Pedestrian>();

        List<Transform> fullPath = new List<Transform>();
        fullPath.Add(spawnPoint);
        fullPath.AddRange(waypoints);
        fullPath.Add(endPoint);

        pedScript.SetPath(fullPath);
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PedestrianManager : MonoBehaviour
{
    public GameObject pedestrianPrefab;
    public float spawnInterval = 3f;

    [Header("Path Settings")]
    public Transform spawnPoint;
    public Transform endPoint;
    public List<Transform> waypoints = new List<Transform>();

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            GameObject ped = Instantiate(pedestrianPrefab, spawnPoint.position, Quaternion.identity);
            Pedestrian pedestrianScript = ped.GetComponent<Pedestrian>();

            List<Transform> fullPath = new List<Transform>();
            fullPath.Add(spawnPoint);
            fullPath.AddRange(waypoints);
            fullPath.Add(endPoint);

            pedestrianScript.SetPath(fullPath);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}

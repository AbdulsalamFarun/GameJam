using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<Transform> spawnPoints;               // Assign 20 points in the Inspector
    public List<GameObject> itemPrefabs;              // Assign burger, cheese, steak prefabs
    public int itemsToSpawn = 10;

    private List<GameObject> spawnedItems = new List<GameObject>();

    void Start()
    {
        RespawnItems();
    }

    public void RespawnItems()
    {
        // Destroy previous items
        foreach (GameObject item in spawnedItems)
        {
            if (item != null)
                Destroy(item);
        }
        spawnedItems.Clear();

        // Pick 10 unique random spawn points
        List<Transform> availablePoints = new List<Transform>(spawnPoints);
        for (int i = 0; i < itemsToSpawn && availablePoints.Count > 0; i++)
        {
            int pointIndex = Random.Range(0, availablePoints.Count);
            Transform spawnPoint = availablePoints[pointIndex];
            availablePoints.RemoveAt(pointIndex);

            GameObject prefab = itemPrefabs[Random.Range(0, itemPrefabs.Count)];
            GameObject spawned = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            spawnedItems.Add(spawned);
        }
    }
}
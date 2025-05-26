using UnityEngine;

public class RespawnableItem : MonoBehaviour
{
    public GameObject prefab; 
    public Transform[] spawnPoints; 

    public GameObject GetPrefab()
    {
        return prefab != null ? prefab : this.gameObject;
    }

    public Transform GetRandomSpawnPoint()
    {
        if (spawnPoints.Length == 0) return null;
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }
}

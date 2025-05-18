using UnityEngine;

public class PedestrianSpawner : MonoBehaviour
{
    public GameObject pedestrianPrefab;
    public Transform leftSpawn;
    public Transform rightSpawn;
    public Transform leftTarget;
    public Transform rightTarget;

    public float spawnInterval = 2f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnPedestrian();
            timer = 0f;
        }
    }

    void SpawnPedestrian()
    {
        bool spawnLeft = Random.value > 0.5f;

        if (spawnLeft)
        {
            GameObject ped = Instantiate(pedestrianPrefab, leftSpawn.position, Quaternion.identity);
            ped.GetComponent<Pedestrian>().target = rightTarget;
        }
        else
        {
            GameObject ped = Instantiate(pedestrianPrefab, rightSpawn.position, Quaternion.identity);
            ped.GetComponent<Pedestrian>().target = leftTarget;
            ped.transform.localScale = new Vector3(-1, 1, 1); // Flip if needed
        }
    }
}

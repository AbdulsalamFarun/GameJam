using UnityEngine;
using System.Collections.Generic;

public class Pedestrian : MonoBehaviour
{
    private List<Transform> path;
    private int currentIndex = 0;
    public float speed = 2f;

    public void SetPath(List<Transform> waypoints)
    {
        path = waypoints;
        currentIndex = 0;
        transform.position = path[0].position;
    }

    void Update()
    {
        if (path == null || currentIndex >= path.Count) return;

        Transform target = path[currentIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentIndex++;
            if (currentIndex >= path.Count)
            {
                Destroy(gameObject); // Pedestrian disappears
            }
        }
    }
}

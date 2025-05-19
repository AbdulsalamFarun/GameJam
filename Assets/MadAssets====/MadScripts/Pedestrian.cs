using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class Pedestrian : MonoBehaviour
{
    private NavMeshAgent agent;
    private List<Transform> path;
    private int currentIndex = 0;

    public void SetPath(List<Transform> waypoints)
    {
        path = waypoints;
        if (path == null || path.Count < 2) return;

        currentIndex = 1;
        Vector3 startPos = path[0].position;
        startPos.y = 0.1f; // Keep it on NavMesh
        transform.position = startPos;

        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = false;
        agent.SetDestination(path[currentIndex].position);
    }

    void Update()
    {
        if (path == null || currentIndex >= path.Count) return;

        if (!agent.pathPending && agent.remainingDistance < 0.2f)
        {
            currentIndex++;
            if (currentIndex >= path.Count)
            {
                Destroy(gameObject);
            }
            else
            {
                agent.SetDestination(path[currentIndex].position);
            }
        }
    }
}

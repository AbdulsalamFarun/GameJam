using UnityEngine;

public class Pedestrian : MonoBehaviour
{
    public float speed = 2f;
    public Transform target;

    void Update()
    {
        if (target == null) return;

        // Move toward target
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Destroy when reached
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}

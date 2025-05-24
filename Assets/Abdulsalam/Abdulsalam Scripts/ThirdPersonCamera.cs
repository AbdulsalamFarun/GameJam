using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;                // Player
    public float distance = 6f;             // Current zoom distance
    public float minDistance = 1f;          // Closest zoom
    public float maxDistance = 3f;         // Farthest zoom
    public float zoomSpeed = 150f;            // How fast to zoom

    public float mouseSensitivity = 3f;
    public float minY = 5f;
    public float maxY = 80f;

    private float yaw = 0f;
    private float pitch = 20f;

    void Start()
    {
        // Hide and lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Mouse look
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minY, maxY);

        // Mouse scroll zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        // Calculate camera position
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 direction = rotation * Vector3.back * distance;
        Vector3 cameraPosition = target.position + direction;

        transform.position = cameraPosition;
        transform.LookAt(target.position);
    }
}

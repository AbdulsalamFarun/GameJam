using UnityEngine;

public class RatController : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public Transform cameraTransform;
    public Animator animator;
    public float rotationSpeed = 10f;
    public float raycastDistance = 1.5f;

    private Rigidbody rb;
    private Vector3 moveInput;
    private float currentSpeed;
    private bool isRunning;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        moveInput = (camForward * v + camRight * h).normalized;

        isRunning = Input.GetKey(KeyCode.LeftShift);
        currentSpeed = isRunning ? runSpeed : walkSpeed;

        // Animator
        animator.SetFloat("Speed", moveInput.magnitude);
        animator.SetBool("IsRunning", isRunning);
    }

    void FixedUpdate()
    {
        // Raycast to detect slope normal
        Ray ray = new Ray(transform.position + Vector3.up * 0.2f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance))
        {
            // Get the slope normal
            Vector3 slopeNormal = hit.normal;

            // Project movement on the slope
            Vector3 moveDirection = Vector3.ProjectOnPlane(moveInput, slopeNormal).normalized;

            // Move the character
            Vector3 moveVelocity = moveDirection * currentSpeed;
            rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

            // Rotate character to match movement direction
            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection, slopeNormal);
                rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            }
        }
    }
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RatController : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.3f;
    public LayerMask groundLayer;
    public Transform cameraTransform;
    public Animator animator;

    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 moveInput;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Input
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Calculate direction relative to camera
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        moveInput = (camForward * v + camRight * h).normalized;

        // Set running
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        animator.SetBool("IsRunning", isRunning);

        // Set current speed
        currentSpeed = isRunning ? runSpeed : walkSpeed;

        // Animator speed parameter (for transitions)
        float movementAmount = moveInput.magnitude;
        animator.SetFloat("Speed", movementAmount);

        
    }

    void FixedUpdate()
    {
        // Move the player
        Vector3 moveVelocity = moveInput * currentSpeed;
        Vector3 newPosition = rb.position + moveVelocity * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);

        // Optional: face move direction
        if (moveInput != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveInput, Vector3.up);
            rb.rotation = Quaternion.RotateTowards(rb.rotation, toRotation, 720 * Time.fixedDeltaTime);
        }
    }
}

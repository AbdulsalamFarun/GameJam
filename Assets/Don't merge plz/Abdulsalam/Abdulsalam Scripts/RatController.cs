using UnityEngine;

public class RatController : MonoBehaviour
{
    public static RatController Instance { get; private set; }
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public Transform cameraTransform;
    public Animator animator;
    public float rotationSpeed = 10f;
    public float raycastDistance = 1.5f;
    public float jumpForce = 5f;

    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.3f;
    private bool isGrounded;

    private Rigidbody rb;
    private Vector3 moveInput;
    private float currentSpeed;
    private bool isRunning;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log(!isGrounded);
        isGrounded = true;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

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

        // Animator basic movement
        animator.SetFloat("Speed", moveInput.magnitude);
        animator.SetBool("IsRunning", isRunning);

        // Jump logic
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetBool("IsJumping", true);

        }

        // Set Falling animation
        if (!isGrounded && rb.linearVelocity.y < -0.1f)
        {
            animator.SetBool("IsFalling", true);
        }
        else
        {
            animator.SetBool("IsFalling", false);

        }

        animator.SetBool("IsJumping", !isGrounded);
    }


    void FixedUpdate()
    {
        // Movement
        Vector3 moveVelocity = moveInput * currentSpeed;
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

        // Rotation
        if (moveInput != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput, Vector3.up);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}

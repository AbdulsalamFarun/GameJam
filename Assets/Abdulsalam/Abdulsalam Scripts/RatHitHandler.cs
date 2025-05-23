using UnityEngine;

public class RatHitHandler : MonoBehaviour
{
    public float stunDuration = 2f;
    public SkinnedMeshRenderer ratRenderer; // Assign in inspector
   
    private bool isStunned = false;
    private float stunTimer = 0f;
    private RatController ratController; // Reference to movement script

    private Animator animator;


    void Start()
    {
        ratController = GetComponent<RatController>();
        animator = GetComponent<Animator>();
        

    }

    void Update()
    {
        if (isStunned)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0f)
            {
                EndStun();
            }
        }
    }

    public void TakeHit()
    {
        if (!isStunned)
        {
            StartStun();
        }
    }

    private void StartStun()
    {
        isStunned = true;
        stunTimer = stunDuration;

        // Disable movement
        if (ratController != null)
        {
            ratController.enabled = false;
            animator.SetTrigger("StunStart");
        }

        
    }

    private void EndStun()
    {
        isStunned = false;

        // Enable movement again
        if (ratController != null)
        {
            ratController.enabled = true;
            animator.SetTrigger("StunEnd");
        }

       
    }

    // Optional: detect hit using trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeHit();
        }
    }
}

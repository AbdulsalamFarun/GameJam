using UnityEngine;

public class RatHitHandler : MonoBehaviour
{
    public float stunDuration = 2f;
    public SkinnedMeshRenderer ratRenderer; // Assign in inspector
                // Assign in inspector or via tag

    private bool isStunned = false;
    private float stunTimer = 0f;
    private RatController ratController;
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

        if (ratController != null)
        {
            ratController.enabled = false;
            animator.SetTrigger("StunStart");
        }
    }

    private void EndStun()
    {
        isStunned = false;

        if (ratController != null)
        {
            ratController.enabled = true;
            animator.SetTrigger("StunEnd");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeHit();
        }
    }
}

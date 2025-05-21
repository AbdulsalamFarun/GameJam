using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Detection Settings")]
    public float viewRadius = 10f;
    public float viewAngle = 90f;
    public Transform eyePosition;
    public LayerMask playerMask;
    public LayerMask obstacleMask;

    [Header("Movement Speeds")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;

    

    [Header("Chase Behavior")]
    public bool canGiveUp = true;
    public bool startChasing = false;

    [HideInInspector] public Transform playerTransform;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator animator;

    

    public float patrolRange = 10f;
    public float minIdleTime = 2f;
    public float maxIdleTime = 5f;

    private Vector3 patrolCenter;

    private IEnemyState currentState;
    public IdleState idleState = new IdleState();
    public PatrolState patrolState = new PatrolState();
    public ChaseState chaseState = new ChaseState();
    public AttackState attackState = new AttackState();

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        patrolCenter = transform.position;

        if (startChasing)
        SwitchState(chaseState);
        else
        SwitchState(patrolState);
    }

    void Update()
    {
        currentState?.UpdateState();
    }

    public void SwitchState(IEnemyState newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState.EnterState(this);
    }

    public Vector3 GetRandomPatrolPoint()
    {
        Vector3 randomPoint = patrolCenter + new Vector3(
            Random.Range(-patrolRange, patrolRange),
            0,
            Random.Range(-patrolRange, patrolRange)
        );

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 2f, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return transform.position;
    }

    public bool PlayerInRange()
    {
        if (playerTransform == null) return false;

        Vector3 dirToPlayer = (playerTransform.position - eyePosition.position).normalized;
        float distanceToPlayer = Vector3.Distance(eyePosition.position, playerTransform.position);

        // 1. Check close detection sphere
        if (distanceToPlayer < 2f) // small radius auto-detect
        {
            if (!Physics.Raycast(eyePosition.position, dirToPlayer, distanceToPlayer, obstacleMask))
            {
                return true;
            }
        }

        // 2. Check field of view cone
        if (distanceToPlayer < viewRadius)
        {
            float angleToPlayer = Vector3.Angle(eyePosition.forward, dirToPlayer);
            if (angleToPlayer < viewAngle / 2f)
            {
                if (!Physics.Raycast(eyePosition.position, dirToPlayer, distanceToPlayer, obstacleMask))
                {
                    return true;
                }
            }
        }

        return false;
    }


    void OnDrawGizmosSelected() //Sphere Gizmo
    {
        if (eyePosition == null) return;

        // Draw sphere to represent detection range (the small detection sphere)
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(eyePosition.position, 2f); // Adjust 3f for your sphere radius
    }

    void OnDrawGizmos() //RayCast Gizmo
    {
        if (eyePosition == null) return;

        // Draw a cone to represent the field of view
        Gizmos.color = Color.yellow;

        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2, 0) * eyePosition.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2, 0) * eyePosition.forward;

        Gizmos.DrawRay(eyePosition.position, leftBoundary * viewRadius);
        Gizmos.DrawRay(eyePosition.position, rightBoundary * viewRadius);

        // Optionally draw a center ray for visual feedback
        Gizmos.color = Color.red;
        Gizmos.DrawRay(eyePosition.position, eyePosition.forward * viewRadius);
    }
}

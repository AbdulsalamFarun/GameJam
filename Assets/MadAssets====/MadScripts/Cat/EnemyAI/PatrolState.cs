using UnityEngine;

public class PatrolState : IEnemyState
{
    private Enemy enemy;
    private Vector3 targetPoint;

    public EnemyStateType GetStateType() => EnemyStateType.Patrol;

    public void EnterState(Enemy enemy)
    {
        enemy.agent.speed = enemy.patrolSpeed;
        this.enemy = enemy;
        targetPoint = enemy.GetRandomPatrolPoint();
        enemy.agent.SetDestination(targetPoint);
        Debug.Log("Patrol State Entered");

        enemy.animator.SetBool("isWalking", true);
        enemy.animator.SetBool("isIdle", false);
        enemy.animator.SetBool("isChasing", false);
        enemy.animator.SetBool("isAttacking", false);
    }

    public void UpdateState()
    {
        if (!enemy.agent.pathPending && enemy.agent.remainingDistance < 0.5f)
        {
            enemy.SwitchState(enemy.idleState);
        }

        if (enemy.PlayerInRange())
        {
            enemy.SwitchState(enemy.chaseState);
            return;
        }

    }

    public void ExitState() { }
}

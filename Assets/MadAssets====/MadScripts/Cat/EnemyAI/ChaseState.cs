using UnityEngine;

public class ChaseState : IEnemyState
{
    private Enemy enemy;
    private Transform player;

    private float giveUpTimer = 0f;
    private float giveUpDelay;

    public EnemyStateType GetStateType() => EnemyStateType.Chase;

    public void EnterState(Enemy enemy)
    {
        enemy.agent.speed = enemy.chaseSpeed;
        this.enemy = enemy;
        player = enemy.playerTransform;
        giveUpTimer = 0f;

        giveUpDelay = UnityEngine.Random.Range(3f, 10f);

        enemy.animator.SetBool("isChasing", true);
    }

    public void UpdateState()
    {
        if (player == null) return;

        enemy.agent.SetDestination(player.position);
        

        // If we can still see the player
        if (enemy.PlayerInRange())
        {
            giveUpTimer = 0f; // Reset timer
        }
        else
        {
            if (enemy.canGiveUp)
            {
                giveUpTimer += Time.deltaTime;
                if (giveUpTimer >= giveUpDelay)
                {
                    enemy.SwitchState(enemy.idleState); // or patrol
                    return;
                }
            }
        }

        // Optional: switch to attack if close
        float distance = Vector3.Distance(enemy.transform.position, player.position);
        if (distance < 1.5f)
        {
            enemy.SwitchState(enemy.attackState);
        }
    }

    public void ExitState()
    {
        giveUpTimer = 0f;
    }
}
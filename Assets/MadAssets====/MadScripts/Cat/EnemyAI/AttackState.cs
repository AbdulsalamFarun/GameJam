using UnityEngine;

public class AttackState : IEnemyState
{
    private Enemy enemy;
    private Transform player;
    public int damage = 10;
    private float attackCooldown = 1f;
    private float timer;

    public EnemyStateType GetStateType() => EnemyStateType.Attack;

    public void EnterState(Enemy enemy)
    {
        this.enemy = enemy;
        player = GameObject.FindWithTag("Player").transform;
        timer = attackCooldown;
        enemy.agent.isStopped = true;
        enemy.animator.SetBool("isAttacking", true);
        enemy.animator.SetBool("isIdle", false);
        enemy.animator.SetBool("isWalking", false);
        enemy.animator.SetBool("isChasing", false);
    }

    public void UpdateState()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            /*PlayerStats playerStats = player.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(damage);
                Debug.Log("Enemy attacks player!");
                timer = attackCooldown;
            }*/
        }

        float distance = Vector3.Distance(enemy.transform.position, player.position);
        if (distance > 1f)
        {
            enemy.SwitchState(enemy.chaseState);
        }
    }

    public void ExitState()
    {
        enemy.agent.isStopped = false;
    }
}

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
        Debug.Log("Switching to Attack State");
    }

    public void UpdateState()
    {

        // Face the player smoothly
        Vector3 direction = (player.position - enemy.transform.position).normalized;
        direction.y = 0f; // keep only horizontal rotation

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

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

        Vector3 chefPos = new Vector3(enemy.transform.position.x, 0, enemy.transform.position.z);
        Vector3 playerPos = new Vector3(player.position.x, 0, player.position.z);
        float distance = Vector3.Distance(chefPos, playerPos);
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

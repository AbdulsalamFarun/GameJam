using UnityEngine;

public class AttackState : IEnemyState
{
    private Enemy enemy;
    private Transform player;
    private float attackCooldown = 3f;
    private float timer;

    public EnemyStateType GetStateType() => EnemyStateType.Attack;

    public void EnterState(Enemy enemy)
    {
        this.enemy = enemy;
        player = GameObject.FindWithTag("Player").transform;
        timer = attackCooldown;
        timer = 0f;
        enemy.agent.isStopped = true;
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
            if (player.localPosition.y <= -5f)
            {
                enemy.animator.SetTrigger("isCrouchAttacking");
                enemy.animator.SetBool("isCrouchChasing", false);
                Debug.Log("Switching to Crouch Attack State");
            }
            else if (player.localPosition.y > -5f)
            {
                enemy.animator.SetTrigger("isAttacking");
                Debug.Log("Switching to Attack State");

                timer = attackCooldown;
                enemy.animator.SetBool("isWaiting", true);
            }
        }

        Vector3 enemyPos = new Vector3(enemy.transform.position.x, 0, enemy.transform.position.z);
        Vector3 playerPos = new Vector3(player.position.x, 0, player.position.z);
        float distance = Vector3.Distance(enemyPos, playerPos);
        if (distance > 2.3f)
        {
            enemy.SwitchState(enemy.chaseState);
        }
    }

    public void ExitState()
    {
        enemy.agent.isStopped = false;
    }
}

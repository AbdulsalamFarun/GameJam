using UnityEngine;

public class AbdulsalamEnemy : MonoBehaviour
{
    public float attackCooldown = 2f;
    private float nextAttackTime = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Time.time >= nextAttackTime)
        {
            RatHitHandler rat = other.GetComponent<RatHitHandler>();
            if (rat != null)
            {
                rat.TakeHit();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }
}

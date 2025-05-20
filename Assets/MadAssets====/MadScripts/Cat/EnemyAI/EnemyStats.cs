using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;
    public System.Action onDeath;


    void Start()
    {
        currentHealth = maxHealth;
        
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} died!");
        if (onDeath != null) onDeath.Invoke();
        Destroy(gameObject);
    }
}

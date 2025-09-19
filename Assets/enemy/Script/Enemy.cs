using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData data;
    private int currentHealth;
    private EnemyWaveSpawner spawner;
   // [SerializeField] private int EXP_Count = 10;
    void Start()
    {
        if (data == null)
        {
            Debug.LogError("EnemyData not set for " + gameObject.name);
            return;
        }

        currentHealth = data.health;
        spawner = FindFirstObjectByType<EnemyWaveSpawner>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"{data.enemyID} took {amount} damage. Remaining: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{data.enemyID} has died.");
        if (spawner != null)
        {
            spawner.RemoveEnemyFromList(this.gameObject);
        }
        Destroy(gameObject);
        GameManager.Instance.EXP();
    }
    

    
}

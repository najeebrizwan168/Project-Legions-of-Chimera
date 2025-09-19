using UnityEngine;

public class Castle : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 0;

    public Animator animator; // Assign Animator with death animation
   

    public bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;

        if (collision.CompareTag("Fire"))
        {
            TakeDamage(5);
            Destroy(collision.gameObject); // Destroy projectile on hit
        }
    }

    void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Castle HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        
        Destroy(gameObject, 1.5f); // Wait for animation to finish (adjust if needed)
    }
}

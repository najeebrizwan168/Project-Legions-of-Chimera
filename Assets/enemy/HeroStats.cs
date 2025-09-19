using System.Collections;
using UnityEngine;

public class HeroStats : MonoBehaviour
{
    public CardData cardData;
    private int currentHealth;
    private int attackDamage;
    public Animator animator;
    private Characterhealthsystem characterhealthsystem;
  
    private bool diedFromCombat = false; // 👈 Track if death was from taking damage

    void Start()
    {
        currentHealth = cardData.health;
        attackDamage = cardData.Attack_damage;
        animator = GetComponent<Animator>();
        characterhealthsystem = GetComponent<Characterhealthsystem>();
        
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage. Remaining HP: {currentHealth}");
        animator.SetTrigger("DamageTrigger");

        if (characterhealthsystem != null)
        {
            characterhealthsystem.TakeDamage(amount);
            Debug.Log("health_working");
        }

        if (currentHealth <= 0)
        {
            animator.SetTrigger("DeathTrigger");

            diedFromCombat = true; // 👈 mark as real death
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject, 2f);
    }

    private void OnDestroy()
    {
        if (HealthSystem.Instance != null && diedFromCombat)
        {
            // Only reduce crystal power if this hero actually died
            
            HealthSystem.Instance.TakeDamage(8f);
            Debug.Log("⚠️ Health reduced because Hero DIED (combat)!");
        }
        else
        {
            Debug.Log("✅ Hero destroyed by cleanup, no health lost.");
        }
    }
}

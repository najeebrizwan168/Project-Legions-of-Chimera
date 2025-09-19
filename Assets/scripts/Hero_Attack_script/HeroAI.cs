using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAI : MonoBehaviour
{
    public float fireRate = 1f;
    public float fireCooldown = 0f;
    private GameObject currentTarget;
    private CardData cardData; // Holds attack damage, level, fire_prefab
    private Animator anim;     // Animator for controlling animations
    public float anim_time = 0f;
    private AudioSource audioSource;
    void Start()
    {
        
        audioSource= GetComponent<AudioSource>();
        cardData = GetComponent<HeroStats>().cardData;
        anim = GetComponent<Animator>();
        if (anim != null && cardData.animatorControllers != null && cardData.animatorControllers.Count > 0)
        {
            int level = Mathf.Clamp(cardData.level, 0, cardData.animatorControllers.Count - 1);
            anim.runtimeAnimatorController = cardData.animatorControllers[level];
            Debug.Log($"Animator Controller assigned for level {level}");
        }

        FindNewTarget();
    }

    void Update()
    {
        if (fireCooldown > 0f)
        {
            fireCooldown -= Time.deltaTime;
        }

        if (currentTarget == null || currentTarget.Equals(null))
        {
            FindNewTarget();
            return;
        }

        // Aim and rotate towards target
        Vector3 direction = currentTarget.transform.position - transform.position;
        direction.z = 0;

        Vector3 originalScale = transform.localScale;
        originalScale.x = direction.x > 0 ? Mathf.Abs(originalScale.x) : -Mathf.Abs(originalScale.x);
        transform.localScale = originalScale;
        
        if (fireCooldown <= 0f)
        {
            FireProjectile(direction.normalized);
            fireCooldown = fireRate;
        }
    }

    void FindNewTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            currentTarget = null;
            return;
        }

        float closestDist = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                nearestEnemy = enemy;
            }
        }

        currentTarget = nearestEnemy;

        if (currentTarget != null)
        {
            Debug.Log($"{gameObject.name} is targeting: {currentTarget.name}");
        }
    }

    void FireProjectile(Vector3 direction)
    {
        if (cardData.Fire_prefab == null || cardData.Fire_prefab.Count == 0) return;

        int level = Mathf.Clamp(cardData.level, 0, cardData.Fire_prefab.Count - 1);
        GameObject fireball = Instantiate(cardData.Fire_prefab[level], transform.position, Quaternion.identity);
      
        Projectile projectile = fireball.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.SetDirection(direction, cardData.Attack_damage, cardData.DamageVfx_prefab,cardData.level);
        }
        
        // ✅ Play attack animation using Trigger (instead of bool)
        if (anim != null)
        {
            anim.SetTrigger("AttackTrigger");
            Debug.Log("Attack animation triggered");
        }
        audioSource.Play();

    }


    private IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(anim_time); // Adjust based on your animation duration
        if (anim != null)
        {
            anim.SetBool("Attack", false);
        }
       
    }
}

using UnityEngine;
using System.Collections.Generic;
using DragonBones;

public class EnemySlowController : MonoBehaviour
{
    [Header("Slow Settings")]
    public float baseSlowDuration = 5f;
    public float slowMultiplier = 0.5f;

    private List<UnityArmatureComponent> slowedEnemies = new List<UnityArmatureComponent>();
    private List<EnemyAi> slowedAIs = new List<EnemyAi>();
    private List<EnemyProjectile> slowedProjectiles = new List<EnemyProjectile>();

    public void ApplySlowEffect()
    {
        float slowDuration = baseSlowDuration; // Reset each use

        // Slow all enemies
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            // DragonBones animation slowdown
            UnityArmatureComponent armature = enemy.GetComponent<UnityArmatureComponent>();
            if (armature != null && !slowedEnemies.Contains(armature))
            {
                armature.animation.timeScale *= slowMultiplier;
                slowedEnemies.Add(armature);
            }

            // Slow AI fire rate and delay
            EnemyAi ai = enemy.GetComponent<EnemyAi>();
            if (ai != null && !slowedAIs.Contains(ai))
            {
                ai.fireRate *= (1f / slowMultiplier);
                ai.fireDelay *= (1f / slowMultiplier);
                slowedAIs.Add(ai);
            }
        }

        // Slow all enemy projectiles
        foreach (var projObj in GameObject.FindGameObjectsWithTag("EnemyAttack"))
        {
            EnemyProjectile proj = projObj.GetComponent<EnemyProjectile>();
            if (proj != null && !slowedProjectiles.Contains(proj))
            {
                proj.speed *= slowMultiplier;
                slowedProjectiles.Add(proj);
            }
        }

        Invoke(nameof(ResetSpeed), slowDuration);
    }

    void ResetSpeed()
    {
        foreach (var armature in slowedEnemies)
        {
            if (armature != null)
                armature.animation.timeScale /= slowMultiplier;
        }

        foreach (var ai in slowedAIs)
        {
            if (ai != null)
            {
                ai.fireRate = ai.originalFireRate;
                ai.fireDelay = ai.originalFireDelay;
            }
        }

        foreach (var proj in slowedProjectiles)
        {
            if (proj != null)
                proj.speed /= slowMultiplier;
        }

        slowedEnemies.Clear();
        slowedAIs.Clear();
        slowedProjectiles.Clear();
    }
}

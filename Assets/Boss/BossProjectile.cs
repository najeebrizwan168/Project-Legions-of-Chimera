using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 direction;
    private int damage;
    private GameObject targetPlayer;
    private GameObject impactVFX;

    // How close the projectile must get to count as a "hit"
    private float hitThreshold = 0.3f;

    public void SetDirection(Vector3 dir, int dmg, GameObject vfx, GameObject target)
    {
        direction = dir.normalized;
        damage = dmg;
        impactVFX = vfx;
        targetPlayer = target;
    }

    void Update()
    {
        if (targetPlayer == null)
        {
            Destroy(gameObject);
            return;
        }

        // Move toward the target
        Vector3 moveDir = (targetPlayer.transform.position - transform.position).normalized;
        transform.position += moveDir * speed * Time.deltaTime;

        // Check if close enough to "hit"
        float distanceToTarget = Vector3.Distance(transform.position, targetPlayer.transform.position);
        if (distanceToTarget < hitThreshold)
        {
            // Apply damage
            HeroStats stats = targetPlayer.GetComponent<HeroStats>();
            if (stats != null)
            {
                stats.TakeDamage(damage);
            }

            // VFX
            if (impactVFX != null)
            {
                Destroy(Instantiate(impactVFX, transform.position, Quaternion.identity), 2f);

            }

            Destroy(gameObject);
            
        }
        
    }
}

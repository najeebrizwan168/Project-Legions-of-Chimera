using UnityEngine;

public class SpikeAttack : MonoBehaviour
{
    public int damageAmount = 100;                // Damage dealt to the enemy
    public GameObject hitVFXPrefab;              // VFX to show on impact
    public float vfxLifetime = 2f;               // How long the VFX stays

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered by: " + other.name); // Add this line

        if (other.CompareTag("Enemy"))
        {
            Enemy enemydata = other.GetComponent<Enemy>();
            if (enemydata != null)
            {
                enemydata.TakeDamage(damageAmount);
                Debug.Log("damage...");
            }

            if (hitVFXPrefab != null)
            {
                GameObject vfx = Instantiate(hitVFXPrefab, other.transform.position, Quaternion.identity);
                Destroy(vfx, vfxLifetime);
            }
        }
    }

}

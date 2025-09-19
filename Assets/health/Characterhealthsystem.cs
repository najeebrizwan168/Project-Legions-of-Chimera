

using UnityEngine;
using UnityEngine.UI;
public class Characterhealthsystem : MonoBehaviour
{
    [Header("Assign the Health Bar SpriteRenderer")]
    public SpriteRenderer healthBar; // Drag your health bar sprite here

    [Header("Health Settings")]
    public float maxHitPoint = 100f;
    public float hitPoint = 100f;

    private float barWidth; // Stores original width of health bar sprite

    private void Start()
    {
        HeroStats heroStats = GetComponent<HeroStats>();
        // Store original width of the health bar
        if (healthBar != null && healthBar.sprite != null)
        {
            barWidth = healthBar.sprite.bounds.size.x;
        }
        maxHitPoint = heroStats.cardData.health;
        hitPoint = maxHitPoint;
        UpdateGraphics();
    }

    public void TakeDamage(float damage)
    {
        hitPoint -= damage;
        if (hitPoint < 0)
            hitPoint = 0;

        UpdateGraphics();
    }

    private void UpdateGraphics()
    {
        if (healthBar == null) return;

        // Health ratio
        float ratio = hitPoint / maxHitPoint;

        // Scale the health bar
        healthBar.transform.localScale = new Vector3(ratio, 1f, 1f);

        // Adjust position so it decreases from the right
        float offset = (1 - ratio) * barWidth / 2f;
        healthBar.transform.localPosition = new Vector3(-offset, healthBar.transform.localPosition.y, healthBar.transform.localPosition.z);
    }
}

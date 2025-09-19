using UnityEngine;

public class MetariodBehaviour : MonoBehaviour
{
    public float fallSpeed = 5f;
    public int damage = 50;
    public GameObject impactVFX;

    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        Destroy(gameObject, 10f); // Safety destroy if it misses everything
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Apply damage
            Enemy data = other.GetComponent<Enemy>();
            if (data != null)
            {
                data.TakeDamage(damage);
            }
           
            // Spawn VFX
            if (impactVFX != null)
            {
                GameObject vfx = Instantiate(impactVFX, transform.position, Quaternion.identity);
                Destroy(vfx, 2f);
            }

            // Destroy meteor
            Destroy(gameObject);
        }
    }
}

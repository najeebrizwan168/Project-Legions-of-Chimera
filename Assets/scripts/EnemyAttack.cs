using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float detectionRadius = 1.5f;
    public LayerMask castleLayer;
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float fireRate = 1.5f;
    public float moveSpeed = 2f;
    public Animator animator;

    private bool isAttacking = false;
    private Transform targetCastle;

    void Start()
    {
        // Optional: auto-find castle
        GameObject castle = GameObject.FindGameObjectWithTag("Castle");
        if (castle != null)
            targetCastle = castle.transform;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (targetCastle == null) return;

        float distance = Vector2.Distance(transform.position, targetCastle.position);

        if (distance <= detectionRadius && !isAttacking)
        {
            StartAttacking();
        }
        else if (!isAttacking)
        {
            // Move toward the castle
            transform.position = Vector2.MoveTowards(transform.position, targetCastle.position, moveSpeed * Time.deltaTime);
        }
    }

    void StartAttacking()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        InvokeRepeating(nameof(FireProjectile), 0f, fireRate);
    }

    void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().linearVelocity = Vector2.left * 5f;
    }

    void OnDrawGizmosSelected()
    {
        // Show detection radius in scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}

using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public float fireRate = 1f;
    private float fireCooldown = 0f;
    private GameObject currentTarget;
    private EnemyData Enemydata;

    private UnityArmatureComponent dragonBonesAnim;
    public UnityEngine.Transform attack_point;

    public bool useSwordInHandLogic = false;
    public GameObject swordInHand;

    public float fireDelay = 1f;          // ✅ Delay before actual fire happens (after animation start)
    public float swordReturnDelay = 1f;     // ✅ Delay before sword reappears in hand
    [HideInInspector] public float originalFireRate;
    [HideInInspector] public float originalFireDelay;
    AudioSource audioSource;
    void Start()
    {
        originalFireRate = fireRate;
        originalFireDelay = fireDelay;
        audioSource = GetComponent<AudioSource>();

        Enemydata = GetComponent<Enemy>().data;
        dragonBonesAnim = GetComponent<UnityArmatureComponent>();


        if (dragonBonesAnim != null)
        {
            dragonBonesAnim.RemoveEventListener(EventObject.COMPLETE, OnAnimationComplete);
            dragonBonesAnim.AddEventListener(EventObject.COMPLETE, OnAnimationComplete);
        }

        FindNewTarget();
    }

    void Update()
    {
        if (fireCooldown > 0f)
            fireCooldown -= Time.deltaTime;

        if (currentTarget == null || currentTarget.Equals(null))
        {
            FindNewTarget();
            return;
        }

        // Face the target
        Vector3 direction = currentTarget.transform.position - transform.position;
        direction.z = 0;

        Vector3 scale = transform.localScale;
        scale.x = direction.x > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;

        if (fireCooldown <= 0f)
        {
            StartCoroutine(FireSequence(direction.normalized));
            fireCooldown = fireRate;
        }
    }

    void FindNewTarget()
    {
        GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");

        if (Players.Length == 0)
        {
            currentTarget = null;
            return;
        }

        float closestDist = Mathf.Infinity;
        GameObject nearest = null;

        foreach (GameObject player in Players)
        {
            float dist = Vector3.Distance(transform.position, player.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                nearest = player;
            }
        }

        currentTarget = nearest;

        if (currentTarget != null)
            Debug.Log($"{gameObject.name} targeting: {currentTarget.name}");
    }

    IEnumerator FireSequence(Vector3 direction)
    {
        // ✅ 1. Play animation immediately
        if (dragonBonesAnim != null)
            dragonBonesAnim.animation.Play("Attack A", 1);
       
         
        // ✅ 2. Wait before doing actual fire
        yield return new WaitForSeconds(fireDelay);

        // ✅ 3. Hide sword/axe if applicable
        if (useSwordInHandLogic && swordInHand != null)
        {
            swordInHand.SetActive(false);
            Invoke(nameof(ResetSword), swordReturnDelay);  // Schedule return
        }

        // ✅ 4. Spawn the firePrefab (projectile)
        if (Enemydata.Fire_prefab != null)
        {
            GameObject projectile = Instantiate(Enemydata.Fire_prefab, attack_point.position, Quaternion.identity);

            EnemyProjectile enemyProjectile = projectile.GetComponent<EnemyProjectile>();
            if (enemyProjectile != null)
            {
                enemyProjectile.SetDirection(direction, Enemydata.damage, Enemydata.impact_vfx);
                StartCoroutine(audioDelay());
            }
        }
    }

    private void OnAnimationComplete(string type, EventObject eventObject)
    {
        if (eventObject.animationState.name == "Attack A")
        {
            GameObject go = eventObject.armature.display as GameObject;
            if (go != null)
            {
                UnityArmatureComponent armature = go.GetComponent<UnityArmatureComponent>();
                if (armature != null)
                {
                    Debug.Log($"{go.name} returning to Idle after Attack A");
                    armature.animation.Play("Idle", 0);
                }
            }
        }
    }

    void ResetSword()
    {
        if (swordInHand != null)
            swordInHand.SetActive(true);
    }
    IEnumerator audioDelay()
    {
        yield return new WaitForSeconds(0.08f);
        if (audioSource != null)
        {
            audioSource.PlayOneShot(Enemydata.audio);
        }
    }
}

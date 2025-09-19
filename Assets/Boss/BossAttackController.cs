using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class BossAttackController : MonoBehaviour
{
    public EnemyData enemyData;
    public float fireSpawnHeight = 5f;
    public float warningDuration = 3f; // Adjustable warning time

    private float attackTimer = 0f;
    private List<UnityEngine.Transform> activePlayers = new List<UnityEngine.Transform>();
    private UnityArmatureComponent dragonBonesAnim;
    private void Start()
    {
        dragonBonesAnim = GetComponent<UnityArmatureComponent>();
        if (dragonBonesAnim != null)
        {
            dragonBonesAnim.RemoveEventListener(EventObject.COMPLETE, OnAnimationComplete);
            dragonBonesAnim.AddEventListener(EventObject.COMPLETE, OnAnimationComplete);
        }
    }
    void Update()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= enemyData.attackCooldown)
        {
            attackTimer = 0f;
            StartCoroutine(PerformBossAttack());
        }
    }

    IEnumerator PerformBossAttack()
    {
        UpdatePlayerList();

        int count = Mathf.Min(4, activePlayers.Count);
        List<UnityEngine.Transform> targets = GetRandomTargets(activePlayers, count);

        List<   UnityEngine.Transform> warnings = new List<UnityEngine.Transform>();

        // Step 1: Show warning for each selected player
        foreach (UnityEngine.Transform player in targets)
        {
                UnityEngine.Transform warning = GetChildWithTag(player, "warning");
            if (warning != null)
            {
                warning.gameObject.SetActive(true);
                warnings.Add(warning);
            }
        }

        // Step 2: Wait for the warning duration
        yield return new WaitForSeconds(warningDuration);
        if (dragonBonesAnim != null)
            dragonBonesAnim.animation.Play("Attack A", 1);
        // Step 3: Fire projectile at each player
        foreach (UnityEngine.Transform player in targets)
        {
            Vector3 spawnPos = player.position + Vector3.up * fireSpawnHeight;
            GameObject fire = Instantiate(enemyData.Fire_prefab, spawnPos, Quaternion.identity);

            BossProjectile projectile = fire.GetComponent<BossProjectile>();
            if (projectile != null)
            {
                Vector3 dir = (player.position - spawnPos).normalized;
                projectile.SetDirection(dir, enemyData.damage, enemyData.impact_vfx, player.gameObject);

            }
            else
            {
                Debug.LogWarning("EnemyProjectile script missing on Fire_prefab!");
            }
        }

        // Step 4: Disable all warning objects
        foreach (UnityEngine.Transform warning in warnings)
        {
            if (warning != null)
                warning.gameObject.SetActive(false);
        }
    }
    
    void UpdatePlayerList()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        activePlayers.Clear();

        foreach (GameObject player in players)
        {
            activePlayers.Add(player.transform);
        }
    }

    List<UnityEngine.Transform> GetRandomTargets(List<UnityEngine.Transform> list, int count)
    {
        List<UnityEngine.Transform> copy = new List<UnityEngine.Transform>(list);
        List<UnityEngine.Transform> selected = new List<UnityEngine.Transform>();

        for (int i = 0; i < count && copy.Count > 0; i++)
        {
            int randIndex = Random.Range(0, copy.Count);
            selected.Add(copy[randIndex]);
            copy.RemoveAt(randIndex);
        }

        return selected;
    }

    UnityEngine.Transform GetChildWithTag(UnityEngine.Transform parent, string tag)
    {
        foreach (UnityEngine.Transform child in parent)
        {
            if (child.CompareTag(tag))
                return child;
        }
        return null;
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
}

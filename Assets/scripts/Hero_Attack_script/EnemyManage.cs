using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    public List<Transform> activeEnemies = new List<Transform>();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        UpdateEnemyList();
    }

    void UpdateEnemyList()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        activeEnemies.Clear();

        foreach (GameObject enemy in enemies)
        {
            activeEnemies.Add(enemy.transform);
        }
    }

    public Transform GetClosestEnemy(Vector3 fromPosition)
    {
        Transform closest = null;
        float minDist = Mathf.Infinity;

        foreach (Transform enemy in activeEnemies)
        {
            float dist = Vector3.Distance(fromPosition, enemy.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = enemy;
            }
        }

        return closest;
    }
}

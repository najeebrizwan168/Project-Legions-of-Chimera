using UnityEngine;
using System.Collections;

public class SceneReadyChecker : MonoBehaviour
{
    public bool IsReady { get; private set; } = false;

    IEnumerator Start()
    {
        EnemyWaveSpawner spawner = null;
        while (spawner == null)
        {
            spawner = FindFirstObjectByType<EnemyWaveSpawner>();
            yield return null;
        }

        while (!spawner.HasSpawnedEnemies)
            yield return null;

        // 🔥 Add 1 second delay AFTER enemies are confirmed spawned
        yield return new WaitForSeconds(1.5f);

        IsReady = true;
    }
}

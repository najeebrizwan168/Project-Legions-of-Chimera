using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class EnemyWaveSpawner : MonoBehaviour
{
    [Header("Spawn Area")]
    public Vector2 spawnAreaSize = new Vector2(6f, 4f);

    [Header("Scaling (per levelIndex)")]
    public float healthScalePerLevel = 0.2f;
    public float damageScalePerLevel = 0.15f;

    [Header("Flow")]
    public float waveSpawnDelay = 0f;
    public bool clearPlayersBetweenWaves = false;

    private readonly List<GameObject> currentEnemies = new List<GameObject>();
    private int currentWaveIndex = 0;
    private bool isSpawning = false;
    private LevelData currentLevel;
    public bool HasSpawnedEnemies { get; private set; } = false;
    public TextMeshProUGUI wave_number;

    private VictoryPanelConfiguration victoryPanel; // ✅ reference to VictoryPanel
    public GameObject[] background = new GameObject[5];
    private PausePanelConfigration pausePanelConfigration;
    public GameObject gameoverpanel;
    public AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        pausePanelConfigration = gameoverpanel.GetComponent<PausePanelConfigration>();
        // deactivate all backgrounds
        foreach (GameObject BG in background)
        {
            BG.SetActive(false);
        }
        pausePanelConfigration.enabled = true;
        currentLevel = LevelManager.SelectedLevel;
        victoryPanel = FindFirstObjectByType<VictoryPanelConfiguration>();

        if (currentLevel == null)
        {
            Debug.LogError("LevelManager.SelectedLevel is NULL!");
            return;
        }

        // ✅ Show a random background at the start
        ActivateRandomBackground();

        StartCoroutine(SpawnWave());
    }


    void Update()
    {
        currentEnemies.RemoveAll(e => e == null);

        if (currentLevel == null) return;

        // ✅ Check if all waves are done & no enemies are alive
        if (currentWaveIndex >= currentLevel.waves.Count && currentEnemies.Count == 0 && !isSpawning)
        {
            Debug.Log("🏆 All waves cleared! Calling levelComplete()");
            if (victoryPanel != null)
                victoryPanel.levelComplete();
            if (pausePanelConfigration != null)
                pausePanelConfigration.enabled=false;
       audioSource.Play();
            StartCoroutine(PauseAfterAnimation());
            enabled = false; // stop checking

            return;
        }

        // ✅ Spawn next wave if no enemies remain
        if (currentEnemies.Count == 0 && !isSpawning && currentWaveIndex < currentLevel.waves.Count)
        {
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        isSpawning = true;
        yield return new WaitForSeconds(waveSpawnDelay);

        // ✅ Remove players between waves
        if (clearPlayersBetweenWaves && currentWaveIndex > 0)
        {
            DestroyAllActivePlayers();
        }

        // ✅ Show wave panel between waves
        if (currentWaveIndex > 0) // not before first wave
        {
            if (UiPanelManager.Instance != null)
            {
                ActivateRandomBackground();
                UiPanelManager.Instance.activewavepanel();
                Debug.Log("📌 Showing Wave Panel before next wave...");

                // wait a few seconds while panel is visible
               
            }
        }

        if (currentWaveIndex >= currentLevel.waves.Count)
        {
            isSpawning = false;
            yield break;
        }

        WaveData wave = currentLevel.waves[currentWaveIndex];
        Debug.Log($"⚔️ Spawning Wave {currentWaveIndex + 1}/{currentLevel.waves.Count}");
        wave_number.text = $"Wave {currentWaveIndex + 1}/{currentLevel.waves.Count}";
        foreach (EnemySpawnData enemyPack in wave.enemies)
        {
            if (enemyPack == null || enemyPack.enemyType == null) continue;

            for (int i = 0; i < enemyPack.count; i++)
            {
                DestroyAllActivePlayers();
                SpawnEnemy(enemyPack.enemyType, currentLevel.levelIndex);
            }
        }

        currentWaveIndex++;
        isSpawning = false;

        if (!HasSpawnedEnemies)
        {
            Debug.Log("✅ First wave fully spawned!");
            yield return new WaitForSeconds(1f);
            HasSpawnedEnemies = true;
        }
    }


    void SpawnEnemy(EnemyData baseData, int levelIndex)
    {
        EnemyData runtimeData = ScriptableObject.Instantiate(baseData);
        runtimeData.health = Mathf.RoundToInt(baseData.health * (1f + levelIndex * healthScalePerLevel));
        runtimeData.damage = Mathf.RoundToInt(baseData.damage * (1f + levelIndex * damageScalePerLevel));

        Vector2 pos = GetRandomPointInArea();
        GameObject enemyGO = Instantiate(runtimeData.prefab, pos, Quaternion.identity);
        currentEnemies.Add(enemyGO);

        Enemy enemyScript = enemyGO.GetComponent<Enemy>();
        if (enemyScript != null)
            enemyScript.data = runtimeData;
    }

    Vector2 GetRandomPointInArea()
    {
        Vector2 center = (Vector2)transform.position;
        float halfW = spawnAreaSize.x * 0.5f;
        float halfH = spawnAreaSize.y * 0.5f;
        return center + new Vector2(Random.Range(-halfW, halfW), Random.Range(-halfH, halfH));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        if (currentEnemies.Contains(enemy))
        {
            currentEnemies.Remove(enemy);
            Debug.Log("Enemy removed. Remaining: " + currentEnemies.Count);
        }
    }

    public void DestroyAllActivePlayers()
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in allPlayers)
        {
            if (player != null && player.activeInHierarchy)
            {
                Destroy(player);
            }
        }
      
    }
    
    void ActivateRandomBackground()
    {
        // turn off all
        foreach (GameObject bg in background)
        {
            bg.SetActive(false);
        }

        // choose random
        int index = Random.Range(0, background.Length);
        background[index].SetActive(true);

        Debug.Log($"🌄 Background changed to index {index}");
    }
    IEnumerator PauseAfterAnimation()
    {
        // Wait in real time, NOT affected by timeScale
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 0; // Pause AFTER animation completes
    }
}



using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Chimera/Level Data")]
public class LevelData : ScriptableObject
{
    public int levelIndex;
    public int level_time;
    public int reward;
    public int After_level_reward;
    public bool levelComplete=false;
    public List<WaveData> waves;
}
[System.Serializable]
public class WaveData
{
    public List<EnemySpawnData> enemies;
}

[System.Serializable]
public class EnemySpawnData
{
    public EnemyData enemyType;
    public int count;
}

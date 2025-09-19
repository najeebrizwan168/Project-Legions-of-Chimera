using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Chimera/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyID;
    public Sprite enemyIcon;                 // For UI/preview if needed
    public GameObject prefab;                // Prefab to spawn
    public int health;
    public float attackCooldown;
    public int damage;
    public GameObject Fire_prefab;
    public GameObject impact_vfx;
    public AudioClip audio;
   
}

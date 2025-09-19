using UnityEngine;

public class MetaroidSpawner : MonoBehaviour
{
    [Header("Meteor Settings")]
    public GameObject meteorPrefab;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;

    [Header("Damage Settings")]
    public int damage = 50;
    public GameObject impactVFX;

    [Header("Movement")]
    public float fallSpeed = 5f;
    public AudioSource Audio;
    void Start()
    {
        Audio= GetComponent<AudioSource>(); 
    }
   public  void MetariodSpawner()
    {
        SpawnMeteor(spawnPoint1.position);
        SpawnMeteor(spawnPoint2.position);
       
    }
    void SpawnMeteor(Vector3 spawnPos)
    {
        GameObject meteor = Instantiate(meteorPrefab, spawnPos, Quaternion.identity);
        MetariodBehaviour behavior = meteor.AddComponent<MetariodBehaviour>();
        behavior.fallSpeed = fallSpeed;
        behavior.damage = damage;
        behavior.impactVFX = impactVFX;
    }
   public  void audioSet()
    {
        Audio.Play();
    }
}

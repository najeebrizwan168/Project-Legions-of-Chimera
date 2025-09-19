using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{
    public GameObject spikePrefab;         // Assign your spike prefab in Inspector
    public Transform spawnAreaReference;   // This is the reference where spikes spawn
    public float spikeLifetime = 2f;       // Time before the spike is destroyed
    public AudioSource Audio;
    private void Start()
    {
        Audio = GetComponent<AudioSource>();
    }
    public void SpawnSpikes()
    {
        if (spikePrefab != null && spawnAreaReference != null)
        {
            // Instantiate the spike at the reference point
            GameObject spike = Instantiate(spikePrefab, spawnAreaReference.position, Quaternion.identity);

            // Optional: match the parent or rotation if needed
            spike.transform.rotation = spawnAreaReference.rotation;

            // Destroy the spike after 2 seconds
            Destroy(spike, spikeLifetime);
            if (spikePrefab != null) 
            {
                Audio.Play();
            }
        }
        else
        {
            Debug.LogWarning("SpikePrefab or SpawnAreaReference not assigned.");
        }
    }
}

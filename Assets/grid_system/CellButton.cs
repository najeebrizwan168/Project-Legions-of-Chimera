using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class CellButton : MonoBehaviour
{
    public GameObject panel_grids;
    private BarFiller barFiller;
    public AudioSource audioSource;
    public AudioClip clip;
    void Start()
    {
      audioSource = GetComponent<AudioSource>();
        barFiller = FindFirstObjectByType<BarFiller>();  // ✅ Unity 2023+ safe method

        if (barFiller == null)
            Debug.LogError("❌ No BarFiller found in the scene!");

        GetComponent<Button>().onClick.AddListener(SpawnUnit);
        
       
    }

    void SpawnUnit()
    {
      
        CardData data = GameManager.Instance.GetSelectedCard();

        if (data == null)
        {
            Debug.LogWarning("⚠️ No card selected!");
            audioSource.Play();
            return;
        }

        if (!barFiller.HasEnoughDots(data.energyCost))
        {
            Debug.Log("❌ Not enough energy!");
            audioSource.Play();
            return;
           
        }

        // Convert UI position to world position (use your world grid setup)
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0f;

        Instantiate(data.characterPrefab, worldPos, Quaternion.identity);

        barFiller.ConsumeDots(data.energyCost);
        StartCoroutine(audioset());
        
       
            
    }
   
    IEnumerator audioset() 
        {
        playclip();
       yield return new WaitForSeconds(0.5f);
        panel_grids.SetActive(false);

    }
    public void playclip()
    {
        audioSource.PlayOneShot(clip);
    }

}
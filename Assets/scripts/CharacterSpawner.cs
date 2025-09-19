using UnityEngine;
using UnityEngine.UI;

public class CharacterSpawner : MonoBehaviour
{
    public BarFiller barFiller;

    public Button button1;
    public Button button2;

    public int character1Cost = 4;
    public int character2Cost = 5;

    public GameObject character1Prefab;
    public GameObject character2Prefab;

    public GameObject spawnPoint1;
    public GameObject spawnPoint2;

    private GameObject selectedCharacter;

    void Start()
    {
        button1.onClick.AddListener(() => OnCharacterButtonClicked(character1Prefab, character1Cost));
        button2.onClick.AddListener(() => OnCharacterButtonClicked(character2Prefab, character2Cost));

        spawnPoint1.SetActive(false);
        spawnPoint2.SetActive(false);
    }

    void OnCharacterButtonClicked(GameObject characterPrefab, int cost)
    {
        if (!barFiller.HasEnoughDots(cost)) return;

        selectedCharacter = characterPrefab;

        spawnPoint1.SetActive(true);
        spawnPoint2.SetActive(true);
    }

    void Update()
    {
        if (selectedCharacter != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                clickPos.z = 0;

                float dist1 = Vector3.Distance(clickPos, spawnPoint1.transform.position);
                float dist2 = Vector3.Distance(clickPos, spawnPoint2.transform.position);

                float maxClickRange = 1.5f; // Adjust if needed

                if (dist1 < maxClickRange)
                {
                    SpawnCharacterAt(spawnPoint1);
                }
                else if (dist2 < maxClickRange)
                {
                    SpawnCharacterAt(spawnPoint2);
                }
            }
        }
    }

    void SpawnCharacterAt(GameObject point)
    {
        Instantiate(selectedCharacter, point.transform.position, Quaternion.identity);
        int cost = selectedCharacter == character1Prefab ? character1Cost : character2Cost;
        barFiller.ConsumeDots(cost);

        spawnPoint1.SetActive(false);
        spawnPoint2.SetActive(false);
        selectedCharacter = null;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class SpawnPlayer : MonoBehaviour
{
    public Button characterButton;           // Button reference
    public Transform spawnPoint;             // Where to spawn the character
    public float moveSpeed = 2f;             // Speed to walk forward

    private GameObject playerPrefab;         // The character prefab from the button child

    void Start()
    {
        // Find the child GameObject under the button (inactive prefab)
        playerPrefab = characterButton.GetComponentInChildren<Transform>(true).gameObject;

        // Hook up button click
        characterButton.onClick.AddListener(OnCharacterButtonClick);
    }

    void OnCharacterButtonClick()
    {
        // Spawn a copy at spawn point
        GameObject player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        player.SetActive(true); // Ensure it's active so Animator plays

        // No need to set animator parameters, it will auto-play Walk
        player.AddComponent<SimpleForwardMover>().speed = moveSpeed;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class InfoButton : MonoBehaviour
{
    [Header("Card Data")]
    public CardData cardData;      // assign in Inspector
    public string levelKey;        // unique key e.g. "Knight_Level"

    [Header("Upgrade Path")]
    public int[] healthUpgrades;   // e.g. {5, 10, 20, 40}
    public int[] damageUpgrades;   // e.g. {2, 5, 8, 12}
    public int[] costUpgrades;     // e.g. {-1, -1, 0, 0}
    public int[] upgradeCosts;     // e.g. {200, 300, 500, 800}

    [Header("Upgrade Menu Reference")]
    public UpgradeCharacter upgradeMenu; // drag your UpgradeCharacter object here

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            upgradeMenu.setaudio();
            upgradeMenu.OpenUpgradeMenu(

                cardData,
                levelKey,
                healthUpgrades,
                damageUpgrades,
                costUpgrades,
                upgradeCosts
            );
        });
    }
}

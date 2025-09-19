using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardUpgradeOffer : MonoBehaviour
{
    public Transform offerPanel;               // Parent panel for UI
    public Image cardIconImage;                // Image component to show card icon
    public Text levelText;                     // Text: "Level 1 → Level 2"
    public Text healthUpgradeText;             // Text: "+20 Health"
    public Text attackUpgradeText;             // Text: "+10 Attack"
    public Button upgradeButton;               // Button to trigger upgrade

    public GameplayDeckLoader deckLoader;      // Reference to deck loader

    private CardData selectedCard;             // Chosen card for upgrade

    void Start()
    {

        ShowUpgradeOffer();
    }

    public void ShowUpgradeOffer()
    {
        string saved = PlayerPrefs.GetString("SelectedDeck", "");
        if (string.IsNullOrEmpty(saved)) return;

        string[] ids = saved.Split(',');
        List<CardData> deckCards = new List<CardData>();

        foreach (string id in ids)
        {
            CardData card = deckLoader.runtimeDeck.Find(c => c.cardID == id);
            if (card != null)
                deckCards.Add(card);
        }

        if (deckCards.Count == 0) return;
        selectedCard = deckCards[Random.Range(0, deckCards.Count)];

        // UI setup
        cardIconImage.sprite = selectedCard.cardIcon;
        int nextLevel = selectedCard.level + 1;
        int healthGain = 20;
        int attackGain = 10;

        levelText.text = $"Level {selectedCard.level} → {nextLevel}";
        healthUpgradeText.text = $"+{healthGain} Health";
        attackUpgradeText.text = $"+{attackGain} Attack";

        upgradeButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.AddListener(() =>
        {
            selectedCard.level += 1;
            selectedCard.health += healthGain;
            selectedCard.Attack_damage += attackGain;

            Debug.Log($"[MatchUpgrade] {selectedCard.cardID} upgraded to Level {selectedCard.level} (Match only)");

            offerPanel.gameObject.SetActive(false);
        });
    }
}
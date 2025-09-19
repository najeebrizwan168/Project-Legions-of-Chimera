using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameplayDeckLoader : MonoBehaviour
{
    public List<CardData> allCards; // Reference to original SO list
    public Transform buttonParent;
    public GameObject cardSlotPrefab;

    public List<CardData> runtimeDeck = new List<CardData>();

    void Start()
    {
        BuildRuntimeDeck(); // make match-only clones
        LoadDeck();
    }

    public void BuildRuntimeDeck()
    {
        runtimeDeck.Clear();

        string saved = PlayerPrefs.GetString("SelectedDeck", "");
        if (string.IsNullOrEmpty(saved)) return;

        string[] ids = saved.Split(',');
        foreach (string id in ids)
        {
            CardData baseCard = allCards.Find(c => c.cardID == id);
            if (baseCard != null)
            {
                runtimeDeck.Add(baseCard.Clone()); // Clone for match-only changes
            }
        }
    }

    void LoadDeck()
    {
        // ✅ Use runtimeDeck here, not allCards
        foreach (CardData card in runtimeDeck)
        {
            GameObject slot = Instantiate(cardSlotPrefab, buttonParent);
            CardSlotUI ui = slot.GetComponent<CardSlotUI>();
            ui.Setup(card); // Pass the match-only card clone

           
        }
    }
}

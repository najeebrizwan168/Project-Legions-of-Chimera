using UnityEngine;
using System.Collections.Generic;

public class ManualDeckSetup : MonoBehaviour
{
    public List<CardUI> manualCardUIs;   // Drag all 6 manually
    public List<CardData> cardDataList;  // Match order to UIs
    public DeckManager deckManager;      // Reference to your deck manager

    void Start()
    {
        for (int i = 0; i < cardDataList.Count; i++)  // ✅ Correct

        {
            CardData data = cardDataList[i];
            manualCardUIs[i].Setup(data, deckManager.AddCardToDeck);
        }
    }
}

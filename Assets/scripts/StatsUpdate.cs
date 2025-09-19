using UnityEngine;

public class StatsUpdate : MonoBehaviour
{
    public CardData[] allCards; // assign all ScriptableObjects in Inspector

    void Awake()
    {
        // Load saved stats for each card
        foreach (CardData card in allCards)
        {
            card.LoadSavedData();
        }
    }
}

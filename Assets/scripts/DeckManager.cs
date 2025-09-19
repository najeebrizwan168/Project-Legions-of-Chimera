using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour
{
    public Transform availableCardsParent;      // Left scroll area (content)
    public Transform selectedDeckParent;        // Right preview area
    public GameObject selectedCardSlotPrefab;   // Right icon slot prefab
    public List<CardData> allCards;             // Assign all your ScriptableObjects
    public Button confirmSelectButton;          // Bottom "Select" button
    public Button clearDeckButton;
    private AudioSource Audio;
   

    private List<CardData> selectedCards = new List<CardData>();
    private const int MaxDeckSize = 4;

    void Start()
    {
        confirmSelectButton.onClick.AddListener(SaveDeck);
        clearDeckButton.onClick.AddListener(ClearDeck);
        LoadSavedDeck();
        Audio = GetComponent<AudioSource>();
    }

    void LoadSavedDeck()
    {
        // Clear old UI
        for (int i = selectedDeckParent.childCount - 1; i >= 0; i--)
        {
            Destroy(selectedDeckParent.GetChild(i).gameObject);
        }

        selectedCards = new List<CardData>();
        string saved = PlayerPrefs.GetString("SelectedDeck", "");

        if (string.IsNullOrEmpty(saved)) return;

        string[] ids = saved.Split(',');

        foreach (string id in ids)
        {
            CardData card = allCards.Find(c => c.cardID == id);
            if (card != null && !selectedCards.Contains(card))
            {
                selectedCards.Add(card);
                CreateCardSlot(card);
            }
        }
    }

    void ClearDeck()
    {
        PlayerPrefs.DeleteKey("SelectedDeck");
        PlayerPrefs.Save();
        LoadSavedDeck();
        Audio.Play();
    }

    public void AddCardToDeck(CardData card)
    {
        if (selectedCards.Count >= MaxDeckSize || selectedCards.Contains(card))
            return;

        selectedCards.Add(card);
        CreateCardSlot(card);
        Debug.Log("addcardtodeck run");
    }

    public void SaveDeck()
    {
        List<string> ids = new List<string>();
        foreach (CardData card in selectedCards)
            ids.Add(card.cardID);

        PlayerPrefs.SetString("SelectedDeck", string.Join(",", ids));
        PlayerPrefs.Save();
        Audio.Play();
        Debug.Log("Deck saved: " + string.Join(",", ids));
    }
  

    // 🔥 NEW helper function to create card slot and apply adjustment
    private void CreateCardSlot(CardData card)
    {
        GameObject slot = Instantiate(selectedCardSlotPrefab, selectedDeckParent);
        Image img = slot.GetComponent<Image>();
        img.sprite = card.cardIcon;

        // Check the bool in CardData
        if (card.imageadjust)
        {
            // Adjust the size or scale here
            RectTransform rt = img.rectTransform;

            // Example adjustment: scale down to 80%
            rt.localScale = new Vector3(3f,3f, 1);

            // You could also adjust width/height directly:
            // rt.sizeDelta = new Vector2(80, 80);
        }
    }
}

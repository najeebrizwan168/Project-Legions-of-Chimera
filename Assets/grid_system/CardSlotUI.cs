using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardSlotUI : MonoBehaviour
{
    public Image iconImage;
    public CardData cardData;
    public TextMeshProUGUI txt;
    public TextMeshProUGUI text_level;
    public void Setup(CardData data)
    {
        cardData = data;
        iconImage.sprite = data.cardIcon;

        ApplyIconAdjustment(cardData);
        txt.text=cardData.energyCost.ToString();
       ;
        text_level.text ="Lvl : "+ PlayerPrefs.GetInt(cardData.cardID).ToString();

        GetComponent<Button>().onClick.AddListener(() =>
        {
            GameManager.Instance.SetSelectedCard(data);
            Debug.Log("Card Selected: " + data.cardID);
        });
    }

    private void ApplyIconAdjustment(CardData card)
    {
        if (card == null || iconImage == null) return;

        var rt = iconImage.rectTransform;

        if (card.imageadjust)
        {
            // Example adjustment: scale up (tune as you like)
            rt.localScale = new Vector3(2f, 2f, 1f);
            Debug.Log("imageadjust TRUE for " + card.cardID);
        }
        else
        {
            rt.localScale = Vector3.one;
            Debug.Log("imageadjust FALSE for " + card.cardID);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class UpgradeOfferUI : MonoBehaviour
{
    public Image cardIconDisplay;
    private CardData cardToUpgrade;

    public void Setup(CardData card)
    {
        cardToUpgrade = card;
        cardIconDisplay.sprite = card.cardIcon;
    }

    public void OnUpgradeClicked()
    {
        if (cardToUpgrade == null) return;

        cardToUpgrade.level += 1;
        cardToUpgrade.health += 20;
        cardToUpgrade.Attack_damage += 10;

        Debug.Log($"Upgraded {cardToUpgrade.cardID} to level {cardToUpgrade.level}");
    }
}

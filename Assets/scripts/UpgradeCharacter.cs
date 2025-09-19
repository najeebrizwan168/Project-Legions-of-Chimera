using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class UpgradeCharacter : MonoBehaviour
{
    [Header("UI References")]
    public Text health_text;
    public Text spawnCost_text;
    public Text damage_text;
    public Text nexthealth_text;
    public Text nextspawnCost_text;
    public Text nextdamage_text;
    public Text level_text;
    public Text upgradeCost_text;
    public Image characterImageUI;
    public Animator animator;

    [Header("Panels")]
    public GameObject inventory;
    public GameObject upgrade_menu;

    private CardData selectedCard;
    private string currentLevelKey;
    private int level;

    // Upgrade paths (per character)
    private int[] healthUpgrades;
    private int[] damageUpgrades;
    private int[] costUpgrades;
    private int[] upgradeCosts;
    int nextHealth;
    int nextDamage;
    int nextCost;
    AudioSource Audio;
    public AudioClip clip_info;
    public AudioClip clip_upgrade;
    public AudioClip upgrade_denays;
    public void OpenUpgradeMenu(CardData card, string levelKey, int[] hUp, int[] dUp, int[] cUp, int[] costUp)
    {
       
        selectedCard = card;
        currentLevelKey = levelKey;

        // Load saved SO values
        selectedCard.LoadSavedData();

        // Load saved level, default = 1
        level = PlayerPrefs.GetInt(currentLevelKey, 1);

        // Copy upgrade arrays
        healthUpgrades = hUp;
        damageUpgrades = dUp;
        costUpgrades = cUp;
        upgradeCosts = costUp;

        // Switch panels
        inventory.SetActive(false);
        upgrade_menu.SetActive(true);

        // Show character image
        characterImageUI.sprite = selectedCard.cardIcon;

        UpdateUI();
        setaudio();
        
    }


    void UpdateUI()
    {
        if (selectedCard == null) return;

        // Current values
        int currentHealth = selectedCard.health;
        int currentDamage = selectedCard.Attack_damage;
        int currentCost = selectedCard.energyCost;

        // Apply upgrades up to current level
        for (int i = 1; i < level; i++)
        {
            if (i - 1 < healthUpgrades.Length) currentHealth = healthUpgrades[i - 1];
            if (i - 1 < damageUpgrades.Length) currentDamage = damageUpgrades[i - 1];
            if (i - 1 < costUpgrades.Length) currentCost = costUpgrades[i - 1];
        }

        // Display current values
        health_text.text = currentHealth.ToString();
        damage_text.text = currentDamage.ToString();
        spawnCost_text.text = currentCost.ToString();
        level_text.text = "Lvl " + level;

        // Next upgrade preview
        if (level - 1 < upgradeCosts.Length)
        {
             nextHealth = (level - 1 < healthUpgrades.Length ? healthUpgrades[level - 1] : 0);
             nextDamage =  (level - 1 < damageUpgrades.Length ? damageUpgrades[level - 1] : 0);
             nextCost =  (level - 1 < costUpgrades.Length ? costUpgrades[level - 1] : 0);

            nexthealth_text.text = nextHealth.ToString();
            nextdamage_text.text = nextDamage.ToString();
            nextspawnCost_text.text = nextCost.ToString();
            upgradeCost_text.text = upgradeCosts[level - 1].ToString();
        }
        else
        {
            nexthealth_text.text = "-";
            nextdamage_text.text = "-";
            nextspawnCost_text.text = "-";
            upgradeCost_text.text = "MAX";
        }
    }

    public void UpgradeButton()
    {
       
        if (selectedCard == null) return;
        if (level - 1 >= upgradeCosts.Length) return; // maxed

        int coins = PlayerPrefs.GetInt("Coins", 0);
        int cost = upgradeCosts[level - 1];

        if (coins >= cost)
        {
            coins -= cost;
            level++;
            Audio.PlayOneShot(clip_upgrade);
            // Update SO values
            selectedCard.health = nextHealth;
            selectedCard.Attack_damage = nextDamage;
            selectedCard.energyCost = nextCost;

            // Save permanent values in PlayerPrefs
            PlayerPrefs.SetInt(selectedCard.cardID + "health", selectedCard.health);
            PlayerPrefs.SetInt(selectedCard.cardID + "damage", selectedCard.Attack_damage);
            PlayerPrefs.SetInt(selectedCard.cardID + "energy", selectedCard.energyCost);
            PlayerPrefs.SetInt(currentLevelKey, level);
            PlayerPrefs.SetInt("Coins", coins);
            PlayerPrefs.SetInt(selectedCard.cardID, level);
            PlayerPrefs.Save();

            UpdateUI();
        }
        else
        {
            Audio.PlayOneShot(upgrade_denays);
        }

    }
    public void BackToInvntory()
    {
        upgrade_menu.SetActive(false);
        inventory.SetActive(true);
    }
    public void setaudio()
    {
        Audio = GetComponent<AudioSource>();
        Audio.PlayOneShot(clip_info);
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static TreasureBox;
using Unity.VisualScripting;
[RequireComponent(typeof(AudioSource))]
public class TreasureBox : MonoBehaviour
{
    public enum Rarity { Common, Rare, Epic, Legendary }

    [System.Serializable]
    public class Reward
    {
        public string rewardName;    // "Coins", "Diamonds", "Sword"
        public Sprite rewardIcon;
        public int amount;
        public Rarity rarity;
        [Range(0f, 100f)] public float weight;
        public CardData cardReward; // if this reward is a character/card
        public bool isCurrency;     // true = coins/diamonds, false = card/item
        public string currencyKey;  // "Coins", "Diamonds" etc.
        public string key;
        public int playerId;
       
    }

  
    [System.Serializable]
    public class RarityDropRate
    {
        public Rarity rarity;
        [Range(0f, 100f)] public float probability;
    }

    [Header("Treasure Box Settings")]
    public List<RarityDropRate> rarityChances = new List<RarityDropRate>();
    public int treasure_price;
    public string playerPerf_key;
    

    [Header("Rewards Pool")]
    public List<Reward> rewards = new List<Reward>();

    [Header("UI References")]
    public Text rewardText;
    public Image rewardIcon;
    public GameObject rewardPanel;
    public Sprite insufficent_money;
    public Text insufficent_text;
    public bool Coins_bool;
    public Sprite diamond;
    public Sprite Gold;
    public Image Amount_type;
    


    [Header("Info Panel References")]
    public GameObject infoPanel;          // Panel that will hold the list of rewards
    public GameObject rewardInfoPrefab;   // Prefab with Text + Icon (for each reward)
    public Image close;
    private AudioSource audioSource;
    public AudioClip clip_open;
   
    public AudioClip clip_show;
    public AudioClip clip_deny;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (rewardPanel != null) rewardPanel.SetActive(false);
        if (infoPanel != null) infoPanel.SetActive(false);
        if (insufficent_text != null) insufficent_text.text = treasure_price.ToString();
        if (Coins_bool == true)
        {
            Amount_type.sprite = Gold;

        }
        else
        {
            Amount_type.sprite = diamond;
        }

    }

    public void OpenBox()
    {
        
        if (treasure_price <= PlayerPrefs.GetInt(playerPerf_key))
        {

            audioSource.PlayOneShot(clip_open);
            Reward reward = GetReward();

            // Save reward into PlayerPrefs (game memory)
            ApplyReward(reward);

            // Show in UI
            if (rewardPanel != null)
            {
                rewardPanel.SetActive(true);
                if (rewardText != null) rewardText.text = $"{reward.rewardName} x{reward.amount} ({reward.rarity})";
                if (rewardIcon != null) rewardIcon.sprite = reward.rewardIcon;
            }

            Debug.Log($"Player received: {reward.rewardName} x{reward.amount} - {reward.rarity}");
            int price = PlayerPrefs.GetInt(playerPerf_key);
            price -= treasure_price;
            PlayerPrefs.SetInt(playerPerf_key, price);

        }
        else
        {
            rewardPanel.SetActive(true);
            if (rewardText != null) rewardText.text = "Insuffient Money";
            if (rewardIcon != null) rewardIcon.sprite = insufficent_money;
            audioSource.
                PlayOneShot(clip_deny);
        }
    }

    private Reward GetReward()
    {
        // Step 1: Roll rarity
        Rarity chosenRarity = RollRarity();

        // Step 2: Collect rewards of that rarity
        List<Reward> pool = rewards.FindAll(r => r.rarity == chosenRarity);

        if (pool.Count == 0)
        {
            Debug.LogWarning("No rewards found for rarity: " + chosenRarity);
            return rewards[0];
        }

        // Step 3: Weighted roll inside rarity
        float totalWeight = 0f;
        foreach (Reward r in pool) totalWeight += r.weight;

        float roll = Random.value * totalWeight;

        foreach (Reward r in pool)
        {
            if (roll < r.weight)
                return r;
            roll -= r.weight;
        }
       
        return pool[0]; // fallback
    }

    private Rarity RollRarity()
    {
        float totalProb = 0f;
        foreach (var rate in rarityChances) totalProb += rate.probability;

        float roll = Random.value * totalProb;

        foreach (var rate in rarityChances)
        {
            if (roll < rate.probability)
                return rate.rarity;
            roll -= rate.probability;
        }
        return rarityChances[0].rarity; // fallback
    }

    private void ApplyReward(Reward reward)
    {
        if (reward.isCurrency)
        {
            // Example: Coins, Diamonds
            int current = PlayerPrefs.GetInt(reward.currencyKey, 0);
            current += reward.amount;
            PlayerPrefs.SetInt(reward.currencyKey, current);
            PlayerPrefs.Save();

            Debug.Log($"[TreasureBox] Added {reward.amount} {reward.currencyKey}. Total: {current}");
        }
        else if (reward.cardReward != null)
        {
            // Unlock a card/character using its ID
            PlayerPrefs.SetInt(reward.key, reward.playerId);
            PlayerPrefs.Save();

            Debug.Log($"[TreasureBox] Unlocked character: {reward.cardReward.cardID}");
        }
        else
        {
            Debug.LogWarning("[TreasureBox] Reward has no type defined!");
        }
        audioSource.PlayOneShot(clip_show);
    }

    // -------------------------------
    // Show ALL rewards inside this box
    // -------------------------------
    public void ShowAllRewards()
    {
        if (infoPanel == null || rewardInfoPrefab == null)
        {
            Debug.LogWarning("[TreasureBox] Missing InfoPanel or RewardInfoPrefab reference!");
            return;
        }

        // Clear old children
        foreach (Transform child in infoPanel.transform)
        {
            Destroy(child.gameObject);
        }

        infoPanel.SetActive(true);

        // Create a UI element for each reward
        foreach (Reward reward in rewards)
        {
            GameObject newRewardUI = Instantiate(rewardInfoPrefab, infoPanel.transform);

            Text txt = newRewardUI.GetComponentInChildren<Text>();
            Image img = newRewardUI.GetComponentInChildren<Image>();

            if (txt != null) txt.text = $"{reward.rewardName} x{reward.amount} ({reward.rarity})";
            if (img != null) img.sprite = reward.rewardIcon;
        }

        Debug.Log("[TreasureBox] Showing all possible rewards in InfoPanel.");
        audioSource.PlayOneShot(clip_show);
    }
    public void rewardcloser()
    {
        if (rewardPanel != null)
        {
           rewardPanel.SetActive(false);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting.Generated.PropertyProviders;

[System.Serializable]
public class RewardData
{
    public int starsRequired;              // e.g. 6, 12, 18 ...
    public Sprite rewardImage;             // image shown
    public string rewardType;              // "Coins" or "Diamonds"
    public int rewardAmount;               // amount to give
}

public class LevelRewardSystem : MonoBehaviour
{
    [Header("Reward Prefab Settings")]
    public GameObject rewardPrefab;     // Prefab with Image, Text, Button
    public Transform rewardPanelParent; // Parent where reward UI will be instantiated
    public RewardData[] rewards;        // Configure in Inspector
    public GameObject rewardPanel;
    [Header("Notification Badge")]
    public Image notificationImage;         // 👈 Drag the badge Image here
    public Sprite[] notificationSprites;    // 👈 index 0 = empty, 1 = "1", … 9 = "9", 10 = "9+"
    public AudioSource audioSource;
    private void Start()
    {
        GenerateRewardsUI();
        UpdateNotificationBadge();
    }

    private void GenerateRewardsUI()
    {
        int playerStars = PlayerPrefs.GetInt("Stars", 0);

        foreach (RewardData reward in rewards)
        {
            GameObject rewardObj = Instantiate(rewardPrefab, rewardPanelParent);

            // Get references
            Image rewardImage = rewardObj.transform.Find("RewardImage").GetComponent<Image>();
            TextMeshProUGUI rewardText = rewardObj.transform.Find("RewardText").GetComponent<TextMeshProUGUI>();
            Button claimButton = rewardObj.transform.Find("ClaimButton").GetComponent<Button>();
            Text buttonText = claimButton.GetComponentInChildren<Text>();

            // Setup image & text
            rewardImage.sprite = reward.rewardImage;
            rewardText.text = reward.rewardAmount + " " + reward.rewardType;

            // Check if already claimed
            string claimKey = "RewardClaimed_" + reward.starsRequired;
            bool alreadyClaimed = PlayerPrefs.GetInt(claimKey, 0) == 1;

            if (alreadyClaimed)
            {
                claimButton.interactable = false;
                buttonText.text = "Claimed";
            }
            else
            {
                // Check if player has enough stars
                if (playerStars >= reward.starsRequired)
                {
                    claimButton.interactable = true;
                    buttonText.text = "Claim";
                    claimButton.onClick.AddListener(() =>
                    {
                        ClaimReward(reward, claimButton, buttonText);
                        UpdateNotificationBadge(); // refresh after claiming

                    });
                }
                else
                {
                    claimButton.interactable = false;
                    buttonText.text = "Locked";
                }
            }
        }
    }

    private void ClaimReward(RewardData reward, Button claimButton, Text buttonText)
    {
        int currentAmount = PlayerPrefs.GetInt(reward.rewardType, 0);
        PlayerPrefs.SetInt(reward.rewardType, currentAmount + reward.rewardAmount);

        string claimKey = "RewardClaimed_" + reward.starsRequired;
        PlayerPrefs.SetInt(claimKey, 1);
        PlayerPrefs.Save();

        claimButton.interactable = false;
        buttonText.text = "Claimed";

        Debug.Log("Reward claimed: " + reward.rewardAmount + " " + reward.rewardType);
        audioSource.Play();
    }

    /// <summary>
    /// Updates the badge sprite based on how many unclaimed rewards exist.
    /// </summary>
    private void UpdateNotificationBadge()
    {
        int playerStars = PlayerPrefs.GetInt("Stars", 0);
        int unclaimedCount = 0;

        foreach (RewardData reward in rewards)
        {
            string claimKey = "RewardClaimed_" + reward.starsRequired;
            bool alreadyClaimed = PlayerPrefs.GetInt(claimKey, 0) == 1;

            if (!alreadyClaimed && playerStars >= reward.starsRequired)
            {
                unclaimedCount++;
            }
        }

        // Select correct sprite
        if (unclaimedCount <= 0)
        {
            notificationImage.sprite = notificationSprites[0]; // empty
        }
        else if (unclaimedCount >= notificationSprites.Length - 1)
        {
            notificationImage.sprite = notificationSprites[notificationSprites.Length - 1]; // "9+"
        }
        else
        {
            notificationImage.sprite = notificationSprites[unclaimedCount];
        }

        // Show/Hide badge if needed
        notificationImage.gameObject.SetActive(unclaimedCount > 0);
    }
    public void rewardpanelon()
    {
        audioSource.Play();
        rewardPanel.SetActive(true);
    }
    public void rewardpaneloff() { audioSource.Play(); rewardPanel.SetActive(false); }

}

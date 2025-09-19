using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public Image icon;
    public Button useButton;

    private CardData cardData;
    private System.Action<CardData> onUseCallback;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Setup(CardData data, System.Action<CardData> onUse)
    {
        cardData = data;
        icon.sprite = data.cardIcon;
        onUseCallback = onUse;
        
        useButton.onClick.RemoveAllListeners();
        useButton.onClick.AddListener(() =>
        {
            onUseCallback?.Invoke(cardData);
            playaudeio();
        });

        Debug.Log("setup run");
    }
   public void playaudeio()
    {
        audioSource.Play();
    }
}
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiPanelManager : MonoBehaviour
{
    [Header("Text Setup")]
    [TextArea]
    public string[] texts = new string[20];   // 20 strings
    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI EXP;

    [Header("Hero Images")]
    public Sprite[] heroSprites = new Sprite[5];   // 5 hero images
    public Image heroImage;

  
    [Header("Info")]
    public Button info;
    public GameObject info_panel;
    public static UiPanelManager Instance;
    public GameObject wavepanel;
    private void Update()
    {
        EXP.text = "EXP : " + GameManager.Inmatchscore;
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
       
        info.onClick.AddListener(infoShow);
       
    }
    private void OnEnable()
    {
        info_panel.SetActive(false);
        // Change text
        if (texts.Length > 0 && textDisplay != null)
        {
            int randomTextIndex = Random.Range(0, texts.Length);
            textDisplay.text = texts[randomTextIndex];
        }

        // Change hero image
        if (heroSprites.Length > 0 && heroImage != null)
        {
            int randomHeroIndex = Random.Range(0, heroSprites.Length);
            heroImage.sprite = heroSprites[randomHeroIndex];
        }

        // Change enemy image
       
    }
    public void infoShow()
    {
        info_panel.SetActive(true);
    }
    public  void activewavepanel()
    {
        wavepanel.SetActive(true);

    }
    public void disablewavepanel()
    {
        wavepanel.SetActive(false);
    }

}

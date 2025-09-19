using TMPro;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class Currency_converter : MonoBehaviour
{
    [Header("Gold Currency")]
    public int Gold_amount;
    public TextMeshProUGUI From;
    public Sprite Coin_image;
    private int coin;
    
    [Header("Diamond Currency")]
    public int Diamond_amount;
    public TextMeshProUGUI To;
    private int Diamond;
    public Sprite Diamond_image;

    [Header("Currency type")]
    public bool Currencytype;  // true = Gold -> Diamond, false = Diamond -> Gold
    public Image From_image;
    public Image To_image;

    [Header("UI")]
    public GameObject insufficent_panel;
    AudioSource audio_s;
    public AudioClip audioClip;
    public AudioClip deny;
    private void Start()
    {
       
        audio_s= GetComponent<AudioSource>();

        coin = PlayerPrefs.GetInt("Coins");
        Diamond = PlayerPrefs.GetInt("diamond");

        if (Currencytype)
        {
            From.text = Gold_amount.ToString() + " ==> ";
            To.text = Diamond_amount.ToString();
            From_image.sprite = Coin_image;
            To_image.sprite = Diamond_image;
        }
        else
        {
            From.text = Diamond_amount.ToString() + " ==> ";
            To.text = Gold_amount.ToString();
            From_image.sprite = Diamond_image;
            To_image.sprite = Coin_image;
        }

        insufficent_panel.SetActive(false);
    }

    public void convert()
    {
      
        coin = PlayerPrefs.GetInt("Coins");
        Diamond = PlayerPrefs.GetInt("diamond");

        if (Currencytype) // Converting Gold -> Diamond
        {
            if (coin >= Gold_amount)
            {
                audio_s.PlayOneShot(audioClip);
                coin -= Gold_amount;
                Diamond += Diamond_amount;
                PlayerPrefs.SetInt("Coins", coin);
                PlayerPrefs.SetInt("diamond", Diamond);
                PlayerPrefs.Save();
                insufficent_panel.SetActive(false);
                
            }
            else
            {
                insufficent_panel.SetActive(true);
                audio_s.PlayOneShot(deny);
            }
        }
        else // Converting Diamond -> Gold
        {
            if (Diamond >= Diamond_amount)
            {
                audio_s.PlayOneShot(audioClip);
                Diamond -= Diamond_amount;
                coin += Gold_amount;
                PlayerPrefs.SetInt("Coins", coin);
                PlayerPrefs.SetInt("diamond", Diamond);
                PlayerPrefs.Save();
                insufficent_panel.SetActive(false);
            }
            else
            {
                insufficent_panel.SetActive(true);
                audio_s.PlayOneShot(deny);
            }
        }
    }

    public void close()
    {
        insufficent_panel?.SetActive(false);
    }
}

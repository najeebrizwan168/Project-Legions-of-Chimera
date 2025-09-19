using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    private string coinKey = "Coins"; // Key used in PlayerPrefs
    public Text coin_text;
   
    void Start()
    {
        // If no coins stored yet, set to 0
        if (!PlayerPrefs.HasKey(coinKey))
        {
            PlayerPrefs.SetInt(coinKey, 0);
        }
        Debug.Log("Coins Loaded: " + GetCoins());
        Updatecointext();
    }
    private void Update()
    {
        Updatecointext();
    }


    // Function to add coins
    public void AddCoins()
    {
        int currentCoins = PlayerPrefs.GetInt(coinKey);
        currentCoins += 1000;
        PlayerPrefs.SetInt(coinKey, currentCoins);
        PlayerPrefs.SetInt("diamond", currentCoins);
        PlayerPrefs.Save();

        Debug.Log( 5 + " coins added! Total coins: " + currentCoins);
    }

    // Function to get current coins
    public int GetCoins()
    {
        return PlayerPrefs.GetInt(coinKey);
    }
    public void Updatecointext()
    {
        if (coin_text != null)
        {
            coin_text.text= GetCoins().ToString();    
        }
    }
   

}

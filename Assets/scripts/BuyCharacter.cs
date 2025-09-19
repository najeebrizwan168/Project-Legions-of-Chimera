using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]

public class BuyCharacter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int buy_amount;
    public GameObject info;
    public GameObject use;
    public CoinManager coinManager;
    public Animator animator;
    public GameObject Lock;
    public string playerperf_id;
    public int id = 1;
    public Text buytext;
    public AudioSource Audio;
    public AudioClip buyClip;
    public AudioClip buydeny;
    

    void Start()
    {

       Audio = GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt(playerperf_id) == id)
        {
            Destroy(Lock);
            info.SetActive(true);
            use.SetActive(true);
            transform.SetAsFirstSibling();
            return;
        }
        if (buytext != null)
        {
            buytext.text = buy_amount.ToString();
        }
       


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void buy()
    {

        if (PlayerPrefs.GetInt("Stars") >= buy_amount)
        {
            Audio.PlayOneShot(buyClip);
            animator.SetBool("unlock", true);

            
            Destroy(Lock, 2);
            info.SetActive(true);
            use.SetActive(true);

            PlayerPrefs.SetInt(playerperf_id, id);
            transform.SetAsFirstSibling();
        }
        else { Audio.PlayOneShot(buydeny); }
    }
    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

    }
    //original
    //public void buy()
    //{

    //    if (coinManager.GetCoins() >= buy_amount)
    //    {
    //        Audio.PlayOneShot(buyClip);
    //        animator.SetBool("unlock", true);

    //        int amount = PlayerPrefs.GetInt("Coins");
    //        amount = amount - buy_amount;
    //        PlayerPrefs.SetInt("Coins", amount);
    //        Destroy(Lock, 2);
    //        info.SetActive(true);
    //        use.SetActive(true);

    //        PlayerPrefs.SetInt(playerperf_id, id);
    //        transform.SetAsFirstSibling();
    //    }
    //    else { Audio.PlayOneShot(buydeny); }
    //}

}

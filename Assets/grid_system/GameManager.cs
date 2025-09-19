using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private CardData selectedCard;
    public GameObject gameoverPanel;
    public TextMeshProUGUI Exp;
    public int points;
    public TextMeshProUGUI expc;
    public int EXPCount;
    public Button timerremove;
    public GameObject victorypanel;
    VictoryPanelConfiguration panelConfiguration;
    public static int Inmatchscore { get; private set; } 

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
     panelConfiguration=victorypanel.GetComponent<VictoryPanelConfiguration>();
        panelConfiguration.enabled = true;
    }
   
    private void Update()
    {
        UpdateUI();
    }
    private void Start()
    {
        gameoverPanel.SetActive(false);
        Inmatchscore = 0;
        
        UpdateUI();
    }



    public void SetSelectedCard(CardData data)
    {
        selectedCard = data;
    }

    public CardData GetSelectedCard()
    {
        return selectedCard;
    }
    public void gameover()
    {
        gameoverPanel.SetActive(true);
        panelConfiguration.enabled=false;
        
    }
    IEnumerator PauseAfterAnimation()
    {
        // Wait in real time, NOT affected by timeScale
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 0; // Pause AFTER animation completes
    }
    public void EXP()
    {
        Inmatchscore += points;
      
        UpdateUI();
    }
  
    public void CallresetTime()
    {
        if (Inmatchscore >= EXPCount)
        {
            Timer.SetAllTimersToHalfSecond();
            
             expuse(EXPCount);
          
        }
        else
        {
            insuffient();
        }
       
    }
    public static void expuse(int points)
    {
        Inmatchscore-=points;
        Debug.Log("Use EXP");
    }
     public void insuffient()
    {
        expc.text = "Your EXP is Not Enough For This";
    }

    public void UpdateUI()
    {
        Exp.text = "EXP" + Inmatchscore.ToString();
    }
    
}

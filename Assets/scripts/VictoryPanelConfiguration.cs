using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class VictoryPanelConfiguration : MonoBehaviour
{
    [Header("VictoryPanel UI ")]
    public GameObject VictoryPanel;
    public TextMeshProUGUI text;

    private int current_level;
    private int levelCoin;
    public GameObject gameoverpanel;
    PausePanelConfigration pausePanelConfigration;
    AudioSource audiosource;
    public AudioClip audioClip;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
        VictoryPanel.SetActive(false);
        current_level = PlayerPrefs.GetInt("Level", 0);
        levelCoin = PlayerPrefs.GetInt("Coins", 0);
        pausePanelConfigration=gameoverpanel.GetComponent<PausePanelConfigration>();

    }

    public void levelComplete()
    {
        
        VictoryPanel.SetActive(true);
        pausePanelConfigration.enabled = false;
        // ✅ 1. Calculate stars based on health
        float healthPercent = HealthSystem.Instance.Gethealthremaining();
        int levelstar = 1;

        if (healthPercent >= 70)
        {
            levelstar = 3;
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
        }
           
        else if (healthPercent >= 50)
        {
            levelstar = 2;
            star1.SetActive(true);
            star2 .SetActive(true);
        }

        else
        {
            levelstar = 1;
            star1.SetActive(true);


        }
           

        // ✅ 2. Save level progress and stars
        LevelConnector.instance.LevelComplete(levelstar);

        // ✅ 3. Save rewards
        SetReward();
    }

    public void nextLevelMap()
    {
        GameObject soundObj = new GameObject("TempAudio");
        AudioSource tempSource = soundObj.AddComponent<AudioSource>();
        tempSource.clip = audioClip;
        tempSource.Play();
        DontDestroyOnLoad(soundObj);
        Destroy(soundObj, audioClip.length); // auto cleanup

        Time.timeScale = 1.0f;
        SceneManager.LoadScene("LevelMap");
    }


    private void SetReward()
    {
        // First time completing the level
        if (current_level <= LevelManager.SelectedLevel.levelIndex)
        {
            text.text = LevelManager.SelectedLevel.reward.ToString();
            PlayerPrefs.SetInt("Coins", levelCoin + LevelManager.SelectedLevel.reward);
        }
        // Replay (already completed level)
        else
        {
            text.text = LevelManager.SelectedLevel.After_level_reward.ToString();
            PlayerPrefs.SetInt("Coins", levelCoin + LevelManager.SelectedLevel.After_level_reward);
        }

        PlayerPrefs.Save();
    }
    //IEnumerator audioset()
    //{
    //    audiosource.PlayOneShot(audioClip);

    //    // Wait for the clip length
    //    yield return new WaitForSeconds(audioClip.length);

    //    Time.timeScale = 1.0f;
    //    SceneManager.LoadScene("LevelMap");
    //}
}

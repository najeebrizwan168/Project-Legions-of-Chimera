using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartPanelConfigurtion : MonoBehaviour
{
    [Header("StartPanel UI")]
    public TextMeshProUGUI Reward_text;
    public int diffculty_level;
    public TextMeshProUGUI diffculty_level_TMP;
    public Text level_text;
    
    [Range(300,10000 )] public int level_reward;
    [Header("Assign your UI Images here")]
    public Image image1;
    public Image image2;
    public Image image3;
    public GameObject back;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float dimAlpha = 0.3f; // Transparency for unselected images
    public int current;
    private void Start()
    {
        SetImageTransparency(diffculty_level);
       current= PlayerPrefs.GetInt("Level");

     
       
       
    }
    private void Update()
    {
        eable();
    }

    public void SetImageTransparency(int selected)
    {
        // Reset all to dim
        SetAlpha(image1, dimAlpha);
        SetAlpha(image2, dimAlpha);
        SetAlpha(image3, dimAlpha);

        // Set selected to full
        if (selected == 1) { SetAlpha(image1, 1f); diffculty_level_TMP.text = "Easy"; } 
        else if (selected == 2)
        {
            SetAlpha(image1, 1f);
            SetAlpha(image2, 1f);
            diffculty_level_TMP.text = "Medium";
        }

        else if (selected == 3)
        {
            SetAlpha(image1, 1f);
            SetAlpha(image2, 1f);
            SetAlpha(image3, 1f);
            diffculty_level_TMP.text = "Hard";

        }
       
    }

    private void SetAlpha(Image img, float alpha)
    {
        if (img != null)
        {
            Color c = img.color;
            c.a = alpha;
            img.color = c;
        }
    }
    public void setreward()
    {
        if (current <= LevelManager.SelectedLevel.levelIndex)
        {
            Reward_text.text = LevelManager.SelectedLevel.reward.ToString();
        }
        else if (current >= LevelManager.SelectedLevel.levelIndex)
        {
            Reward_text.text = LevelManager.SelectedLevel.After_level_reward.ToString();
        }
    }
    private void eable()
    {
        if (gameObject.activeInHierarchy || enabled)
          back.SetActive(true);

        Debug.Log("Running only when enabled!");
    }

    public void start_panel()
    {

        GameObject soundObj = new GameObject("TempAudio");
        AudioSource tempSource = soundObj.AddComponent<AudioSource>();
        tempSource.clip = clip;
        tempSource.Play();
        DontDestroyOnLoad(soundObj);
        Destroy(soundObj, clip.length); // auto cleanup
        SceneManager.LoadScene("Loading");

    }
    public void leveltextsetter()
    {
        level_text.text = "Level " + (LevelManager.SelectedLevel.levelIndex + 1).ToString();
    }
    public void  backtolevel()
    {
       
       back.SetActive(false);
       gameObject.SetActive(false);
      
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelConnector : MonoBehaviour
{
    [Header("All Level Buttons / Objects")]
    public GameObject[] levelObjects;  // Assign all your level objects in Inspector

    private int currentLevel = 0;
    [Header("StartPanel UI")]
    public GameObject StartPanel;
    public Button Start_Button;
    [Header("Level Management")]
   // public LevelKey LevelKey_script;
    public string level_key;
    public static LevelConnector instance;
    public GameObject chapter1;
    public GameObject chapter2;
    public TextMeshProUGUI chaptertext;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public Text leveltext;
  


    void Start()
    {
       
       
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
            StartPanel.SetActive(false);
        // Initialize PlayerPrefs if not exists
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 0);
            PlayerPrefs.Save();
        }

        currentLevel = PlayerPrefs.GetInt("Level");

        // Load saved progress (stars + cup for each level)
        LoadLevelUI();
        
    }
    private void Update()
    {

        if (LevelManager.SelectedLevel!=null) {
            leveltext.text = "Level : " + LevelManager.SelectedLevel.levelIndex ;
        }
       

    }
    // Call this function when player finishes a level
    public void LevelComplete(int starsEarned)
    {
        currentLevel = PlayerPrefs.GetInt("Level");
        int currentstar = PlayerPrefs.GetInt("Level" + LevelManager.SelectedLevel.levelIndex + "_Stars");
        int totalstar= PlayerPrefs.GetInt("Stars");
        // Save stars for current level (clamped between 0–3)
        int finalStars = Mathf.Clamp(starsEarned, 0, 3);
        PlayerPrefs.SetInt("Level" + LevelManager.SelectedLevel.levelIndex + "_Stars", finalStars);
        if (LevelManager.SelectedLevel.levelIndex >= currentLevel || currentLevel == 0)
        {
           PlayerPrefs.SetInt("Stars",totalstar+finalStars);
        }
       else  if (currentstar < finalStars)
        {
            totalstar -= currentstar;
            totalstar += finalStars;
            PlayerPrefs.SetInt("Stars",totalstar);
        }

            // Save cup unlocked
            PlayerPrefs.SetInt("Level" + LevelManager.SelectedLevel.levelIndex + "_Cup", 1);
        if (LevelManager.SelectedLevel.levelIndex >= currentLevel || currentLevel==0)
        {
            PlayerPrefs.SetInt("Level", currentLevel + 1);
        }
        // Unlock next level
       

        PlayerPrefs.Save();

        // Update UI immediately
        LoadLevelUI();
    }

    // Load all levels' UI (stars + cups + lock/unlock)
    private void LoadLevelUI()
    {
        int savedLevel = PlayerPrefs.GetInt("Level");
       
        for (int i = 0; i < levelObjects.Length; i++)
        {
            GameObject level = levelObjects[i];

            // Skip if the reference is missing or destroyed
            if (level == null) continue;

            // Lock/Unlock levels
            level.SetActive(i <= savedLevel);

            // --- rest of your star & cup logic ---
            Transform[] children = level.GetComponentsInChildren<Transform>(true);
            GameObject[] stars = new GameObject[3];
            GameObject cup = null;

            foreach (Transform child in children)
            {
                if (child.CompareTag("Star1")) stars[0] = child.gameObject;
                if (child.CompareTag("Star2")) stars[1] = child.gameObject;
                if (child.CompareTag("Star3")) stars[2] = child.gameObject;
                if (child.CompareTag("Cup")) cup = child.gameObject;
            }

            foreach (var s in stars) if (s != null) s.SetActive(false);
            if (cup != null) cup.SetActive(false);

            int savedStars = PlayerPrefs.GetInt("Level" + i + "_Stars", 0);
            int cupUnlocked = PlayerPrefs.GetInt("Level" + i + "_Cup", 0);

            for (int j = 0; j < savedStars; j++)
            {
                if (stars[j] != null)
                    stars[j].SetActive(true);
            }

            if (cupUnlocked == 1 && cup != null)
                cup.SetActive(true);
        }
    }

    public void PanelLoad()
    {
        StartPanel.SetActive(true);
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene(level_key);
        Debug.Log("Scene Loaded" + " Level " + currentLevel);
    }

    public void startButtonFun()
    {
         
        Start_Button.onClick.AddListener(LoadLevel);

    }
    public void chapter1changer()

    {
        audioSource.PlayOneShot(audioClip);
        chapter1.SetActive(true);
        chapter2.SetActive(false);
        chaptertext.text = "Chapter 1";

        
    }
    public void chapter2changer()
    {
        audioSource.Play();
        chapter1.SetActive(false);
        chapter2.SetActive(true);
        chaptertext.text = "Chapter 2";
    }
}

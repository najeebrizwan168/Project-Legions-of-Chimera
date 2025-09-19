using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PausePanelConfigration : MonoBehaviour
{
    [Header("PausePanel Configuration")]
    public GameObject pausePanel;
    public Button pausebutton;
    public Button homebutton;
    public Button LevelSelection;
    public Button Restart;
    public float pausetime;
    public Button resumebutton;
    [SerializeField] AudioSource audioSource;
   
    public AudioClip clip;
    private void Start()
    { 
        if(pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
       
        Setui();
    }

    public void Setui()
    {
        if (pausebutton != null) { pausebutton.onClick.AddListener(setpauseButton); }
        else
        {
            homebutton.onClick.AddListener(SetHOmeButton);
            LevelSelection.onClick.AddListener(levelSelect);
            Restart.onClick.AddListener(restart);
            return;
        }
    }

    public void setpauseButton()
    {

        pausePanel.SetActive(true); // Show panel
        homebutton.onClick.AddListener(SetHOmeButton);
        LevelSelection.onClick.AddListener(levelSelect);
        Restart.onClick.AddListener(restart);
        resumebutton.onClick.AddListener(resume);
        audioSource.Play();
        // Start coroutine to pause AFTER animation plays
        StartCoroutine(PauseAfterAnimation());
    }
    public void audioshot()
    {
        GameObject soundObj = new GameObject("TempAudio");
        AudioSource tempSource = soundObj.AddComponent<AudioSource>();
        tempSource.clip = clip;
        tempSource.Play();
        DontDestroyOnLoad(soundObj);
        Destroy(soundObj, clip.length); // auto cleanup
    }
    public void SetHOmeButton()
    {
        Time.timeScale = 1; // Resume before changing scene
        SceneLoader.Load("mainmenu");
        audioshot();
    }

    public void restart()
    {
        Time.timeScale = 1; // Resume before restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        audioshot();
    }

    public void levelSelect()
    {
        audioshot();
        SceneLoader.Load("LevelMap");
        Time.timeScale = 1; 
       
       
    }

    public void resume()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    IEnumerator PauseAfterAnimation()
    {
        // Wait in real time, NOT affected by timeScale
        yield return new WaitForSecondsRealtime(pausetime);
        Time.timeScale = 0; // Pause AFTER animation completes
    }
}

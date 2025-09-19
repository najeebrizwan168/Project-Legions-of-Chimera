using UnityEngine;
using System.Collections;
public class LevelTimerSetup : MonoBehaviour
{
    [Header("Reference to Timer script in scene")]
    public Timer timer;
    public GameObject gameoverPanell;
    public AudioSource Audio;
    void Start()
    {
        gameoverPanell.SetActive(false);
        if (LevelManager.SelectedLevel == null)
        {
            Debug.LogError("❌ No level selected! Make sure LevelButton sets LevelManager.SelectedLevel.");
            return;
        }

        if (timer == null)
        {
            Debug.LogError("❌ No Timer reference assigned!");
            return;
        }

        // ✅ Set timer from LevelData
        int levelTime = LevelManager.SelectedLevel.level_time;
        timer.hours = 0;
        timer.minutes = levelTime / 60;
        timer.seconds = levelTime % 60;

        // ✅ Properly reset timer
        timer.StopTimer();     // clears old values
        timer.StartTimer();    // starts with the new values

        Debug.Log($"✅ Timer set for Level {LevelManager.SelectedLevel.levelIndex} → {levelTime} seconds");
    }
    public void enablegameoverpanel()
    {

        StartCoroutine(PauseAfterAnimation());
        gameoverPanell.SetActive(true);
        

       
    }
    IEnumerator PauseAfterAnimation()
    {
        Audio.Play();
        // Wait in real time, NOT affected by timeScale
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 0; // Pause AFTER animation completes
    }
}

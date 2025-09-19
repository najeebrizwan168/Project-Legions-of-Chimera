using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public static string sceneToLoad; // Scene name to load next
    public Text progressText;         // UI Text to display percentage

    private void Start()
    {
        // If this is the Loading Scene, start loading the target
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            StartCoroutine(LoadSceneAsync(sceneToLoad));
        }
    }

    /// <summary>
    /// Call this from anywhere to load a scene via the loading screen
    /// </summary>
    public static void Load(string targetScene)
    {
        sceneToLoad = targetScene;
        SceneManager.LoadScene("Loading UI"); // Replace with your loading scene name
    }

    private IEnumerator LoadSceneAsync(string targetScene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(targetScene);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            if (progressText != null)
                progressText.text = (progress * 100f).ToString("F0") + "%";

            if (operation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(0.3f); // Optional delay
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}

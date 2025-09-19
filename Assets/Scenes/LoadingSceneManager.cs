using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingSceneManager : MonoBehaviour
{
    public string gameplaySceneName = "Level0";
    public Text loadingText;

    void Start()
    {
        StartCoroutine(LoadGameScene());
    }

    IEnumerator LoadGameScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(gameplaySceneName);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            loadingText.text = $"Loading... {progress * 100:F0}%";

            if (progress >= 1f)
                break;

            yield return null;
        }

        // 🔥 Add manual delay before activating the scene
        yield return new WaitForSeconds(1f);

        // Allow activation
        asyncLoad.allowSceneActivation = true;
        yield return null;

        // Wait until the scene & enemies are ready
        yield return StartCoroutine(WaitForSceneReady());
        Debug.Log("✅ Scene & Enemies Ready!");
    }

    IEnumerator WaitForSceneReady()
    {
        Scene loadedScene = SceneManager.GetSceneByName(gameplaySceneName);
        while (!loadedScene.isLoaded)
            yield return null;

        SceneManager.SetActiveScene(loadedScene);

        SceneReadyChecker checker = null;
        while (checker == null)
        {
            checker = FindFirstObjectByType<SceneReadyChecker>();
            yield return null;
        }

        while (!checker.IsReady)
            yield return null;
    }
}

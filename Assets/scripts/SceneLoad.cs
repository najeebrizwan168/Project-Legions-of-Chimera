using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour
{
    [Header("Scene To Load")]
    public string sceneName = "mainmenu"; // Default value; can be changed in Inspector
   
    [Header("Optional Button Reference")]
    public Button loadButton;
    public bool gamescene;
    public AudioClip clip;
    
    void Start()
    {
        if (loadButton != null)
        {
            loadButton.onClick.AddListener(LoadScene);
        }
    }

    public void LoadScene()
    {
        GameObject soundObj = new GameObject("TempAudio");
        AudioSource tempSource = soundObj.AddComponent<AudioSource>();
        tempSource.clip = clip;
        tempSource.Play();
        DontDestroyOnLoad(soundObj);
        if(clip != null)
        {
            Destroy(soundObj, clip.length); // auto cleanup
        }
      
        SceneLoader.Load(sceneName);
       
            
    }
}

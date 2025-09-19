using UnityEngine;
using TMPro;

public class GameSettings : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TextMeshProUGUI nameDisplay;

    private const string playerNameKey = "Name";
    public GameObject settings;
    public AudioSource audioSource;
    public AudioClip clip_cancel;
  

    private void Start()
    {
       
        settings.SetActive(false);
        // Load saved name
        if (PlayerPrefs.HasKey(playerNameKey))
        {
            string savedName = PlayerPrefs.GetString(playerNameKey);
            nameDisplay.text = savedName;
            nameInputField.text = savedName;
        }
        else
        {
            nameDisplay.text = "Player";
        }

        // Hide input field by default
        nameInputField.gameObject.SetActive(false);
    }

    // Reset all saves
    public void NewGame()
    {
        audioSource.Play();
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        nameDisplay.text = "Player";
        nameInputField.text = "";
    }

    // Show the input field
    public void OnEditNameButton()
    {
        audioSource.Play();
        nameInputField.gameObject.SetActive(true);
    }

    // Save name
    public void SaveNewName()
    {
        audioSource.Play();
        string newName = nameInputField.text;

        if (!string.IsNullOrEmpty(newName))
        {
            PlayerPrefs.SetString(playerNameKey, newName);
            PlayerPrefs.Save();
            nameDisplay.text = newName;
        }

        nameInputField.gameObject.SetActive(false);
    }
    public void setting1()
    {
        settings.SetActive(false);
        audioSource.PlayOneShot(clip_cancel);
    }
    public void settingon()
    {
        settings.SetActive(true);
        audioSource.PlayOneShot(clip_cancel);

    }
   
   
    

}
